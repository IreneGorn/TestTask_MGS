using UnityEngine;

public interface ICube
{
    Transform Transform { get; }
    void Pickup(Transform holder);
    void Drop();
}