using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private IInputService inputService;
    [Inject] private ICubeInteractor cubeInteractor;
    [Inject] private ZoneManager zoneManager;
    
    private CharacterController characterController;
    public float moveSpeed = 5f;
    public float interactionDistance = 2f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCubeInteraction();
        UpdateHeldCubePosition();
    }
    
    private void UpdateHeldCubePosition()
    {
        if (cubeInteractor.IsHoldingCube())
        {
            ICube heldCube = cubeInteractor.GetHeldCube();
            Vector3 targetPosition = transform.position + transform.forward * 1.5f + Vector3.up;
            heldCube.Transform.position = Vector3.Lerp(heldCube.Transform.position, targetPosition, Time.deltaTime * 10f);
        }
    }

    private void HandleMovement()
    {
        Vector2 input = inputService.GetMovementInput();
        Vector3 movement = new Vector3(input.x, 0, input.y) * moveSpeed * Time.deltaTime;
        characterController.Move(movement);
    }

    private void HandleCubeInteraction()
    {
        if (inputService.IsPickupDropPressed())
        {
            if (cubeInteractor.IsHoldingCube())
            {
                TryPlaceCube();
            }
            else
            {
                TryPickupCube();
            }
        }
    }

    private void TryPickupCube()
    {
        ICube nearestCube = FindNearestCube();
        if (nearestCube != null)
        {
            cubeInteractor.PickupCube(nearestCube);
            
            zoneManager.RemoveCubeFromThirdZone(nearestCube);
        }
    }

    private void TryPlaceCube()
    {
        Vector3 playerPosition = transform.position;
        int row = Mathf.RoundToInt(playerPosition.x - zoneManager.GetSecondZonePosition(0, 0).x);
        int column = Mathf.RoundToInt(playerPosition.z - zoneManager.GetSecondZonePosition(0, 0).z);

        if (row >= 0 && row < 3 && column >= 0 && column < 3)
        {
            ICube heldCube = cubeInteractor.GetHeldCube();
            if (zoneManager.PlaceCubeInSecondZone(heldCube, row, column))
            {
                Vector3 placePosition = zoneManager.GetSecondZonePosition(row, column);
                heldCube.Transform.position = placePosition;
                cubeInteractor.DropCube();
            }
        }
    }

    private ICube FindNearestCube()
    {
        List<ICube> thirdZoneCubes = zoneManager.GetThirdZoneCubes();
        ICube nearestCube = null;
        float nearestDistance = float.MaxValue;

        foreach (ICube cube in thirdZoneCubes)
        {
            float distance = Vector3.Distance(transform.position, cube.Transform.position);
            if (distance < nearestDistance && distance <= interactionDistance)
            {
                nearestCube = cube;
                nearestDistance = distance;
            }
        }

        return nearestCube;
    }
}