using UnityEngine;

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
        void Start()
        {
            foreach (Transform transform in linesRoot)
            {
                if (transform.name == goalGameObjectName)
                {
                    goalPositionX = transform.position.x;
                    continue;
                }
            }
        }

        void Update()
        {
            
        }

        public float GetGoalDistanceRate()
        {
            return playerTransform.position.x / goalPositionX;
        }
    }
}
