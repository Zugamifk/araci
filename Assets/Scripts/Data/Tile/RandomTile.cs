using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Tiles/Random Tile")]
public class RandomTile : Tile
{
    [SerializeField]
    Sprite[] _sprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        var i = Random.Range(0, _sprites.Length);
        tileData.sprite = _sprites[i];
    }
}
