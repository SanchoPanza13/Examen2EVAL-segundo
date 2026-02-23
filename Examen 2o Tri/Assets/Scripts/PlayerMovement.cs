using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 5f, sprintSpeed = 10f, jumpForce = 8f;
    public float gravity = -9.81f, mouseSensitivity = 2f;
    public CharacterController controller; public Transform groundCheck;
    public LayerMask groundMask; public float groundDistance = 0.4f;
    public bool isMobile = false;
    Vector2 moveDir = Vector2.zero;
    bool isGrounded;
    float xRotation = 0f, yVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (!isMobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= my;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mx);

        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yVelocity = jumpForce;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Vector3 move = transform.right * moveDir.x + transform.forward * moveDir.y;

        float speed = (Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : walkSpeed;
        move *= speed;

        if (isGrounded && yVelocity < 0) yVelocity = -2f;
        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity;
        controller.Move(move * Time.deltaTime);
    }
}
