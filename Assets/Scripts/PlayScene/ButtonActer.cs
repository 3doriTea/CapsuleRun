using UnityEngine;

namespace PlayScene
{
    public class ButtonActer : MonoBehaviour
    {
        const string PlayerTagName = "Player";

        [SerializeField]
        private GameObject breakGameObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == PlayerTagName)
            {
                Destroy(breakGameObject);
                breakGameObject = null;
                Destroy(gameObject);
            }
        }
    }
}
