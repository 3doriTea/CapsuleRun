using UnityEngine;

namespace WalkScene
{
    /// <summary>
    /// 歩く画面シーンを制御する
    /// </summary>
    public class WalkingController : MonoBehaviour
    {
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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int i = 0; i < (int)InnerScene.Max; i++)
            {
                sceneGameObjects[i] = transform.GetChild(i).gameObject;
                sceneGameObjects[i].SetActive(false);
            }
        }

        /// <summary>
        /// 個別シーンを遷移する
        /// </summary>
        /// <param name="next">次のシーン</param>
        public void MoveScene(InnerScene next)
        {
            // 現在のシーンゲームオブジェクトを無効化して次のシーンのを有効化する
            sceneGameObjects[(int)currentScene].SetActive(false);
            sceneGameObjects[(int)next].SetActive(true);
            currentScene = next;
        }
    }
}
