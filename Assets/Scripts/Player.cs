using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f, gravity = 10f, jumpHeight = 15f, groundRayDistance = 1.1f, sprintFactor = 2f;

    private CharacterController charC;
    private Vector3 motion; // Movement offset per frame

    private void Start()
    {
        charC = GetComponent<CharacterController>();   
    }

    private void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    inputH *= sprintFactor;
        //    inputV *= sprintFactor;
        //}

        Move(inputH, inputV);

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                motion.y = jumpHeight;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint(inputH, inputV);
        }

        motion.y -= gravity * Time.deltaTime;
        charC.Move(motion * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Raycast below the player
        Ray groundRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        // If hitting something
        if (Physics.Raycast(groundRay, out hit, groundRayDistance))
        {
            // Return true
            return true;
        }
        // Else
        // Return false
        return false;
    }

    private void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        direction = transform.TransformDirection(direction);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed; 

        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }

    void Sprint(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        direction = transform.TransformDirection(direction);

        motion.x = direction.x * runSpeed;
        motion.z = direction.z * runSpeed;
    }

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
