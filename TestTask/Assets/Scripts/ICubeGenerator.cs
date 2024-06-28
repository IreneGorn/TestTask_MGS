using System.Collections.Generic;
using UnityEngine;

public interface ICubeGenerator
{
    ICube[,] GenerateRandomCubeSet(Vector3 startPosition, int rows, int columns, int numCubes);
    List<ICube> GenerateLooseCubes(Vector3 startPosition, int count);
}