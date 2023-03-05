using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    public float speed = 10;
    public float groundDrag;
    public float jumpForce;
    public float cooldown;
    public float airMult;
    public bool canJump;

    [Header("Grounded")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public bool MoveLock;

    float xInput;
    float zInput;

    Vector3 moveDirection;

    Rigidbody rBody;

    public Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.freezeRotation = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveLock == false)
        {
            FixedUpdate();
        }

    }

    void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        playerInput();
        PlayerMove();
        VelControl();

        if (grounded)
        {
            rBody.drag = groundDrag;
        }
        else
        {
            rBody.drag = 0;
        }
    }

    void playerInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown("space") && canJump && grounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), cooldown);
        }
    }

    void PlayerMove()
    {
        moveDirection = orientation.forward * zInput + orientation.right * xInput;

        if(grounded)
        {
            rBody.AddForce(moveDirection.normalized * speed, ForceMode.Force);
        }

        else if(!grounded)
        {
            rBody.AddForce(moveDirection.normalized * speed * airMult, ForceMode.Force);
        }
    }

    void VelControl()
    {
        Vector3 costVel = new Vector3(rBody.velocity.x, 0f, rBody.velocity.z);

        if(costVel.magnitude > speed)
        {
            Vector3 velLimit = costVel.normalized * speed;
            rBody.velocity = new Vector3(velLimit.x, rBody.velocity.y, velLimit.z);
        }
    }

    void Jump()
    {
        rBody.velocity = new Vector3(rBody.velocity.x, 0f, rBody.velocity.z);
        rBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        canJump = true;
    }
}
