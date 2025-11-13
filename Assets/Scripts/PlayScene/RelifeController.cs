using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PlayScene
{
    /// <summary>
    /// プレイヤーリトライの管理
    /// </summary>
    public class RelifeController : MonoBehaviour
    {
        const float DamageBeginY = -0.0f;
        const float DamageEndY = -30.0f;
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private Image image;
        
        private void Start()
        {
            Color color = image.color;
            color.a = 0.0f;
            image.color = color;
        }

        private void Update()
        {
            float y = playerTransform.position.y;
            // image.enabled = y <= DamageBeginY;
            if (y > DamageBeginY)
            {
                return;
            }

            float rate = (y - DamageBeginY) / (DamageEndY - DamageBeginY);

            if (rate >= 1.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }

            Color color = image.color;
            color.a = Easing.InOutCirc(rate);
            image.color = color;
        }
    }
}
