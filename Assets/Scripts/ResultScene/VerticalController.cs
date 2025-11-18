using System.Collections.Generic;
using UnityEngine;

namespace ResultScene
{
    public class CheckPointData
    {
        public float PositionRate;  // チェックポイントの位置 (スタート地点からのレート)
        public float Timestamp;  // タイムスタンプ
    }

    public class VerticalController : MonoBehaviour
    {
        public static float goalTime = 0.0f;
        public static List<CheckPointData> checkPointData = new();

        [SerializeField]
        private GameObject verticalLineOrigin;

        void Start()
        {
            foreach (CheckPointData data in checkPointData)
            {
                GameObject verticalLine = Instantiate(verticalLineOrigin, null, true);
                VerticalLine vertical = verticalLine.GetComponent<VerticalLine>();
                vertical.SetPosRate(data.PositionRate);
                vertical.SetHeightRate(data.Timestamp / goalTime);
            }
        }
    }
}
