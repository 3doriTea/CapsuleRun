using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace WalkScene
{
    public class InfoController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI stepText;
        [SerializeField]
        private TextMeshProUGUI distText;
        [SerializeField]
        private TextMeshProUGUI timeText;
        private DateTime startTime = DateTime.MaxValue;

        public void Update()
        {
            DateTime now = DateTime.Now;
            if (startTime > now)
            {
                timeText.text = $"__ __ __";
                return;  // まだ初期化されていない
            }
            TimeSpan diff = now - startTime;
            timeText.text = $"{(int)diff.TotalHours}h {diff.Minutes:00}:{diff.Seconds:00}";
        }

        public void SetStepCount(int count)
        {
            stepText.text = $"Step : {count}";
        }

        public void SetDistance(float distance)
        {
            distText.text = $"Dist : {distance:0.0}km";
        }

        public void SetStartTime(DateTime at)
        {
            startTime = at;
        }
    }
}
