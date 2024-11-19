using UnityEngine;

public interface Ipickuppeable
{
    string Name { get; }
    Sprite ItemIcon { get; }
    void PickUp(PlayerController player);
    void Drop();
}
