using System.Collections.Generic;
using UnityEngine;

public class AlgorithmBFS
{
    private readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(0, 1),   // góra
        new Vector2Int(0, -1),  // dó³
        new Vector2Int(-1, 0),  // lewo
        new Vector2Int(1, 0)    // prawo
    };

    public List<Vector2Int> FindPath(int[,] grid, Vector2Int start, Vector2Int goal)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        bool[,] visited = new bool[rows, cols];
        Dictionary<Vector2Int, Vector2Int> parent = new Dictionary<Vector2Int, Vector2Int>();

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(start);
        visited[start.x, start.y] = true;
        parent[start] = start;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == goal)
                return ReconstructPath(parent, start, goal);

            foreach (var dir in directions)
            {
                Vector2Int neighbor = current + dir;

                if (IsInGrid(neighbor, rows, cols) &&
                    !visited[neighbor.x, neighbor.y] &&
                    grid[neighbor.x, neighbor.y] == 0)
                {
                    visited[neighbor.x, neighbor.y] = true;
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return null;
    }

    private bool IsInGrid(Vector2Int pos, int rows, int cols)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < rows && pos.y < cols;
    }

    private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> parent,
                                             Vector2Int start, Vector2Int goal)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = goal;

        while (current != start)
        {
            path.Add(current);
            current = parent[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}
