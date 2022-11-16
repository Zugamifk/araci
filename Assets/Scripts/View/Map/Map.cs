using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Map : MonoBehaviour
{
    [SerializeField]
    Tilemap _tilemap;

    public static Map Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PositionObject(IMovementModel model, Transform root)
    {
        root.localPosition = _tilemap.CellToLocalInterpolated(model.Position);
    }

    public void MoveObject(IMovementModel model, Transform root)
    {
        var step = model.DesiredMove;
        switch (model.MovementSpace)
        {
            case Space.World:
                break;
            case Space.Local:
                break;
            case Space.Grid:
                step = _tilemap.CellToLocalInterpolated(step);
                break;
            default:
                break;
        }
        root.localPosition += (Vector3)step;
    }

}
