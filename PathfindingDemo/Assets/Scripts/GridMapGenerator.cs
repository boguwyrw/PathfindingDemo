using UnityEngine;

public class GridMapGenerator : MonoBehaviour
{
    [SerializeField] private SingleTile[] singleTiles;

    private void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("map_config");

        if (jsonFile == null)
        {
            Debug.LogError("Nie znaleziono pliku map_config.json w folderze Resources!");
            return;
        }

        MapConfig data = JsonUtility.FromJson<MapConfig>(jsonFile.text);

        GridMap(data);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(GetTilePosition(hit.point));
        }
    }
    
    public void GridMap(MapConfig data)
    {
        int width = 0;
        int.TryParse(data.Width, out width);
        
        int height = 0;
        int.TryParse(data.Height, out height);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (data.Map[x][z] == '0')
                {
                    TileSpawn(x, z, 0);
                }
                else if (data.Map[x][z] == '1')
                {
                    TileSpawn(x, z, 1);
                }
                else if (data.Map[x][z] == '2')
                {
                    TileSpawn(x, z, 2);
                }
            }
        }
    }

    private void TileSpawn(int x, int z, int tileNo)
    {
        Transform tileTransform = Instantiate(singleTiles[tileNo].TilePrefab, TileWorldPosition(x, z), Quaternion.identity);
        Tile tile = tileTransform.GetComponent<Tile>();
        tile.SetColor(singleTiles[tileNo].TileColor);
    }

    public Vector3 TileWorldPosition(int x, int z)
    {
        return new Vector3(x, 0.0f, z);
    }

    public TilePosition GetTilePosition(Vector3 worldPosition)
    {
        return new TilePosition(
            Mathf.RoundToInt(worldPosition.x),
            Mathf.RoundToInt(worldPosition.z)
        );
    }
}
