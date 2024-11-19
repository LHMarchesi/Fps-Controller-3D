using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey : MonoBehaviour, Ipickuppeable
{
    private PlayerController playerController;
    private Rigidbody rb;

    public string Name => throw new System.NotImplementedException();

    public Sprite ItemIcon => throw new System.NotImplementedException();

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(PlayerController player)
    {
        transform.SetParent(player.playerHand, true);
        transform.position = player.playerHand.position;
        transform.rotation = Quaternion.identity;

        rb.isKinematic = true;

        player.currentItem = this;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        this.transform.SetParent(null);
        playerController.currentItem = null;
    }

}
