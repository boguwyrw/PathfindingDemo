using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetColor(Color tileColor)
    {
        meshRenderer.material.color = tileColor;
    }
}
