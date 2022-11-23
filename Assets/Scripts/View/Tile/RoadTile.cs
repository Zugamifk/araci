using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Tiles/Road Tile")]
public class RoadTile : Tile
{
    [Flags]
    enum SideMask
    {
        Top = 8,
        Right = 1,
        Bottom = 2,
        Left = 4
    }

    [SerializeField]
    Sprite[] _sprites;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        if(tilemap.GetTile(position) == this)
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

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        SideMask mask = 0;
        if(tilemap.GetTile(position + Vector3Int.up) == this)
        {
            mask |= SideMask.Top;
        } 

        if(tilemap.GetTile(position + Vector3Int.right) == this)
        {
            mask |= SideMask.Right;
        }

        if(tilemap.GetTile(position + Vector3Int.down) == this)
        {
            mask |= SideMask.Bottom;
        }

        if(tilemap.GetTile(position + Vector3Int.left) == this)
        {
            mask |= SideMask.Left;
        }

        var index = (uint)mask;
        if (index > 0 && index <= _sprites.Length)
        {
            tileData.sprite = _sprites[index-1];
        } else
        {
            tileData.sprite = _sprites[0];
        }
    }
}
