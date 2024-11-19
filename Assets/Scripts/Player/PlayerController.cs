using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerHand;
    public Ipickuppeable currentItem;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -10f;

    public PlayerInventory playerInventory;
    private CharacterController controller;
    private Camera playerCamera;
    private Vector3 velocity;
    private bool isGrounded;
    private float pickUpRange = 3.5f;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInventory = GetComponent<PlayerInventory>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentItem = playerInventory.GetSelectedItem();
            if (currentItem != null)
            {
                currentItem.Drop();
            }
        }

    }

    private void HandleMovement()
    {
        float x;
        float z;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // applys gravity force
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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

            IInteractable interactableObj = hit.transform.GetComponent<IInteractable>();
            if (interactableObj != null)
            {
                interactableObj.Interact();
            }
        }
    }
}

