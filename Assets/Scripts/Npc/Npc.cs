using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] private TextAsset inkJSONAsset;
    [SerializeField] private bool HasReward;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnPoint;
    BubbleChatManager story;
    PlayerController player;
    bool chatLoaded;
    CursorLockMode previousCursorLockState;
    bool previousCursorVisible;

    public void Interact()
    {
        story = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<BubbleChatManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerController>();
        if (story != null && !chatLoaded)
        {
            // Guardar el estado del cursor
            previousCursorLockState = Cursor.lockState;
            previousCursorVisible = Cursor.visible;

            story.SetJsonFile(inkJSONAsset);
            story.LoadChat();
            chatLoaded = true;

            if (HasReward)
            {
                story.SetRewardItem(itemPrefab, spawnPoint);
            }

            Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor para la UI
            Cursor.visible = true; // Mostrar el cursor
        }
    }

    private void Update()
    {
        if (chatLoaded)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 5f)
            {
                Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor de nuevo cuando termine la interacción
                Cursor.visible = false;
                story.FinishStory();

                // Restaurar el estado original del cursor
                chatLoaded = false;
                Cursor.lockState = previousCursorLockState;
                Cursor.visible = previousCursorVisible;
            }
        }
    }
}
