using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 0.7f;
    public float moveSpeed = 0.003f;
    public float jumpForce = 8.0f;
    public float turnAmount;
    public bool isGrounded;

    Vector3 jump;
    Vector3 forward;
    Vector3 backward;

    public cameraMovement mouseXUpdate;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 20.0f, 0.0f);
        forward = new Vector3(0.0f, 0f, 1.0f);
        backward = new Vector3(0.0f, 0f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        movement();
        
    }

    void movement()
    {
        // walk/run forward
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(forward * moveSpeed);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(forward * (moveSpeed * 6));
            }
            else {
                transform.Translate(forward * (moveSpeed * 3));
            }
        }

        // walk backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(backward * (moveSpeed * 2));
        }

        // turn right
        if ((Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.eulerAngles -= turnSpeed * new Vector3(x: 0, y: turnSpeed, z: 0);
            }
            else
            {
                transform.eulerAngles += turnSpeed * new Vector3(x: 0, y: turnSpeed, z: 0);
                turnAmount = transform.eulerAngles.y;

            }
        }

        // turn left
        if ((Input.GetKey(KeyCode.A)))
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.eulerAngles += turnSpeed * new Vector3(x: 0, y: turnSpeed, z: 0);
            }
            else
            {
                transform.eulerAngles -= turnSpeed * new Vector3(x: 0, y: turnSpeed, z: 0);
                turnAmount = transform.eulerAngles.y;
            }
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            updateTurnAmount();
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    void updateTurnAmount()
    {
        turnAmount = mouseXUpdate.mouseX;
    }

    //bool isGrounded()
    //{
    //    if (transform.position.y < .5)
    //    {
    //        return true;
    //    }

    //    else
    //    {
    //        return false;
    //    }
    //}

    //Rigidbody movement
    //rb.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.Impulse)

    //if (rb.velocity.magnitude > maxSpeed)
    //{
    //    rb.velocity = rb.velocity.normalized * maxSpeed;
    //}
}

