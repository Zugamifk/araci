using System.Collections;
using System.Collections.Generic;
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

    public void PositionObject(IMovementModel model, Transform root)
    {
        root.localPosition = _tilemap.CellToLocalInterpolated(model.Position);
    }

}
