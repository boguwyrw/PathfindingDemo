using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private bool isTileTraversable;

    private int tileX = -1;
    private int tileZ = -1;

    private int tileMarkerValue = 0;

    private Vector2Int tileCoordinates;

    public bool IsTileTraversable { get { return isTileTraversable; } }
    public int TileX { get { return tileX; } }
    public int TileZ { get { return tileZ; } }
    public int TileMarkerValue { get { return tileMarkerValue; } }
    public Vector2Int TileCoordinates { get { return tileCoordinates; } }

    public void SetColor(Color tileColor)
    {
        meshRenderer.material.color = tileColor;
    }

    public void SetTraversable(bool traversable)
    {
        isTileTraversable = traversable;
    }

    public void SetTilePosition()
    {
        tileX = Mathf.RoundToInt(transform.position.x);
        tileZ = Mathf.RoundToInt(transform.position.z);
        tileCoordinates = new Vector2Int(tileX, tileZ);
    }

    public void SetTileValue(int tileValue)
    {
        tileMarkerValue = tileValue;
    }
}
