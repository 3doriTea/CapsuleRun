using UnityEngine;
using TMPro;
using PlayScene;

namespace ResultScene
{
    public class GoalTimeController : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;

        private void Start()
        {
            float time = VerticalController.goalTime;
            var (minutes, seconds, milliseconds) = TimerController.ToString(time);

            Debug.Log($"{minutes:D2}m {seconds:D2}s {milliseconds:D2}ms");
            textMesh.text = $"{minutes:D2}m {seconds:D2}s {milliseconds:D2}ms";
        }
    }
}
