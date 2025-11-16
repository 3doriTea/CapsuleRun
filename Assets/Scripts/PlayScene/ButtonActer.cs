using UnityEngine;

public class ButtonActer : MonoBehaviour
{
    [SerializeField]
    private GameObject breakGameObject;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Destroy(breakGameObject);
        Destroy(gameObject);
    }
}
