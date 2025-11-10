using System.Runtime.CompilerServices;
using PlayScene;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float playerHeight;
    private float swingSpeed;
    private float walkSpeed;
    private float rayDistance;
    private LayerMask climbableMonkyBars;
    private Vector3 moveDirection = Vector3.zero;
    private RaycastHit monkeybarsHit;
    private PlayerController playerContoroller;
    private CharacterController pController;
    bool IsSwing = false;
    private float rayoffsetheight;
     void Start()
     {
        playerContoroller = GetComponent<PlayerController>();
        pController = GetComponent<CharacterController>();
     }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayoffsetheight;
        bool monkyBarsDetected = Physics.Raycast(rayOrigin, transform.up, out monkeybarsHit, rayDistance);
        Debug.DrawRay(rayOrigin, transform.up * rayDistance, new Color(0, 1, 1));


        if (monkyBarsDetected && !IsSwing)
        {
            Debug.Log("“Vˆä‚É‚Â‚¢‚Ä‚¢‚é");
             StartSwing();
        }
        else if(IsSwing)
        {
            if(!monkyBarsDetected)
            StopSwing();
        }
        if(IsSwing)
        {
            SwingMonkeyBars();
        }
        else
        {
            NomalMove();
        }
    
    }
    private void StartSwing()
    {
        IsSwing = true;
        Debug.Log("Swing Climb");
        moveDirection = Vector3.zero;
    }
    private void StopSwing()
    {
        IsSwing = false;
        Debug.Log("Stop Swing Climb");
        moveDirection.x = 0;
    }
    private void SwingMonkeyBars()
    {
        float horizontalInput = playerContoroller.status.InputMoveX 
                               * playerContoroller.status.InputMoveX;
        Vector3 swingSlide = Vector3.up * horizontalInput;

        Vector3 targetMove = swingSlide.normalized * swingSpeed;
        pController.Move(targetMove * Time.deltaTime);
    }
    private void  NomalMove()
    {
        float horizontalInput = playerContoroller.status.InputMoveX;
        moveDirection = new Vector3(horizontalInput, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= walkSpeed;
    }

}
