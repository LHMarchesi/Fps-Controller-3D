using UnityEngine;
using UnityEngine.EventSystems; // Necesario para comprobar si el cursor está sobre la UI

public class LookWithMouse : MonoBehaviour
{
    const float k_MouseSensitivityMultiplier = 0.01f;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool unlockPressed, lockPressed;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * k_MouseSensitivityMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * k_MouseSensitivityMultiplier;

        unlockPressed = Input.GetKeyDown(KeyCode.Escape);
        lockPressed = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);

        // Si el cursor está sobre la UI, no bloquearlo
        if (EventSystem.current.IsPointerOverGameObject()) // Comprobar si el cursor está sobre un objeto de la UI
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && !unlockPressed)
        {
            // Solo bloquear el cursor cuando no está sobre la UI
            if (lockPressed)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

                playerBody.Rotate(Vector3.up * mouseX);
            }
        }

        // Manejando desbloqueo con Escape
        if (unlockPressed)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
