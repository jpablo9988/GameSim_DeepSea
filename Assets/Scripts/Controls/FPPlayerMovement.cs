using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField]
    public bool ControlsActive = true;

    [SerializeField]
    private MouseLook lookControls;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        if (ControlsActive)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(speed * Time.deltaTime * move);

            if (Input.GetButtonDown("Jump") & isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
    public void ActivateControls(bool value)
    {
        this.ControlsActive = value;
        lookControls.ActivateControls(value);
    }
}