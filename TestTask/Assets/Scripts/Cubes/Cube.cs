using UnityEngine;

public class Cube : MonoBehaviour, ICube
{
    public Transform Transform => transform;

    private bool isPickedUp = false;
    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Pickup(Transform holder)
    {
        isPickedUp = true;
        rb.isKinematic = true;
        col.enabled = false;
        // Можно добавить визуальный эффект при подборе куба
    }

    public void Drop()
    {
        isPickedUp = false;
        rb.isKinematic = false;
        col.enabled = true;
        // Можно добавить визуальный эффект при падении куба
    }
}