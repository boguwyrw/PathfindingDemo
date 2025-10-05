using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int moveRange = 4;
    [SerializeField] private int attackRange = 3;

    private Vector2Int playerPosition;

    public Vector2Int PlayerPosition { get { return playerPosition; } }

    public List<Vector2Int> AvailableMoveTiles => AvailableTiles(moveRange);
    public List<Vector2Int> AvailableAttackTiles => AvailableTiles(attackRange);

    private void Start()
    {
        int posX = Mathf.RoundToInt(transform.position.x);
        int posZ = Mathf.RoundToInt(transform.position.z);
        playerPosition = new Vector2Int(posX, posZ);
    }

    public List<Vector2Int> AvailableTiles(int range)
    {
        List<Vector2Int> availableTilesList = new List<Vector2Int>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                Vector2Int availableTileCoordinates = new Vector2Int(x, z);
                Vector2Int availableCoordinates = playerPosition + availableTileCoordinates;
                availableTilesList.Add(availableCoordinates);
            }
        }

        return availableTilesList;
    }
}
