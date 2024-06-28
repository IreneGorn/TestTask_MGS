using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZoneManager : MonoBehaviour
{
    public Transform firstZoneTransform;
    public Transform secondZoneTransform;
    public Transform thirdZoneTransform;
    
    private ICubeGenerator cubeGenerator;
    private ICube[,] firstZoneCubes;
    private ICube[,] secondZoneCubes;
    private List<ICube> thirdZoneCubes;

    private int minFirstZoneCubes = 4;  // Минимальное количество кубов в первой зоне
    private int maxFirstZoneCubes = 8; // Максимальное количество кубов в первой зоне

    [Inject]
    public void Construct(ICubeGenerator cubeGenerator)
    {
        this.cubeGenerator = cubeGenerator;
    }

    public void InitializeZones(int rows, int columns)
    {
        int numCubes = Random.Range(minFirstZoneCubes, maxFirstZoneCubes + 1); // Генерируем случайное количество кубов
        firstZoneCubes = cubeGenerator.GenerateRandomCubeSet(firstZoneTransform.position, rows, columns, numCubes);
        secondZoneCubes = new ICube[rows, columns];
        thirdZoneCubes = cubeGenerator.GenerateLooseCubes(thirdZoneTransform.position, rows * columns);
    }
    
    public Vector3 GetSecondZonePosition(int row, int column)
    {
        return secondZoneTransform.position + new Vector3(row, 0, column);
    }

    public bool PlaceCubeInSecondZone(ICube cube, int row, int column)
    {
        if (secondZoneCubes[row, column] == null)
        {
            secondZoneCubes[row, column] = cube;
            return true;
        }
        return false;
    }

    public ICube RemoveCubeFromSecondZone(int row, int column)
    {
        ICube cube = secondZoneCubes[row, column];
        secondZoneCubes[row, column] = null;
        return cube;
    }

    public ICube[,] GetFirstZoneCubes()
    {
        return firstZoneCubes;
    }
    
    public ICube[,] GetSecondZoneCubes()
    {
        return secondZoneCubes;
    }

    public List<ICube> GetThirdZoneCubes()
    {
        return thirdZoneCubes;
    }
    
    public void RemoveCubeFromThirdZone(ICube cube)
    {
        thirdZoneCubes.Remove(cube);
    }
}