#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -10f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.4f;
    public GameObject playerHand;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Iweapon currentWeapon;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        currentWeapon = null;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon?.Attack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            currentWeapon?.DropWeapon();
        }

    }

    private void PlayerMovement()
    {
        float x;
        float z;
        bool jumpPressed = false;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jumpPressed = Input.GetButtonDown("Jump");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void ChangeWeapon(Iweapon weapon)
    {
        this.currentWeapon = weapon;
    }
}
