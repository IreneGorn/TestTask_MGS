using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : ICubeGenerator
{
    private GameObject cubePrefab;

    public CubeGenerator(GameObject cubePrefab)
    {
        this.cubePrefab = cubePrefab;
    }

    public ICube[,] GenerateRandomCubeSet(Vector3 startPosition, int rows, int columns)
    {
        ICube[,] cubeSet = new ICube[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = startPosition + new Vector3(i, 0.5f, j);
                GameObject cubeObject = Object.Instantiate(cubePrefab, position, Quaternion.identity);
                ICube cube = cubeObject.GetComponent<ICube>();
                cubeSet[i, j] = cube;
                
                // Делаем куб не интерактивным
                Rigidbody rb = cubeObject.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;
            }
        }
        return cubeSet;
    }

    public List<ICube> GenerateLooseCubes(Vector3 startPosition, int count)
    {
        List<ICube> looseCubes = new List<ICube>();
        for (int i = 0; i < count; i++)
        {
            Vector3 position = startPosition + new Vector3(Random.Range(-2f, 2f), 0.5f, Random.Range(-2f, 2f));
            GameObject cubeObject = Object.Instantiate(cubePrefab, position, Quaternion.identity);
            ICube cube = cubeObject.GetComponent<ICube>();
            looseCubes.Add(cube);
        }
        return looseCubes;
    }
}