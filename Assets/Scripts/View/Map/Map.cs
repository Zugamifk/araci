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

    public void PositionObject(IMovementModel model, Transform root)
    {
        root.localPosition = _tilemap.CellToLocalInterpolated(model.Position);
    }

    public void MoveObject(IMovementModel model, Rigidbody2D rigidBody)
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
        //rigidBody.transform.position = rigidBody.transform.position + (Vector3)step;
        //rigidBody.vel((Vector2)rigidBody.transform.position + step);
        rigidBody.MovePosition((Vector2)rigidBody.transform.position + step);
    }

}
