using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ZoneManager : MonoBehaviour
{
    public Transform firstZoneTransform;
    public Transform secondZoneTransform;
    public Transform thirdZoneTransform;
    
    [SerializeField] private List<Transform> firstZoneQuads;
    [SerializeField] private List<Transform> secondZoneQuads;
    
    private ICubeGenerator cubeGenerator;
    private ICube[,] firstZoneCubes;
    private ICube[,] secondZoneCubes;
    private List<ICube> thirdZoneCubes;

    private int minFirstZoneCubes = 4; 
    private int maxFirstZoneCubes = 8;

    private float cellSize = 1f;
    
    [Inject]
    public void Construct(ICubeGenerator cubeGenerator)
    {
        this.cubeGenerator = cubeGenerator;
    }

    public void InitializeZones(int rows, int columns)
    {
        int numCubes = Random.Range(minFirstZoneCubes, maxFirstZoneCubes + 1);
        firstZoneCubes = cubeGenerator.GenerateRandomCubeSet(firstZoneQuads, numCubes);
        secondZoneCubes = new ICube[rows, columns];
        thirdZoneCubes = cubeGenerator.GenerateLooseCubes(thirdZoneTransform.position, rows * columns);
    }
    
    public void MagnetizeCubeToSecondZone(ICube cube)
    {
        Vector3 cubePosition = cube.Transform.position;
        Transform nearestQuad = FindNearestQuad(cubePosition, secondZoneQuads);
    
        if (nearestQuad != null)
        {
            int index = secondZoneQuads.IndexOf(nearestQuad);
            int row = index / 3;
            int column = index % 3;

            if (secondZoneCubes[row, column] == null)
            {
                Vector3 newPosition = nearestQuad.position + Vector3.up * 0.5f;
                cube.Transform.position = newPosition;

                cube.Transform.rotation = Quaternion.identity;

                Rigidbody rb = cube.Transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                secondZoneCubes[row, column] = cube;
            }
        }
    }

    private Transform FindNearestQuad(Vector3 position, List<Transform> quads)
    {
        return quads.OrderBy(q => Vector3.Distance(q.position, position)).FirstOrDefault();
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