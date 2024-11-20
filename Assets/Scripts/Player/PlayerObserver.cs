using System;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnGetDamage += DamageEffect;
        PlayerHealth.OnDead += PlayerDead;
    }

    private void PlayerDead()
    {
        LosePanel.SetActive(true);
        playerController.CanMove(false);
    }

    private void DamageEffect()
    {
        Debug.Log("Damage taken");
    }
}
