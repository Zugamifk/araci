using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground : MonoBehaviour
{
    [SerializeField]
    Tilemap m_Tilemap;
    [SerializeField]
    TileBase[] m_Tiles;
    [SerializeField]
    Vector2Int m_Dimensions;

    HashSet<Vector2Int> m_GeneratedChunks = new HashSet<Vector2Int>();

    private void Update()
    {
        var c = Camera.main.transform.position;
        var p = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        UpdateChunks(p.x, p.y);

        p = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        UpdateChunks(p.x, p.y);

        p = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        UpdateChunks(p.x, p.y);

        p = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        UpdateChunks(p.x, p.y);
    }

    void UpdateChunks(float x, float y)
    {
        var xi = Mathf.FloorToInt(x/m_Dimensions.x) * m_Dimensions.x;
        var yi = Mathf.FloorToInt(y/m_Dimensions.y) * m_Dimensions.y;
        var p = new Vector2Int(xi, yi);
        if(!m_GeneratedChunks.Contains(p))
        {
            AddChunk(xi, yi);
        }

    }

    void AddChunk(int x, int y)
    {
        var b = new BoundsInt(x, y, 0, m_Dimensions.x, m_Dimensions.y, 1);
        var n = m_Dimensions.x * m_Dimensions.y;
        var t = new TileBase[n];
        for (int i = 0; i < n; i++)
        {
            t[i] = m_Tiles[Random.Range(0, m_Tiles.Length)];
        }
        m_Tilemap.SetTilesBlock(b, t);
        m_GeneratedChunks.Add(new Vector2Int(x, y));
    }
}
