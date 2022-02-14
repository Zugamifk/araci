using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropTable : ScriptableObject
{
    [SerializeField]
    Item[] m_Items;

    public Item[] GetLevelupOptions(int count, PlayerController player)
    {
        var items = m_Items.Where(player.CanPickup);
        var result = new Item[count];
        int rolls = 0;
        for(int i=0;i<count && rolls++ < 100; i++)
        {
            var item = items.ElementAt(Random.Range(0, items.Count()));
            if(result.Contains(item))
            {
                i--;
            } else
            {
                result[i] = item;
            }
        }

        return result;
    }
}
