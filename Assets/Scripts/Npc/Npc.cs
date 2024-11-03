using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    BubbleChatManager story;
    private void Awake()
    {
        story = GetComponentInChildren<BubbleChatManager>();
    }

    public void Interact()
    {
        story.LoadChat();
    }
}
