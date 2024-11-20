using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle0Handler : MonoBehaviour
{
    [SerializeField] private ItemPlatform[] itemPlatforms;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform spawnPoint;
    private bool allColliding;
    private bool hasSpawned;
   
    // Update is called once per frame
    void Update()
    {
        foreach (ItemPlatform itemPlatform in itemPlatforms)
        {
            if (!itemPlatform.isColliding)
            {
                allColliding = false;
                break;
            }
            else
            {
                allColliding = true;    
            }
        }

        if (allColliding)
        {
            if (itemPrefab != null && spawnPoint != null)
            {
                if (!hasSpawned)
                {
                    Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
                    hasSpawned = true;
                }
            }
        }
    }
}
