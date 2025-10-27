using UnityEngine;
using PlayScene;
using UnityEngine.UIElements.Experimental;
using Unity.VisualScripting;
public class PlayerClimbingthewall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void Climb()
    {
        bool isClimd = false;
        bool isGlab = false;
        Ray wallCheck = new Ray(transform.position + Vector3.up, transform.forward);
        Ray upperWallCheck = new Ray(transform.position + Vector3.up, transform.forward);

        bool isForwardwall = Physics.Raycast(wallCheck);
        bool isUpperWall = Physics.Raycast(upperWallCheck);


        Vector3 ClimbPos;
        Vector3 ClimbOldPos;
        if (isForwardwall && !isForwardwall)
        {
            isGlab = true;
        }
        if (isGlab && !isForwardwall)
        {
            ClimbOldPos = transform.position;

            ClimbPos = transform.position + transform.forward * 4 + Vector3.up * 5.5f;
            isGlab = false;
            isClimd = true;

        }
        if(isClimd)
        {
           
           
        }
    }
}
