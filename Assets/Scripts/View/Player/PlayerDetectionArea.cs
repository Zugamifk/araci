using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        var target = collision.GetComponent<InteractableTarget>();
        if (target == null) return;

        var id = collision.GetComponent<Identifiable>();
        if (!target.IsInteractable)
        {
            Game.Do(new RemoveInteractable(id.Id));
        } else
        {
            var pos = Map.Instance.GridToWorldSpace(target.transform.position);
            Game.Do(new AddInteractable(id.Id, pos));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var target = collision.GetComponent<InteractableTarget>();
        if (target == null) return;

        var id = collision.GetComponent<Identifiable>();
        Game.Do(new RemoveInteractable(id.Id));
    }
}
