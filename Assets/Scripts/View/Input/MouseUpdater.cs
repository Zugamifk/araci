using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUpdater : MonoBehaviour
{
    [SerializeField]
    Camera _raycastCamera;

    private void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // UI handles itself
            return;
        }

        HandleWorldInput();
    }

    bool HandleWorldInput()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return false;
        }

        Ray ray = _raycastCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return false;
        }

        var handler = hit.collider.GetComponent<MouseListener>();
        if (handler == null)
        {
            return false;
        }

        Game.Do(new UpdateWorldMousePosition(handler.Id, hit.point));

        return true;
    }
}
