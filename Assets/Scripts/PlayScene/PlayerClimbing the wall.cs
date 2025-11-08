using UnityEngine;
using System.Collections.Generic;
public class PlayerClimbingthewall : MonoBehaviour
    {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   
    public float climbSpeed = 3.0f;
    public float walkspeed = 5.0f;
 
    public float rayDistance = 0.6f;
    public LayerMask climbableWallLayer;

    private PlayerController PController = FindObjectOfType<PController>();
    private Vector3 moveDirection = Vector.zero;
    private bool IsClimbing = false;
    private RaycastHit wallHit;
        
        void Start()
        {
          PController = GetComponent<PlayerController>();


        }

    // Update is called once per frame
    void Update()
       {
        //RayCastÇ…ÇÊÇÈï«ÇÃåüèo
        bool wallDetected = Physics.Raycast(transform.position, transform.forward, out wallHit,rayDistance);

        bool rightMouseHeld = Input.GetMouseButton(1);

        if(wallDetected && rightMouseHeld && !IsClimbing)
        {
            StartClimbing();
        }
        else if(IsClimbing && (!wallDetected ||!rightMouseHeld))
        {
            StopClimbing();
        }
        if(IsClimbing)
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
        Debug.Log("ï«ÇìoÇ¡ÇƒÇÈÇÊ");
        moveDirection = Vector.zero;
       }
       private void StopClimbing()
       {
        IsClimbing = false;
        Debug.Log("ï«ìoÇËÇµÇƒÇ¢Ç»Ç¢");
        moveDirection.y = 0;
        }

    private void HandClimbing()
    {
        Vector3 wallNormal = wallHit.normal;
        float capuleHalfHeight = PController.height / 2.0f;
        Vector3 wallStickPosition = wallHit.point + wallNormal * 0.05f;

        float verticalInput = Input.GetAxis("Vertical");

        Vecor3 climbUp = Vector3.up * verticalInput;

        Vecor3 targetMove = climbUp.normalized * climbSpeed;

        PController.Move(targetMove * Time.deltaTime);
    }
    private void NormalMove()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = transform.TramsformDirection(moveDirection);
        moveDirection *= walkspeed;
    }

    
}




