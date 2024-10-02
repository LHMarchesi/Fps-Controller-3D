using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, Iweapon
{
    [SerializeField] private int TotalAmmo;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Transform bulletPosition;

    private Stack<GameObject> bullets = new Stack<GameObject>();
    PlayerController player;

    private void Start()
    {
        for (int i = 0; i < TotalAmmo; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, this.transform);
            bullets.Push(obj);
            obj.SetActive(false);
        }
    }
    public void Attack()
    {
        if (bullets.Count > 0)
        {
            GameObject obj = bullets.Pop();
            obj.transform.position = bulletPosition.position;
            obj.SetActive(true);
            obj.transform.SetParent(null);
        }
    }

    public void DropWeapon()
    {
        this.transform.SetParent(null);
        player.currentWeapon = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            this.transform.SetParent(player.playerHand.transform, true);
            this.transform.position = player.playerHand.transform.position;
            this.transform.rotation = Quaternion.identity;
            player.ChangeWeapon(this);
        }
    }
}
