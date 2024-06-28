using UnityEngine;

public class InputService : IInputService
{
    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");  // A и D
        float vertical = Input.GetAxis("Vertical");      // W и S
        return new Vector2(horizontal, vertical).normalized;
    }

    public bool IsPickupDropPressed()
    {
        bool isPressed = Input.GetKeyDown(KeyCode.T);
        Debug.Log($"T key pressed: {isPressed}");
        return isPressed;
    }
}