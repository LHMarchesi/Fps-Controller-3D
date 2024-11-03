using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    BasicInkExample story;
    private void Awake()
    {
        story = GetComponentInChildren<BasicInkExample>();
    }

    public void Interact()
    {
        story.LoadChat();
    }
}
