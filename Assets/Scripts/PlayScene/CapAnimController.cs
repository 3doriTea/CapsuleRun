using UnityEngine;

namespace PlayScene
{
    public class CapAnimController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public void SetIsRunning(bool isRunning)
        {
            animator.SetBool("Running", isRunning);
        }

        public void Jump()
        {
            animator.SetTrigger("Jump");
        }
    }
}
