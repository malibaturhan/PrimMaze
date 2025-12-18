using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int depth;
    [SerializeField] private int scale = 6;
    private byte[,] map;

    void Start()
    {
        Initialize();
        Generate();
        Build();
    }

    public virtual void Initialize()
    {
        map = new byte[depth, width];
        for (byte z = 0; z < depth; z++)
        {
            for (byte x = 0; x < width; x++)
            {
                map[z, x] = 1;
            }
        }
    }

    #region Sample Walker

    private void WalkVertical(byte x, byte z)
    {
           
    }

    #endregion

    public virtual void Generate()
    {
        // Random Walker
        
    }

    public virtual void Build()
    {
        for (byte z = 0; z < depth; z++)
        {
            for (byte x = 0; x < width; x++)
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


    public int CountSquareNeighbours(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public int CountDiagonalNeighbours(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public int CountAllNeighbours(int x, int y)
    {
        throw new System.NotImplementedException();
    }
}