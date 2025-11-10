using PlayScene;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float playerHeight;
    private float climbSpeed;
    private float wallSpeed;
    private float rayDistance;
    private LayerMask climbableMonkyBars;
    private Vector3 moveDirection = Vector3.zero;
    private RaycastHit monkeybarsHit;
    private PlayerController playerContoroller;
    private CharacterController pController;
    bool IsSwing = false;
     void Start()
     {
        playerContoroller = GetComponent<PlayerController>();
        pController = GetComponent<CharacterController>();
     }

    // Update is called once per frame
    void Update()
    {
        bool monkyBarsDetected = Physics.Raycast(transform.position, transform.up, out monkeybarsHit, rayDistance);
        Debug.DrawRay(transform.position, transform.up * rayDistance, new Color(0, 1, 1));


        if (monkyBarsDetected && !IsSwing)
        {
            Debug.Log("“Vˆä‚É‚Â‚¢‚Ä‚¢‚é");
           // StartSwing();
        }
        else if(IsSwing)
        {
            if(!monkyBarsDetected)
            //StopSwing();
        }
        if(IsSwing)
    }
}
