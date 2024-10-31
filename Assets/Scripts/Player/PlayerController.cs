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
    public Transform playerHand;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Camera playerCamera;
    public Iweapon currentWeapon;
    public Ipickuppeable currentItem;
    private Vector3 velocity;
    private bool isGrounded;
    private float pickUpRange = 3.5f;


    private void Awake()
    {
        currentWeapon = null;
        currentItem = null;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon?.Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            currentWeapon?.Aim();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentItem == null)
            {
                TryPickUpItem();
            }
            else
            {
                currentItem.Drop();
            }
        }

    }

    private void HandleMovement()
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

    private void TryPickUpItem()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // Raycast desde el centro de la pantalla
        RaycastHit hit;

        // Dibujar el rayo en la escena para visualizarlo (color verde, durará 0.1 segundos)
        Debug.DrawRay(ray.origin, ray.direction * pickUpRange, Color.green, 0.1f);

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            Ipickuppeable pickableItem = hit.transform.GetComponent<Ipickuppeable>();
            if (pickableItem != null)
            {
                pickableItem.PickUp(this);
                currentItem = pickableItem;
            }
        }
    }
}

