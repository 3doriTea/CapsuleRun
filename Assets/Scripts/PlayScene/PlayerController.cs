using UnityEngine;

namespace PlayScene
{
    public class PlayerController : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");


        }
    }
}
