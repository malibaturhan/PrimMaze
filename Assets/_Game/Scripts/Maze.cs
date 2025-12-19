using UnityEngine;

public abstract class Maze : MonoBehaviour
{
    [SerializeField] internal int width;
    [SerializeField] internal int depth;

    [SerializeField] internal int scale = 6;

    // map[row(z), column(x)]
    internal int[,] map;

    void Start()
    {
        Initialize();
        Generate();
        Build();
    }

    public virtual void Initialize()
    {
        map = new int[depth, width];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                map[z, x] = 1;
            }
        }
    }

    #region Sample Walker

    private void WalkVertical(int x, int z)
    {
        map[z, x] = 0;
        bool goUp = Random.Range(0, 2) == 0;
        bool done = false;
        while (!done)
        {
            int goLeftRightMiddle = Random.Range(-1, 2);
            int nextX = goLeftRightMiddle + x;
            int nextZ = z + (goUp ? 1 : -1);
            map[nextZ, nextX] = 0;
            x = nextX;
            z = nextZ;
            done |= x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1;
            Debug.Log("Walking Vertical");
        }
    }

    private void WalkHorizontal(int x, int z)
    {
        map[z, x] = 0;
        bool goRight = Random.Range(0, 2) == 0;
        bool done = false;
        while (!done)
        {
            int goUpDownMiddle = Random.Range(-1, 2);
            int nextZ = goUpDownMiddle + z;
            int nextX = x + (goRight ? 1 : -1);
            map[nextZ, nextX] = 0;
            x = nextX;
            z = nextZ;
            done |= x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1;
            Debug.Log("Walking horizontal");
        }
    }

    #endregion

    public virtual void Generate()
    {
        // Random Walker
        var startX = Random.Range(1, width - 1);
        var startZ = Random.Range(1, depth - 1);
        for (int i = 0; i < Mathf.Ceil(width / 10); i++)
        {
            for (int j = 0; j < Mathf.Ceil(depth / 10); j++)
            {
                WalkHorizontal(startX, startZ);
                WalkVertical(startX, startZ);
            }
        }
    }

    public virtual void Build()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[z, x] == 1)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x * scale, 0, z * scale);
                    cube.transform.localScale = new Vector3(scale, scale, scale);
                }
            }
        }
    }


    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;

        if (map[z, x + 1] == 0) count++;
        if (map[z, x - 1] == 0) count++;
        if (map[z + 1, x] == 0) count++;
        if (map[z - 1, x] == 0) count++;

        return count;
    }

    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;

        if (map[z - 1, x - 1] == 0) count++;
        if (map[z - 1, x + 1] == 0) count++;
        if (map[z + 1, x - 1] == 0) count++;
        if (map[z + 1, x + 1] == 0) count++;

        return count;
    }

    public int CountAllNeighbours(int x, int z)
    {
        return CountDiagonalNeighbours(x, z) + CountSquareNeighbours(x, z);
    }
}