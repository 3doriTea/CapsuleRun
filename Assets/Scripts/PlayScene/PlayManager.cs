using Unity.VisualScripting;
using UnityEngine;

namespace PlayScene
{
    public class PlayManager : MonoBehaviour
    {
        const float ToResultSceneTime = 3.0f;
        [SerializeField]
        private TimerController timerController;
        [SerializeField]
        private PlayerController playerController;
        private float goalTime = 0.0f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //Screen.autorotateToPortrait = false;
            //Screen.autorotateToLandscapeLeft = true;
            //Screen.autorotateToLandscapeRight = true;
            //Screen.autorotateToPortraitUpsideDown = false;
            //Screen.orientation = ScreenOrientation.AutoRotation;

            Screen.orientation = ScreenOrientation.LandscapeLeft;
            playerController.OnGoalAction.AddListener(OnGoal);
        }
        
        // Update is called once per frame
        void Update()
        {

        }

        private void OnGoal()
        {
            goalTime = timerController.StopAndGetTime();
            Invoke(nameof(ToResultScene), ToResultSceneTime);
        }
        
        private void ToResultScene()
        {
            
        }
    }
}
