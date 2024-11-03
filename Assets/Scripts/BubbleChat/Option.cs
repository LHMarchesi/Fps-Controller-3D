using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour, IInteractable
{
    public UnityEvent onClick; 

    public void Interact()
    {
        onClick?.Invoke();
    }
}
