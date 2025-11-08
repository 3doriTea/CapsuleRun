using UnityEngine;
using UnityEngine.UI;

namespace PlayScene
{
    public class ToGoalSliderController : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private LinesController linesController;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            slider.value = linesController.GetGoalDistanceRate();
        }
    }
}
