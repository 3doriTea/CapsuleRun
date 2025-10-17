using UnityEngine;
using UnityEngine.SceneManagement;

namespace WalkScene
{
    /// <summary>
    /// 歩く画面シーンを制御する
    /// </summary>
    public class WalkingController : MonoBehaviour
    {
        const string PlayScene = "SampleScene";

        /// <summary>
        /// Unityのシーンではなく、Walkシーンの中での個別シーン
        /// </summary>
        public enum InnerScene
        {
            Start,
            Stop,
            Walking,
            ToPlay,
            Max,
        }
        private readonly GameObject[] sceneGameObjects = new GameObject[(int)InnerScene.Max];
        private InnerScene currentScene = InnerScene.Start;

        public void OnButton(string name)
        {
            switch (name)
            {
                case "Finish":
                    MoveScene(InnerScene.ToPlay);
                    break;
                case "ToGame":
                    SceneManager.LoadScene(PlayScene);
                    break;
                default:
                    Debug.LogError($"Unknown button name:{name}");
                    break;
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int i = 0; i < (int)InnerScene.Max; i++)
            {
                sceneGameObjects[i] = transform.GetChild(i).gameObject;
                sceneGameObjects[i].SetActive(false);
            }
            MoveScene(InnerScene.Start);
        }

        /// <summary>
        /// 個別シーンを遷移する
        /// </summary>
        /// <param name="next">次のシーン</param>
        public void MoveScene(InnerScene next)
        {
            if (currentScene == next)
            {
                return;  // 今も次も同じなら無視
            }
            if (currentScene == InnerScene.Start && next == InnerScene.Stop)
            {
                return;  // 今がスタートでまだ止まっているならスタートのままにする
            }

            // 現在のシーンゲームオブジェクトを無効化して次のシーンのを有効化する
            sceneGameObjects[(int)currentScene].SetActive(false);
            sceneGameObjects[(int)next].SetActive(true);
            currentScene = next;
        }
    }
}
