using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour, ITileMapService
{
    [SerializeField]
    Tilemap _tilemap;

    public static Map Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 WorldToGridSpace(Vector2 worldPosition)
    {
        return _tilemap.LocalToCellInterpolated(worldPosition);
    }

    public Vector2 GridToWorldSpace(Vector2 gridPosition)
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
        velocity = _tilemap.CellToLocalInterpolated(velocity);
        rigidBody.velocity = velocity;
    }

}
