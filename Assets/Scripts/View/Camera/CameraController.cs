using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Camera realCamera;

    private void Start()
    {
        Game.Do(new RegisterCamera(OnRegistered));
    }

    void OnRegistered(ICameraModel model)
    {
        model.Size.ValueChanged += OnSizeChanged;

        var position = Game.Model.Positions[model.Id];
        position.Position.ValueChanged += OnMoved;
    }

    void OnSizeChanged(float oldSize, float newSize)
    {
        realCamera.orthographicSize = newSize;
    }

    void OnMoved(Vector2 oldPosition, Vector2 newPosition)
    {
        transform.position = Map.Instance.GridToWorldSpace(newPosition);
    }
}
