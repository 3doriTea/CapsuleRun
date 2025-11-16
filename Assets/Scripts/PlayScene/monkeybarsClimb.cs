using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using PlayScene;


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
    private RaycastHit hit;
    private PlayerController playerContoroller;
    private CharacterController pController;
    bool IsSwing = false;
    private float maxDistance = 1.0f;
    private Vector3 rayoffsetheight = new Vector3(0,0.5f,0);
     void Start()
     {
        playerContoroller = GetComponent<PlayerController>();
        pController = GetComponent<CharacterController>();
     }

    // Update is called once per frame
    void Update()
    {
       // playerContoroller.status.IsTouchWallUp
        //  Vector3 rayOrigin = transform.position + Vector3.up * rayoffsetheight;
        Vector3 rayOrigin = transform.position +  rayoffsetheight;
        bool monkyBarsDetected = Physics.Raycast(rayOrigin, transform.up, out hit, maxDistance, climbableMonkyBars);
        Debug.DrawRay(rayOrigin, transform.up * rayDistance, Color.red);


        //if (monkyBarsDetected && !IsSwing)
        //{
        //    Debug.Log("天井に衝突しました");
        //     StartSwing();
        //}
        //else if(IsSwing)
        //{
        //    if(!monkyBarsDetected)
        //    {
        //        Debug.Log("天井はない");
        //        StopSwing();
        //    }
            
        //}
        if(monkyBarsDetected)
        {
            Debug.Log("天井に衝突しました");
        }
        else
        {
            Debug.Log("地面にいる");
        }
        if (IsSwing)
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
        Debug.Log("天井を伝っているよ");
        moveDirection = Vector3.zero;
      
    }
    private void StopSwing()
    {
        IsSwing = false;
        Debug.Log("天井に付いていないよ");
        moveDirection.y = 0;
    }
    private void SwingMonkeyBars()
    {
        float horizontalInput = playerContoroller.status.InputMoveX * playerContoroller.status.InputMoveX;
        //左に移動
        Vector3 swingSlide = Vector3.left * horizontalInput;

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
