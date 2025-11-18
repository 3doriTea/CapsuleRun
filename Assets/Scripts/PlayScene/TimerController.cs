using UnityEngine;
using TMPro;
using ResultScene;

namespace PlayScene
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMesh;
        private float timer = 0.0f;
        private bool stopped = false;

        void Start()
        {
            VerticalController.goalTime = 0.0f;
        }

        void Update()
        {
            if (stopped)
            {
                return;
            }

            timer += Time.deltaTime;

            var (minutes, seconds, milliseconds) = ToString(timer);
            textMesh.SetText($"{minutes:D2} : {seconds:D2} . {milliseconds:D2}");
        }

        public float StopAndGetTime()
        {
            VerticalController.goalTime = timer;
            stopped = true;
            return timer;
        }

        public static (int minutes, int seconds, int milliseconds)ToString(float time)
        {
            int minutes = (int)(time / 60.0f);
            int seconds = (int)(time % 60.0f);
            int milliseconds = (int)(time % 1.0f * 100.0f);

            return (minutes, seconds, milliseconds);
        }
    }
}
