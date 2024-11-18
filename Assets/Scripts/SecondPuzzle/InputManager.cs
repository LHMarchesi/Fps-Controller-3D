using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameController gameController;

    public LineDrawer lineDrawer;

    private Ball selectedBall = null;

    private bool isConnecting = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectBall();
        }
    }

    void SelectBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Ball clickedBall = hit.transform.GetComponent<Ball>();

            if (clickedBall != null)
            {
                if (clickedBall.isConnected)
                {
                    Debug.Log("This ball is already connected.");
                    return;  // No permitir la selección si ya está conectada
                }

                if (selectedBall == null)
                {
                    // Primer clic: seleccionar una bola
                    selectedBall = clickedBall;
                    return;
                }
                else
                {
                    // Segundo clic: intentar conectar las bolas
                    if (selectedBall.color == clickedBall.color)
                    {
                        // Si las bolas son del mismo color y no están conectadas
                        lineDrawer.DrawLine(selectedBall, clickedBall);

                        // Marcar ambas bolas como conectadas
                        selectedBall.isConnected = true;
                        clickedBall.isConnected = true;

                        selectedBall = null;

                        // Evitar nuevas conexiones hasta que el jugador seleccione un nuevo par
                        isConnecting = false;

                    }
                    else
                    {
                        Debug.Log("Balls with different colors cannot be connected.");

                        selectedBall = null;
                    }
                }
            }
        }
    }
}



