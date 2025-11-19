using UnityEngine;
using UnityEngine.SceneManagement;

namespace ResultScene
{
    public class ButtonController : MonoBehaviour
    {
        const string SceneNameWalk = "Walking";
        const string SceneNameSample = "SampleScene";

        /// <summary>
        /// ボタンが押されたときの処理
        /// </summary>
        /// <param name="name">ボタン名</param>
        public void OnButton(string name)
        {
            switch (name)
            {
                case "walk":
                    SceneManager.LoadScene(SceneNameWalk);
                    break;
                case "retry":
                    SceneManager.LoadScene(SceneNameSample);
                    break;
                default:
                    Debug.LogError("処理が未実装のボタン名");
                    break;
            }
        }
    }
}
