using UnityEngine;

public class CubeInteractor : ICubeInteractor
{
    private ICube heldCube;

    public bool IsHoldingCube() => heldCube != null;

    public void PickupCube(ICube cube)
    {
        if (!IsHoldingCube())
        {
            heldCube = cube;
            cube.Pickup(null);
        }
    }

    public void DropCube()
    {
        if (IsHoldingCube())
        {
            heldCube.Drop();
            heldCube = null;
        }
    }

    public ICube GetHeldCube() => heldCube;
}