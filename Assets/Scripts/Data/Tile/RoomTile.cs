using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Room Tile")]
public class RoomTile : RandomTile
{
    [SerializeField]
    GameObject _wallPrefab;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        if (tilemap.GetTile(position) == this)
        {
            tilemap.RefreshTile(position);
        }

        var pos = position + Vector3Int.up;
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }

        pos = position + Vector3Int.right;
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }

        pos = position + Vector3Int.down;
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }

        pos = position + Vector3Int.left;
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }
    }

    //public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    //{
    //    var pos = tileData.transform.GetPosition(); ;
    //    if (tilemap.GetTile(position + Vector3Int.up) != this)
    //    {
    //        SpawnWall(pos);
    //    }

    //    base.GetTileData(position, tilemap, ref tileData);
    //}

    //void SpawnWall(Vector3 position)
    //{
    //    var wall = Instantiate(_wallPrefab);
    //    wall.transform.position = position;
    //}
}
