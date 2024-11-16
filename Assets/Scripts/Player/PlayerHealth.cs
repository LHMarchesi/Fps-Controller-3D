using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnGetDamage;
    public static event Action OnDead;
    [SerializeField] private int health;

    public int Health { get => health; private set => health = value; }

    public void GetDamage()
    {
        OnGetDamage?.Invoke();
        health -= 1;
    }

    private void Update()
    {
        if (health <= 0)
        {
            OnDead?.Invoke();
        }
    }
}
