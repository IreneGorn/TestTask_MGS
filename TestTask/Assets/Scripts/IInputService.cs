using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    Vector2 GetMovementInput();
    bool IsPickupDropPressed();
}
