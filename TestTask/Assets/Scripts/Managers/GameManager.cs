using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private ZoneManager zoneManager;
    private ICubeChecker cubeChecker;

    [Inject]
    public void Construct(ZoneManager zoneManager, ICubeChecker cubeChecker)
    {
        this.zoneManager = zoneManager;
        this.cubeChecker = cubeChecker;
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        zoneManager.InitializeZones(3, 3);  // 3x3 сетка кубов
    }

    public void CheckPlayerSet()
    {
        ICube[,] originalSet = zoneManager.GetFirstZoneCubes();
        ICube[,] playerSet = zoneManager.GetSecondZoneCubes();
        bool isCorrect = cubeChecker.CheckCubeSet(playerSet, originalSet);
        
        if (isCorrect)
        {
            Debug.Log("Поздравляем! Вы правильно расставили кубы!");
        }
        else
        {
            Debug.Log("Расстановка кубов неверна. Попробуйте еще раз!");
        }
    }
}