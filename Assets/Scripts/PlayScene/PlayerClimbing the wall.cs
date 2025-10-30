using UnityEngine;
using PlayScene;
using UnityEngine.UIElements.Experimental;
using Unity.VisualScripting;
using Unity.Mathematics;
public class PlayerClimbingthewall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Ray‚ð”ò‚Î‚·ŠJŽn“_
    public Transform rayOrigin;
    public float rayDistance = 0.5f;
    public LayerMask ClimbAbleLayer;
    private RaycastHit hit;
    void Start()
    {
        Vector3 direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Climb()
    {
       
    }
}
