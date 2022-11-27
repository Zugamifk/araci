using PlasticPipe.PlasticProtocol.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Tiles/Blob Tile")]
public class BlobTile : Tile
{
    static readonly Matrix4x4 _flipHorz = Matrix4x4.Rotate(Quaternion.AngleAxis(180, Vector3.right));
    static readonly Matrix4x4 _flipVert = Matrix4x4.Rotate(Quaternion.AngleAxis(180, Vector3.up));
    static readonly Matrix4x4 _rotate = Matrix4x4.Rotate(Quaternion.AngleAxis(180, Vector3.forward));

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

    [SerializeField, Tooltip("If true, treat empty tiles as matching this tile")]
    bool _nullIsThis;
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
    [SerializeField]
    Sprite _twoSidesOpposite;
    [SerializeField]
    Sprite _oneSideSideCorner;
    [SerializeField]
    Sprite _oneSideMiddleCorner;
    [SerializeField]
    Sprite _twoCornersAdjacent;
    [SerializeField]
    Sprite _twoCornersOppositeSide;
    [SerializeField]
    Sprite _twoCornersOppositeMiddle;
    [SerializeField]
    Sprite _threeCornersSide;
    [SerializeField]
    Sprite _threeCornersMiddle;
    [SerializeField]
    Sprite _oneSideTwoCorners;

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

        Sprite sprite = _filled;
        var matrix = Matrix4x4.identity;
        var index = (uint)sideMask;
        switch (index)
        {
            // three sides
            case 4:
                matrix = _flipVert;
                goto case 1;
            case 16:
                matrix = _rotate;
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
                matrix = _rotate;
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
            // two sides opposite
            case 68:
                matrix = _flipHorz;
                goto case 17;
            case 17:
                sprite = _twoSidesOpposite;
                break;
            // one side side corner
            case 23:
                matrix = _flipVert;
                goto case 71;
            case 113:
                matrix = _flipHorz;
                goto case 71;
            case 116:
                matrix = _rotate;
                goto case 71;
            case 71:
                sprite = _oneSideSideCorner;
                break;
            // one side middle corner
            case 29:
                matrix = _flipVert;
                goto case 197;
            case 92:
                matrix = _rotate;
                goto case 197;
            case 209:
                matrix = _flipHorz;
                goto case 197;
            case 197:
                sprite = _oneSideMiddleCorner;
                break;
            // two corners adjacent
            case 95:
                matrix = _flipHorz;
                goto case 125;
            case 215:
                matrix = _rotate;
                goto case 125;
            case 245:
                matrix = _flipVert;
                goto case 125;
            case 125:
                sprite = _twoCornersAdjacent;
                break;
            // two corners opposide side
            case 119:
                sprite = _twoCornersOppositeSide;
                break;
            // two corners opposite middle
            case 221:
                sprite = _twoCornersOppositeMiddle;
                break;
            // three corners side
            case 213:
                matrix = _flipVert;
                goto case 93;
            case 93:
                sprite = _threeCornersSide;
                break;
            // three corners middle
            case 117:
                matrix = _flipHorz;
                goto case 87;
            case 87:
                sprite = _threeCornersMiddle;
                break;
            // one side two corners
            case 21:
                matrix = _flipVert;
                goto case 69;
            case 84:
                matrix = _rotate;
                goto case 69;
            case 81:
                matrix = _flipHorz;
                goto case 69;
            case 69:
                sprite = _oneSideTwoCorners;
                break;
            default:
                break;
        }

        tileData.sprite = sprite;
        tileData.transform = matrix;
        tileData.flags = TileFlags.LockTransform;
        tileData.colliderType = colliderType;
    }

    bool IsBlob(ITilemap tilemap, Vector3Int position)
    {
        var tile = tilemap.GetTile(position);
        return (_nullIsThis && tile == null) || tile == this;
    }
}
