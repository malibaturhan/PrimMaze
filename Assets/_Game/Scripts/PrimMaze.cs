using System.Collections.Generic;
using UnityEngine;

public class PrimMaze : Maze
{
    public override void Generate()
    {
        int startX = Random.Range(1, width - 1);
        int startZ = Random.Range(1, depth - 1);
        map[startZ, startX] = 0;

        HashSet<Vector2Int> frontier = new HashSet<Vector2Int>();
        AddFrontier(startX, startZ, frontier);

        while (frontier.Count > 0)
        {
            Debug.Log("Generating");
            Vector2Int cell = GetRandomFromSet(frontier);
            frontier.Remove(cell);

            int x = cell.x;
            int z = cell.y;

            if (x <= 0 || z <= 0 || x >= width - 1 || z >= depth - 1)
            {
                continue;
            }

            if (CountSquareNeighbours(x, z) == 1)
            {
                map[z, x] = 0;
                AddFrontier(x, z, frontier);
            }
        }
    }

    private void AddFrontier(int x, int z, HashSet<Vector2Int> frontier)
    {
        frontier.Add(new Vector2Int(x + 1, z));
        frontier.Add(new Vector2Int(x - 1, z));
        frontier.Add(new Vector2Int(x, z + 1));
        frontier.Add(new Vector2Int(x, z - 1)); 
    }

    private Vector2Int GetRandomFromSet(HashSet<Vector2Int> set)
    {
        int index = Random.Range(0,set.Count);
        foreach (var item in set)
        {
            if (index-- == 0)
            {
                return item;
            }
        }

        return default;
    }
    
}