public class CubeChecker : ICubeChecker
{
    public bool CheckCubeSet(ICube[,] playerSet, ICube[,] originalSet)
    {
        int rows = playerSet.GetLength(0);
        int columns = playerSet.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (playerSet[i, j] == null || originalSet[i, j] == null)
                {
                    return false;
                }

                if (!CompareCubes(playerSet[i, j], originalSet[i, j]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CompareCubes(ICube cube1, ICube cube2)
    {
        return cube1 == cube2;
    }
}