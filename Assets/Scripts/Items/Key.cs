using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour, Ipickuppeable
{
    [SerializeField] private string gateTag = "";
    PlayerController playerController;
    Rigidbody keyRb;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        keyRb = GetComponent<Rigidbody>(); 
    }
    public void PickUp(PlayerController playerController)
    {
        this.playerController = playerController;

        transform.SetParent(playerController.playerHand, true);
        transform.position = playerController.playerHand.position;
        transform.rotation = Quaternion.identity;
        keyRb.isKinematic = true;

        playerController.currentItem = this;
    }

    public void Drop()
    {
        if (playerController.currentItem != null)
        {
            transform.SetParent(null);
            keyRb.isKinematic = false;
            keyRb.detectCollisions = true;
            playerController.currentItem = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(gateTag))
        {
            playerController.currentItem = null;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
