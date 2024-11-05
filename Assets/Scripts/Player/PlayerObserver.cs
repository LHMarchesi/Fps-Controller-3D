using System;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerHealth.OnGetDamage += DamageEffect;
        PlayerHealth.OnDead += PlayerDead;
    }

    private void PlayerDead()
    {
        Debug.Log("Player dead");

    }

    private void DamageEffect()
    {
        Debug.Log("Damage taken");
    }
}
