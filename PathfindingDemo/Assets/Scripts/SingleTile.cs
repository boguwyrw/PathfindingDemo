using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "ScriptableObjects/SingleTile", order = 1)]
public class SingleTile : ScriptableObject
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Color tileColor;
    [SerializeField] private bool isTraversable;

    public Transform TilePrefab { get { return tilePrefab; } }
    public Color TileColor { get { return tileColor; } }
    public bool IsTraversable { get { return isTraversable; } }
}
