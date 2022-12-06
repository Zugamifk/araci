using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileMapService : IService
{
    Vector2 GridToWorldSpace(Vector2 gridPosition);
    Vector2 WorldToGridSpace(Vector2 worldPosition);
}
