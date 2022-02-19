using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccuumRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gem = collision.gameObject.GetComponent<ExperienceGem>();
        if(gem!=null)
        {
            gem.StartVaccuuming(Services.Find<Character>().transform, 3);
        }
    }
}
