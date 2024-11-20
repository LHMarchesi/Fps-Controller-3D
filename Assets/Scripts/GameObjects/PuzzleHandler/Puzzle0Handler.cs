using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle0Handler : MonoBehaviour
{
    [SerializeField] private ItemPlatform[] itemPlatforms;
    [SerializeField] private GameObject npc;
    private bool allColliding;
   
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
            npc.SetActive(true);
        }
        else
        {
            npc.SetActive(false);
        }

    }
}
