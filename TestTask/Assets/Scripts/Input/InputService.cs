using UnityEngine;

public class InputService : IInputService
{
    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");  
        float vertical = Input.GetAxis("Vertical");    
        return new Vector2(horizontal, vertical).normalized;
    }

    public bool IsPickupDropPressed()
    {
        bool isPressed = Input.GetKeyDown(KeyCode.T);
        return isPressed;
    }
}