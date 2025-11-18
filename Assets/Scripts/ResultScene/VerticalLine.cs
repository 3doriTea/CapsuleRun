using UnityEngine;
using UnityEngine.UIElements;

namespace ResultScene
{
    /// <summary>
    /// 結果シーンで出てくる経過時間のバーティカル棒
    /// </summary>
    public class VerticalLine : MonoBehaviour
    {
        const float PosMin = 500.0f;
        const float PosMax = 1350.0f;
        const float HeightMax = 340.0f;

        [SerializeField]
        private RectTransform rectTransform;

        public void SetHeightRate(float rate)
        {
            Vector2 size = rectTransform.sizeDelta;
            size.y = HeightMax * rate;
            rectTransform.sizeDelta = size;
        }

        public void SetPosRate(float rate)
        {
            Vector3 position = rectTransform.position;
            position.x = (PosMax - PosMin) * rate + PosMin;
            rectTransform.position = position;
        }
    }
}
