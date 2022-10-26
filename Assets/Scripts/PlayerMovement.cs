using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float wallRaycastDistance = 1f;
    [SerializeField] private float edgeForce = 2f;
    [SerializeField] private float sphereRay = 10f;


    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] public Vector3 velocity;

    [SerializeField] bool crouching;
    [SerializeField] bool isSprint;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isClimbing;
    [SerializeField] bool isWRunning;



    void Update()
    {
        // Makes a sphere that checks for floor. If collision, true, if not, false
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Resets velocity, so gravity is normal
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveInput = transform.right * x + transform.forward * z; // Right includes right and left
        // Forward includes forward and backwards because "W" = 1 and "S" = -1.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(moveInput * sprintSpeed * Time.deltaTime);
            isSprint = true;
        }
        else
        {
            controller.Move(moveInput * walkSpeed * Time.deltaTime);
            isSprint = false;
        }

        if (Input.GetKey(KeyCode.C))
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }


        if (crouching)
        {
            controller.height = 1.5f;
            // transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        else
        {
            controller.height = 2f;
            //groundCheck.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            // transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }






        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Making player jump using spacebar
        }

        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, wallRaycastDistance))
            {
                //  if (hit.collider.GetComponent<Climbable>() != null)  //If it has script climable
                //  {
                //      StartCoroutine(Climb(hit.collider));
                //  }
            }



            //if (Physics.Raycast(transform.position, transform.right, out hit,  wallRaycastDistance) || Physics.Raycast(transform.position, -transform.right, out hit, wallRaycastDistance))
            //{
            //    if (hit.collider.GetComponent<WallRunnable>() != null)  //If it has script climable
            //    {
            //        Debug.Log("AWOIEJROAWIEJR");
            //        StartCoroutine(wallRun(hit.collider));
            //    }
            //}

        }



        velocity.y += gravity * Time.deltaTime; // Simulating gravity, more time in air = faster fall.

        controller.Move(velocity * Time.deltaTime);

    }

    private IEnumerator Climb(Collider climbableCollider)
    {
        isClimbing = true;
        while (Input.GetKey(KeyCode.Space))
        {

            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.forward * wallRaycastDistance, Color.green, 0.5f);
            if (Physics.Raycast(transform.position, transform.forward, out hit, wallRaycastDistance))
            {
                if (hit.collider == climbableCollider)
                {
                    controller.Move(new Vector3(0f, climbSpeed * Time.deltaTime, 0f));
                    velocity.y = -2f;
                    yield return null;
                }
                else
                {
                    controller.Move(new Vector3(0f, edgeForce * Time.deltaTime, 0f));
                    velocity.y = -2f;
                    break;
                }
            }
            else
            {
                controller.Move(new Vector3(0f, edgeForce * Time.deltaTime, 0f));
                velocity.y = -2f;
                break; //Stop climbing
            }
        }
        isClimbing = false;
    }
}
