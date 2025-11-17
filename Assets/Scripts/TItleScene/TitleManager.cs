using UnityEngine;

namespace TitleScene
{
    public class TitleManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Screen.autorotateToPortrait = true;
            // Screen.autorotateToLandscapeLeft = false;
            // Screen.autorotateToLandscapeRight = false;
            // Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.Portrait;
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
