using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Character>()!=null)
        {
            PickupItem();
            Destroy(gameObject);
        }
    }

    public abstract void PickupItem();

}
