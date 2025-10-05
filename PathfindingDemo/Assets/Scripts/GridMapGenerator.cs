using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GridMapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private SingleTile[] singleTiles;

    private GameObject playerGO;

    private int[,] grid;

    private int width = 0;
    private int height = 0;

    private Dictionary<Vector2Int, Tile> tilesDictionary = new Dictionary<Vector2Int, Tile>();

    private Vector2Int start;
    private Vector2Int goal;

    private bool isMarkAsStart;
    private bool isMarkAsGoal;

    private void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("map_config");

        if (jsonFile == null)
        {
            Debug.LogError("map_config.json file not found in Resources folder!");
            return;
        }

        MapConfig data = JsonUtility.FromJson<MapConfig>(jsonFile.text);

        int.TryParse(data.Width, out width);     
        int.TryParse(data.Height, out height);

        grid = new int[width, height];

        GridMap(data);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Tile tile = hit.transform.GetComponent<Tile>();
                if (tile.IsTileTraversable && !isMarkAsStart)
                {
                    tile.SetColor(Color.cyan);
                    isMarkAsStart = true;
                    start = new Vector2Int(tile.TileX, tile.TileZ);
                    playerGO = Instantiate(player, new Vector3(tile.TileX, 0.5f, tile.TileZ), Quaternion.identity);
                }
                else if (tile.IsTileTraversable && isMarkAsStart && !isMarkAsGoal)
                {
                    tile.SetColor(Color.grey);
                    isMarkAsGoal = true;
                    goal = new Vector2Int(tile.TileX, tile.TileZ);
                    Player playerScript = playerGO.GetComponent<Player>();
                    bool outOfRange = playerScript.AvailableMoveTiles.Contains(goal);
                    if (!outOfRange)
                        infoText.text = "out of range";
                }
            }
        }
    }

    public void GridMap(MapConfig data)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (data.Map[x][z] == '0')
                {
                    TileSpawn(x, z, 0, 0);
                    grid[x, z] = 0;
                }
                else if (data.Map[x][z] == '1')
                {
                    TileSpawn(x, z, 1, -1);
                    grid[x, z] = 1;
                }
                else if (data.Map[x][z] == '2')
                {
                    TileSpawn(x, z, 2, -1);
                    grid[x, z] = 1;
                }
            }
        }
    }

    public void FindPathButton()
    {
        AlgorithmBFS algorithmBFS = new AlgorithmBFS();
        List<Vector2Int> path = algorithmBFS.FindPath(grid, start, goal);

        if (path != null)
        {
            foreach (var p in path)
            {
                Tile selectedTile = tilesDictionary.Single(s => s.Key == p).Value;
                selectedTile.SetColor(Color.blue);
            }
        }
        else
        {
            Debug.Log("No path!");
        }
    }

    private void TileSpawn(int x, int z, int tileNo, int tileValue)
    {
        Transform tileTransform = Instantiate(singleTiles[tileNo].TilePrefab, TileWorldPosition(x, z), Quaternion.identity);
        Tile tile = tileTransform.GetComponent<Tile>();

        tile.SetColor(singleTiles[tileNo].TileColor);
        tile.SetTraversable(singleTiles[tileNo].IsTraversable);
        tile.SetTilePosition();
        tile.SetTileValue(tileValue);

        Vector2Int tileCoordinates = new Vector2Int(tile.TileX, tile.TileZ);
        tilesDictionary.Add(tileCoordinates, tile);
    }

    public Vector3 TileWorldPosition(int x, int z)
    {
        return new Vector3(x, 0.0f, z);
    }
}
