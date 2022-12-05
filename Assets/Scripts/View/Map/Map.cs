using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField]
    Tilemap _tilemap;

    public static Map Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetGridPosition(Vector2 worldPosition)
    {
        return _tilemap.LocalToCellInterpolated(worldPosition);
    }

    public Vector2 GetWorldPosition(Vector2 gridPosition)
    {
        return _tilemap.CellToLocalInterpolated(gridPosition);
    }

    public void PositionObject(IMovementModel model, Transform root)
    {
        root.localPosition = _tilemap.CellToLocalInterpolated(model.Position);
    }

    public void MoveObject(IMovementModel model, Rigidbody2D rigidBody)
    {
        var velocity = model.Direction * model.Speed;
        switch (model.MovementSpace)
        {
            case Space.World:
                break;
            case Space.Local:
                break;
            case Space.Grid:
                velocity = _tilemap.CellToLocalInterpolated(velocity);
                break;
            default:
                break;
        }
        rigidBody.velocity = velocity;
    }

}
