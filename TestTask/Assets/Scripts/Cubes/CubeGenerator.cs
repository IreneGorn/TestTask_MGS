using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : ICubeGenerator
{
    private GameObject cubePrefab;

    public CubeGenerator(GameObject cubePrefab)
    {
        this.cubePrefab = cubePrefab;
    }

    public ICube[,] GenerateRandomCubeSet(Vector3 startPosition, int rows, int columns, int numCubes)
    {
        ICube[,] cubeSet = new ICube[rows, columns];
        List<Vector2Int> positions = GenerateRandomPositions(rows, columns, numCubes);

        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int pos = positions[i];
            Vector3 position = startPosition + new Vector3(pos.x, 0.5f, pos.y);
            GameObject cubeObject = Object.Instantiate(cubePrefab, position, Quaternion.identity);
            ICube cube = cubeObject.GetComponent<ICube>();
            cubeSet[pos.x, pos.y] = cube;
            
            Rigidbody rb = cubeObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }

        return cubeSet;
    }

    private List<Vector2Int> GenerateRandomPositions(int rows, int columns, int numPositions)
    {
        List<Vector2Int> positions = new List<Vector2Int>();
        List<Vector2Int> allPositions = new List<Vector2Int>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                allPositions.Add(new Vector2Int(i, j));
            }
        }

        int maxIndex = Mathf.Min(numPositions, allPositions.Count);
        for (int i = 0; i < maxIndex; i++)
        {
            int randomIndex = Random.Range(0, allPositions.Count);
            positions.Add(allPositions[randomIndex]);
            allPositions.RemoveAt(randomIndex);
        }

        return positions;
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