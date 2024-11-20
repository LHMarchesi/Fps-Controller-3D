using UnityEngine;
using UnityEngine.UI;

public class Sprinting : MonoBehaviour
{
    [SerializeField] private float runningSpeed;
    [SerializeField] private Slider staminaSlider;

    PlayerController playerController;
    private float maxStamina = 7;
    private float minStaminaToRun = 3;
    private float staminaDepletionRate = 2f; // Tasa de consumo de stamina al correr
    private float staminaRecoveryRate = .8f; // Tasa de recuperación de stamina cuando no se corre
    private float currentStamina;
    private bool isSprinting;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
    }

    public void Update()
    {
        HandleSprinting();
        RecoverStamina();
        staminaSlider.value = currentStamina;
    }

    private void HandleSprinting()
    {
        // Activar el sprint si se presiona la tecla y hay suficiente stamina
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            if (currentStamina >= minStaminaToRun)
            {
                playerController.ChangeSpeed(runningSpeed);
            }

            currentStamina -= staminaDepletionRate * Time.deltaTime; // Consumir stamina
            isSprinting = true;
        }
        else
        {
            // Volver a la velocidad normal si no se está corriendo o se agotó la stamina
            playerController.ChangeSpeed(playerController.Speed);
            isSprinting = false;
        }

        // Limitar la stamina al rango [0, maxStamina]
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    private void RecoverStamina()
    {
        // Recuperar stamina cuando no se está corriendo
        if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
        }
    }
}
