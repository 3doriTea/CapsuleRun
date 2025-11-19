using UnityEngine;

namespace TitleScene
{
    public class TitleManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //ScreenUtil.SetRotate(ScreenOrientation.LandscapeLeft);

            ScreenUtil.SetRotate(ScreenOrientation.Portrait);
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
