using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : ICubeGenerator
{
    private GameObject cubePrefab;

    public CubeGenerator(GameObject cubePrefab)
    {
        this.cubePrefab = cubePrefab;
    }

    public ICube[,] GenerateRandomCubeSet(List<Transform> quads, int numCubes)
    {
        ICube[,] cubeSet = new ICube[3, 3];
        List<Transform> availableQuads = new List<Transform>(quads);

        for (int i = 0; i < numCubes && availableQuads.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableQuads.Count);
            Transform quad = availableQuads[randomIndex];
            availableQuads.RemoveAt(randomIndex);

            Vector3 position = quad.position + Vector3.up * 0.5f;
            GameObject cubeObject = Object.Instantiate(cubePrefab, position, Quaternion.identity);
            ICube cube = cubeObject.GetComponent<ICube>();

            int quadIndex = quads.IndexOf(quad);
            int row = quadIndex / 3;
            int column = quadIndex % 3;
            cubeSet[row, column] = cube;

            Rigidbody rb = cubeObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
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