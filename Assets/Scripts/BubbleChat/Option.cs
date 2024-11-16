using System;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public event Action OnClick;
    Button buttonComponent;
    private void Start()
    {
        buttonComponent = GetComponentInChildren<Button>();
        buttonComponent.onClick.AddListener(() => Press());
        
    }
    public void Press()
    {
        OnClick?.Invoke();  // Invoca el evento cuando el botón es presionado
    }
}
