using System.Collections.Generic;
using UnityEngine;

public interface ICubeGenerator
{
    ICube[,] GenerateRandomCubeSet(List<Transform> quads, int numCubes);
    List<ICube> GenerateLooseCubes(Vector3 startPosition, int count);
}