using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f, gravity = -10f, jumpHeight = 15f, currentJumpHeight, groundRayDistance = 1.1f, dashSpeed = 15f, dashDistance = 5f;

    private CharacterController charC;
    private Vector3 motion; // Movement offset per frame
    private bool isJumping = false, isDashing = false;
    private float currentSpeed;
    private Vector3 dashStartPos;

    private void Start()
    {
        charC = GetComponent<CharacterController>();   
    }

    private void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        bool inputRun = Input.GetKey(KeyCode.LeftShift);
        bool inputJump = Input.GetButtonDown("Jump");

        Vector3 inputDir = new Vector3(inputH, 0, inputV);
        inputDir = transform.TransformDirection(inputDir);
        // If input exceeds 1 (i.e. if horizontal and vertical are pressed at the same time) then 
        if (inputDir.magnitude > 1)
        {
            // Normalize to 1
            inputDir.Normalize();
        }

        if (inputRun)
        {
            currentSpeed = runSpeed;
        }

        else
        {
            currentSpeed = walkSpeed;
        }

        if (isDashing)
        {
            print("Is dashing");
            float distance = Vector3.Distance(dashStartPos, transform.position);
            if (distance < dashDistance)
            {
                currentSpeed = dashSpeed;
            }

            else
            {
                currentSpeed = walkSpeed;
                isDashing = false;
            }
        }

        Move(inputDir.x, inputDir.z, currentSpeed);

        // If object is grounded
        if (charC.isGrounded)
        {
            // And jump is pressed?
            if (inputJump)
            {
                Jump(jumpHeight);
            }

            // Cancel the y velocity;
            motion.y = 0f;

            // Is jumping bool set to true?
            if (isJumping)
            {
                // Set jump height
                motion.y = currentJumpHeight;
                // Reset back to false
                isJumping = false;
            }
        }
        motion.y += gravity * Time.deltaTime;
        charC.Move(motion * Time.deltaTime);
    }

    private void Move(float inputH, float inputV, float speed)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        motion.x = direction.x * speed;
        motion.z = direction.z * speed;
    }

    public void Walk (float inputH, float inputV)
    {
        Move(inputH, inputV, walkSpeed);
    }

    public void Run (float inputH, float inputV)
    {
        Move(inputH, inputV, runSpeed);
    }

    public void Jump (float height)
    {
        isJumping = true;
        currentJumpHeight = height;
    }

    public void Dash()
    {
        isDashing = true;
        dashStartPos = transform.position;
    }

    //void Sprint(float inputH, float inputV)
    //{
    //    Vector3 direction = new Vector3(inputH, 0f, inputV);

    //    direction = transform.TransformDirection(direction);

    //    motion.x = direction.x * runSpeed;
    //    motion.z = direction.z * runSpeed;
    //}
    
    //private void Move(float inputH, float inputV, float speed)
    //{
    //    Vector3 direction = new Vector3(inputH, 0f, inputV);

    //    direction = transform.TransformDirection(direction);

    //    float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

    //    motion.x = direction.x * currentSpeed;
    //    motion.z = direction.z * currentSpeed;
    //}

    //#region Variables
    //[Header("Movement")]
    //public Vector3 moveDir;
    //public float moveSpeed;
    //public float jumpSpeed, gravity;
    //public CharacterController charC;
    //#endregion

    //// Start is called before the first frame update
    //void Start()
    //{
    //    charC = GetComponent<CharacterController>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    moveDir = transform.TransformDirection(moveDir);
    //    moveDir *= moveSpeed*Time.deltaTime;
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        moveDir.y = jumpSpeed*Time.deltaTime;
    //    }

    //    moveDir.y -= gravity * Time.deltaTime;
    //    charC.Move(moveDir);


    //}
}
