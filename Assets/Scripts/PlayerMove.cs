using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float jumpHeight = 5.0f;
    [SerializeField] float sprintSpeed = 2.0f;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] float fallspeed = 2;
    [SerializeField] CharacterController controller;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;

    [SerializeField] LayerMask groundMask;
    
    //private enum jumpState {AIRBORNE, LANDING, LANDED};
    private bool isGrounded;

    private enum moveType {STAND, WALK, SPRINT, FALLING}

    private moveType walkstate;

    private Vector3 airMomentum = Vector3.zero;
    private Vector3 groundMomentum = Vector3.zero;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        GameObject feet = GameObject.Find("PlayerFeet");
        groundCheck = feet.GetComponent<Transform>();
        velocity = Vector3.zero;
        walkstate = moveType.STAND;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Mover();
    }

    void Mover()
    {
        float boost = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            boost *= sprintSpeed;
        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        SetWalkState(xMove,zMove,boost);

        Vector3 move = (transform.right * xMove) + (transform.forward * zMove);
        if(walkstate == moveType.SPRINT)
        {
            groundMomentum = move * (boost * 0.8f);
        }
        else
        {
            groundMomentum = move;
        }

        if(!isGrounded)
        {
            move *= 0.7f;
            controller.Move(airMomentum * Time.deltaTime);
        }

        controller.Move((move * speed * boost) * Time.deltaTime);

    }

    //TODO: IMPLEMENT LANDING LAG SYSTEM 
    //(DONT WANT TO BE ABLE TO SPRINT BACKWARDS OR SIDEWAYS AT FULL SPEED AFTER LANDING. WANT MOMENTUM LEFT OVER FROM JUMP TO BE SOMEWHAT PRESERVED)
    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
            controller.Move(airMomentum*0.2f*Time.deltaTime); //preserve momentum upon landing
            airMomentum = Vector3.zero;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            if(walkstate == moveType.SPRINT)
            {
                velocity.y = Mathf.Sqrt((jumpHeight*1.33f) * -2f * gravity); //jump should be 33% higher when sprinting
            }
            else
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            print("Jumping!");
            airMomentum = groundMomentum * speed * 0.8f;
        }
        velocity.y += (gravity * fallspeed) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void SetWalkState(float xMove, float zMove, float boost)
    {
        if((xMove > 0.0f || zMove > 0.0f))
        {
            if(boost > 1.0f)
            {
                walkstate = moveType.SPRINT;
            }
            else
            {
                walkstate = moveType.WALK;
            }
        }
        else if(isGrounded)
        {
            walkstate = moveType.STAND;
        }
        else
        {
            walkstate = moveType.FALLING;
        }
    }
}
