using UnityEngine;

public class Cube : MonoBehaviour, ICube
{
    public Transform Transform => transform;

    private Vector3 originalPosition;
    private bool isPickedUp = false;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    public void Pickup(Transform holder)
    {
        isPickedUp = true;
        // Можно добавить визуальный эффект при подборе куба
    }

    public void Drop()
    {
        isPickedUp = false;
        transform.position = originalPosition;
        // Можно добавить визуальный эффект при падении куба
    }

    private void Update()
    {
        if (isPickedUp)
        {
            // Если куб подобран, он следует за игроком
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
        }
    }
}