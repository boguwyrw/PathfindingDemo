public struct TilePosition
{
    public int TileX;
    public int TileZ;

    public TilePosition(int tileX, int tileZ)
    {
        TileX = tileX;
        TileZ = tileZ;
    }

    public override string ToString()
    {
        return $"X: {TileX}, Z: {TileZ}";
    }
}
