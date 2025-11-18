using UnityEngine;

public class ButtonActer : MonoBehaviour
{
    [SerializeField]
    private GameObject breakGameObject;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log($"ƒqƒbƒg:{hit.gameObject.name}");
        Destroy(breakGameObject);
        Destroy(gameObject);
    }
}
