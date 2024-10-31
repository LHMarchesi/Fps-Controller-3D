using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ipickuppeable 
{
    void PickUp(PlayerController player);
    void Drop();
}
