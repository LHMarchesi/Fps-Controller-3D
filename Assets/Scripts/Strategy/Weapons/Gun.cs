using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, Iweapon, Ipickuppeable
{
    [SerializeField] private int TotalAmmo;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Transform bulletPosition;

    private Stack<GameObject> bullets = new Stack<GameObject>();
    private PlayerController player;

    private void Start()
    {
        for (int i = 0; i < TotalAmmo; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, this.transform);
            bullets.Push(obj);
            obj.SetActive(false);
        }
    }

    public void Shoot()
    {
        if (bullets.Count > 0)
        {
            GameObject obj = bullets.Pop();
            obj.transform.position = bulletPosition.position;
            obj.SetActive(true);
            obj.transform.SetParent(null);
        }
    }

    public void Aim()
    {
        throw new System.NotImplementedException();
    }

    public void PickUp(PlayerController player)
    {
        this.player = player;

        this.transform.SetParent(player.playerHand, true);
        this.transform.position = player.playerHand.position;
        this.transform.rotation = Quaternion.identity;

        player.ChangeWeapon(this);
        player.currentItem = this;
    }

    public void Drop()
    {
        if (player != null)
        {
            this.transform.SetParent(null);
            player.currentWeapon = null;
            player.currentItem = null;

            player = null; 
        }
    }
}
