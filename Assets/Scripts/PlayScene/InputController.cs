using UnityEngine;
using UnityEngine.UI;

namespace PlayScene
{
    public class InputController : MonoBehaviour
    {
        const string ButtonNameJump = "Jump";
        const string ButtonNameLeft = "Left";
        const string ButtonNameRight = "Right";

        [SerializeField]
        private Button[] buttonJump;
        [SerializeField]
        private Button buttonLeft;
        [SerializeField]
        private Button buttonRight;

        // ジャンプ入力
        public bool InputJump { get; private set; }
        // 左右方向の入力
        public float InputHorizontal { get; private set; }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnButtonDown(string name)
        {
            switch (name)
            {
                case ButtonNameJump:
                    InputJump = true;
                    break;
                case ButtonNameLeft:
                    InputHorizontal = -1.0f;
                    break;
                case ButtonNameRight:
                    InputHorizontal = +1.0f;
                    break;
                default:
                    Debug.Log($"unknown button name: {name} from Input Controller");
                    break;
            }
        }
        public void OnButtonUp(string name)
        {
            switch (name)
            {
                case ButtonNameJump:
                    InputJump = false;
                    break;
                case ButtonNameLeft:
                    InputHorizontal = 0;
                    break;
                case ButtonNameRight:
                    InputHorizontal = 0;
                    break;
                default:
                    Debug.Log($"unknown button name: {name} from Input Controller");
                    break;
            }
        }
    }
}
