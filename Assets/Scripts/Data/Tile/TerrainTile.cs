using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tiles/Terrain Tile")]
public class TerrainTile : Tile
{
    static readonly Matrix4x4 _flipHorz = Matrix4x4.Rotate(Quaternion.AngleAxis(180, Vector3.right));
    static readonly Matrix4x4 _flipVert = Matrix4x4.Rotate(Quaternion.AngleAxis(180, Vector3.up));

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
    [SerializeField]
    Sprite _fourCorners;
    [SerializeField]
    Sprite _twoSidesSideCorner;
    [SerializeField]
    Sprite _twoSidesMiddleCorner;
    [SerializeField]
    Sprite _twoSidesAdjacentSide;
    [SerializeField]
    Sprite _twoSidesAdjacentMiddle;

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

        pos = position + new Vector3Int(1, 1, 0);
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
        if (IsBlob(tilemap, position + new Vector3Int(0, 1, 0)))
        {
            sideMask |= Edge.North;
        }
        else
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
        var matrix = Matrix4x4.identity;
        var index = (uint)sideMask;
        Debug.Log(index);
        switch (index)
        {
            // three sides
            case 4:
                matrix = _flipVert;
                goto case 1;
            case 16:
                matrix = _flipVert * _flipHorz;
                goto case 1;
            case 64:
                matrix = _flipHorz;
                goto case 1;
            case 1:
                sprite = _threeSides;
                break;
            // four corners
            case 85:
                sprite = _fourCorners;
                break;
            // one side
            case 31:
                matrix = _flipVert * _flipHorz;
                goto case 241;
            case 124:
                matrix = _flipVert;
                goto case 241;
            case 199:
                matrix = _flipHorz;
                goto case 241;
            case 241:
                sprite = _oneSide;
                break;
            // side corner
            case 127:
                matrix = _flipVert;
                goto case 247;
            case 247:
                sprite = _sideCorner;
                break;
            // middle corner
            case 223:
                matrix = _flipHorz;
                goto case 253;
            case 253:
                sprite = _middleCorner;
                break;
            // two side middle corner
            case 80:
                matrix = _flipHorz;
                goto case 5;
            case 5:
                sprite = _twoSidesMiddleCorner;
                break;
            // two sides side corner
            case 20:
                matrix = _flipVert;
                goto case 65;
            case 65:
                sprite = _twoSidesSideCorner;
                break;
            // two sides middle
            case 112:
                matrix = _flipHorz;
                goto case 7;
            case 7:
                sprite = _twoSidesAdjacentMiddle;
                break;
            // two sides side
            case 28:
                matrix = _flipVert;
                goto case 193;
            case 193:
                sprite = _twoSidesAdjacentSide;
                break;
            default:
                break;
        }

        tileData.sprite = sprite;
        tileData.transform = matrix;
        tileData.colliderType = ColliderType.Sprite;
        tileData.flags = TileFlags.LockTransform;
    }

    bool IsBlob(ITilemap tilemap, Vector3Int position)
    {
        var tile = tilemap.GetTile(position);
        return tile == null || tile == this;
    }
}