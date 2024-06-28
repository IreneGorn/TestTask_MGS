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
            cube.Pickup(null); // Здесь null, так как мы не привязываем куб к трансформу
            cube.Transform.position += Vector3.up * 1.5f;
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