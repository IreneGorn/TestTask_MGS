public interface ICubeInteractor
{
    bool IsHoldingCube();
    void PickupCube(ICube cube);
    void DropCube();
    ICube GetHeldCube();
}