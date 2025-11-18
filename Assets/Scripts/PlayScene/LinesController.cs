using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayScene
{
	public class LinesController : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("�S�[�����C���̃Q�[���I�u�W�F�N�g�̖��O")]
		private string goalGameObjectName;

		[SerializeField]
		[Tooltip("���ԃe�[�v��S�[���e�[�v���܂Ƃ߂Ă������[�g")]
		private Transform linesRoot;
		[SerializeField]
		[Tooltip("�v���C���[�̍��W�n�R���|�[�l���g")]
		private Transform playerTransform;
		private float goalPositionX;  // �S�[����x���W
		private List<float> checkPointsPosX = new ();
		private int currentPoint;
		public Action<float, int> OnCheckPoint = (float x, int index) => { };
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
