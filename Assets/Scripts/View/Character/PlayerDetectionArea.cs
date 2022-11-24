using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<InteractableTarget>();
        if (target == null ) return;

        var id = other.GetComponent<Identifiable>();
        var pos = Map.Instance.GetGridPosition(target.transform.position);
        Game.Do(new AddInteractable(id.Id, pos));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var target = collision.GetComponent<InteractableTarget>();
        if (target == null) return;

        var id = collision.GetComponent<Identifiable>();
        Game.Do(new RemoveInteractable(id.Id));
    }
}
