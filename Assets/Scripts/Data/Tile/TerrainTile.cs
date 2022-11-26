using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Tiles/Terrain Tile")]
public class TerrainTile : Tile
{
    [Flags]
    enum Edge
    {
        North = 1,
        NorthEast = 2,
        East = 4,
        SouthEast = 8,
        South = 16,
        SouthWest = 32,
        West = 64,
        NorthWest = 128
    }

    [SerializeField]
    Sprite _filled;
    [SerializeField]
    Sprite _oneSide;
    [SerializeField]
    Sprite _sideCorner;
    [SerializeField]
    Sprite _middleCorner;
    [SerializeField]
    Sprite _threeSides;

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

        pos = position + new Vector3Int(1,1,0);
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }

        pos = position + new Vector3Int(-1, 1, 0);
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }

        pos = position + new Vector3Int(-1, -1, 0);
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }

        pos = position + new Vector3Int(1, -1, 0);
        if (tilemap.GetTile(pos) == this)
        {
            tilemap.RefreshTile(pos);
        }
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        bool northEastForceBorder = false;
        bool southEastForceBorder = false;
        bool southWestForceBorder = false;
        bool northWestForceBorder = false;
        Edge sideMask = 0;
        if (IsBlob(tilemap, position + new Vector3Int(0,1,0)))
        {
            sideMask |= Edge.North;
        } else
        {
            northEastForceBorder = true;
            northWestForceBorder = true;
        }

        if (IsBlob(tilemap, position + new Vector3Int(1, 0, 0)))
        {
            sideMask |= Edge.East;
        }
        else
        {
            northEastForceBorder = true;
            southEastForceBorder = true;
        }

        if (IsBlob(tilemap, position + new Vector3Int(0, -1, 0)))
        {
            sideMask |= Edge.South;
        }
        else
        {
            southEastForceBorder = true;
            southWestForceBorder = true;
        }

        if (IsBlob(tilemap, position + new Vector3Int(-1, 0, 0)))
        {
            sideMask |= Edge.West;
        }
        else
        {
            northWestForceBorder = true;
            southWestForceBorder = true;
        }

        if (!southWestForceBorder && IsBlob(tilemap, position + new Vector3Int(-1, -1, 0)))
        {
            sideMask |= Edge.SouthWest;
        }

        if (!southEastForceBorder && IsBlob(tilemap, position + new Vector3Int(1, -1, 0)))
        {
            sideMask |= Edge.SouthEast;
        }

        if (!northEastForceBorder && IsBlob(tilemap, position + new Vector3Int(1, 1, 0)))
        {
            sideMask |= Edge.NorthEast;
        }

        if (!northWestForceBorder && IsBlob(tilemap, position + new Vector3Int(-1, 1, 0)))
        {
            sideMask |= Edge.NorthWest;
        }

        Sprite sprite = null;
        var index = (uint)sideMask;
        Debug.Log(index);
        switch (index)
        {
            case 1:
            case 4:
            case 16:
            case 64:
                sprite = _threeSides;
                break;
            case 31:
            case 124:
            case 241:
            case 199:
                sprite = _oneSide;
                break;
            case 127:
            case 247:
                sprite = _sideCorner;
                break;
            case 223:
            case 253:
                sprite = _middleCorner;
                break;
            default:
                break;
        }

        tileData.sprite = sprite;
    }

    bool IsBlob(ITilemap tilemap, Vector3Int position)
    {
        var tile = tilemap.GetTile(position);
        return tile == null || tile == this;
    }
}
