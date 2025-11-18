using UnityEngine;

public class HouseAnimController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void OnOpenHouse()
    {
        Debug.Log("OPENアニメーション");
        animator.SetTrigger("Open");
    }
}
