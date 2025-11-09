using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayScene
{
	public class LinesController : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("ゴールラインのゲームオブジェクトの名前")]
		private string goalGameObjectName;

		[SerializeField]
		[Tooltip("中間テープやゴールテープをまとめておくルート")]
		private Transform linesRoot;
		[SerializeField]
		[Tooltip("プレイヤーの座標系コンポーネント")]
		private Transform playerTransform;
		private float goalPositionX;  // ゴールのx座標
		private List<float> checkPointsPosX = new ();
		private int currentPoint;
		public UnityAction<float, int> OnCheckPoint = (float x, int index) => { };
		void Start()
		{
			foreach (Transform transform in linesRoot)
			{
				if (transform.name == goalGameObjectName)
				{
					goalPositionX = transform.position.x;
					continue;
				}
				else
				{
					checkPointsPosX.Add(transform.position.x);
				}
			}
			checkPointsPosX.Sort();

			currentPoint = 0;
			foreach (float checkPointPosX in checkPointsPosX)
			{
				if (checkPointPosX < playerTransform.position.x)
				{
					currentPoint++;
				}
				else
				{
					break;
				}
			}
		}

		void Update()
		{
			if (checkPointsPosX.Count >= currentPoint)
			{
				return;
			}
			if (checkPointsPosX[currentPoint] <= playerTransform.position.x)
			{
				OnCheckPoint(checkPointsPosX[currentPoint], currentPoint);
				currentPoint++;
			}
		}

		public float GetGoalDistanceRate()
		{
			return playerTransform.position.x / goalPositionX;
		}
	}
}
