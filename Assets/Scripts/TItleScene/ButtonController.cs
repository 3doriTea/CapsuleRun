using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{
    public class ButtonController : MonoBehaviour
    {
        const string PlaySceneName = "SampleScene";
        const string WalkSceneName = "Walking";

        public void OnButton(string name)
        {
            switch (name)
            {
                case "play":
                    SceneManager.LoadScene(PlaySceneName);
                    break;
                case "save":
                    SceneManager.LoadScene(WalkSceneName);
                    break;
                default:
                    break;
            }
        }
    }
}
