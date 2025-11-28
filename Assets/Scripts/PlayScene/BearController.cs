using UnityEngine;

namespace PlayScene
{
    public class BearController : MonoBehaviour
    {
        const float ToKillPlayerTeleportPositionY = -10.0f;  // プレイヤ倒すためにテレポートさせる高さ
        const string PlayerTagName = "Player";
        [SerializeField]
        private Transform playerTransform;
        private bool isOnPlayer;  // プレイヤーが侵入しているか true / false

        [SerializeField]
        private bool atakking;  // 現在攻撃中か

        [SerializeField]
        private Animator animator;

        private void OnTriggerEnter(Collider other)
        {
            // 侵入したのがプレイヤーなら
            if (other.gameObject.tag == PlayerTagName)
            {
                isOnPlayer = true;
                animator.SetBool("isInPlayer", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // 脱出したのがプレイヤーなら
            if (other.gameObject.tag == PlayerTagName)
            {
                isOnPlayer = false;
                animator.SetBool("isInPlayer", false);
            }
        }

        private void Update()
        {
            if (atakking && isOnPlayer)
            {
                PlayerController playerController = playerTransform.GetComponent<PlayerController>();
                playerController.status.Velocity
                    += new Vector3(-1.0f, 0.3f).normalized * 2.0f;
                playerController.status.FaintTimeLeft = 3.0f;
            }
        }
    }
}
