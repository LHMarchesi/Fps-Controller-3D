using UnityEngine;

public class Item : MonoBehaviour, Ipickuppeable
{
    public string itemName;
    public Sprite itemIcon;
    private PlayerController playerController;
    private Rigidbody rb;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    public string Name => itemName;

    public Sprite ItemIcon => itemIcon;

    public void PickUp(PlayerController playerController)
    {
        playerController.playerInventory.AddItem(this);
    }

    public void Drop()
    {
        playerController.playerInventory.RemoveItem(this);
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }


    public void showItem(GameObject itemObj)
    {
        itemObj.SetActive(true);
        itemObj.transform.SetParent(playerController.playerHand, true);
        itemObj.transform.position = playerController.playerHand.position;
        itemObj.transform.rotation = Quaternion.identity;
        rb.isKinematic = true;
    }

    public void hide(GameObject itemObj)
    {
        itemObj.SetActive(false);
    }
}
