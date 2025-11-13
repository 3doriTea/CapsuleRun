using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WalkScene
{
    public class GaugeController : MonoBehaviour
    {
        const float OneCupValue = 100.0f;
        const float AnimTotalTime = 3.0f;

        [SerializeField]
        private TextMeshProUGUI cupCountTextMesh;  // 1カプセル溜まった分の個数表示
        [SerializeField]
        private Slider slider;

        float resultValue = 0.0f;  // 最終結果のゲージ量
        float currentValue = 0.0f;  // 現在のゲージ量
        float oneSecValue;  // 1秒で加算する量
        void Start()
        {
            resultValue = WalkingManager.TotalStepCount;
            Debug.Log(resultValue);
            oneSecValue = resultValue / AnimTotalTime;
        }

        void Update()
        {
            currentValue += oneSecValue * Time.deltaTime;
            if (currentValue >= resultValue)
            {
                currentValue = resultValue;
                Destroy(this);
                return;
            }

            // 溜まった個数を表示する
            cupCountTextMesh.text = $"x{(int)(currentValue / OneCupValue)}";
            slider.value = (currentValue % OneCupValue) / OneCupValue;
        }
    }
}
