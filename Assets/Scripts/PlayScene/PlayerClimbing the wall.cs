using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using PlayScene;

[RequireComponent(typeof(PlayerController))]
public class PlayerClimbingthewall : MonoBehaviour
    {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private InputAction moveAction;

    public float climbSpeed = 3.0f;
    public float walkspeed = 5.0f;

    private float rayDistance = 2.0f;
    public LayerMask climbableWallLayer;
    private PlayerController playerController;
    private CharacterController PController;
    private Vector3 moveDirection = Vector3.zero;
    private bool IsClimbing = false;
    private RaycastHit wallHit;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        PController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //RayCastによる壁の検出
        bool wallDetected = Physics.Raycast(transform.position, transform.right, out wallHit, rayDistance);
        Debug.DrawRay(transform.position, transform.right * rayDistance, new Color(1, 0, 1));

        Mouse mouse = Mouse.current;
        // bool rightMouseHeld = Input.GetMouseButton(1);
        bool rightMouseHeld = mouse.rightButton.IsPressed();

        //壁に当たったら
        if (wallDetected && rightMouseHeld && !IsClimbing)
        {
            StartClimbing();
        }
        //壁から離れたら
        else if (IsClimbing && (!wallDetected || !rightMouseHeld))
        {
            StopClimbing();
        }
        if (IsClimbing)
        {
            HandClimbing();
        }
        else
        {
            NormalMove();
        }
    }
    private void StartClimbing()
    {
        IsClimbing = true;
        Debug.Log("壁を登ってるよ");
        moveDirection = Vector3.zero;
    }
    private void StopClimbing()
    {
        IsClimbing = false;
        Debug.Log("壁登りしていない");
        moveDirection.y = 0;
    }

    private void HandClimbing()
    {
       
            Vector3 wallNormal = wallHit.normal;
            Quaternion targetRotaion = Quaternion.LookRotation(-wallNormal, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotaion, Time.deltaTime * 10.0f); 
            
            float capuleHalfHeight = PController.height / 2.0f;
            Vector3 wallStickPosition = wallHit.point + wallNormal * 0.05f;

            //float verticalInput = Input.GetAxis("Vertical");
            // 横移動2乗して登る
            float verticalInput =
                playerController.status.InputMoveX
                * playerController.status.InputMoveX;


            
           //上に移動
           Vector3 climbUp = Vector3.up * verticalInput;

            //最終的な移動の方向
            Vector3 targetMove = climbUp.normalized * climbSpeed;

            PController.Move(targetMove * Time.deltaTime);

        


    }
       
        
    private void NormalMove()
    {
        //float verticalInput = Input.GetAxis("Vertical");
        //float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalInput = playerController.status.InputMoveX;
        moveDirection = new Vector3(horizontalInput, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= walkspeed;
    }


}