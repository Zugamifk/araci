using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropController
{
    DropTable m_DropTable;

    float m_DropWeightTotal = 0;

    public void SetDrops(DropTable drops)
    {
        m_DropTable = drops;
    
        foreach(var d in drops.Drops)
        {
            m_DropWeightTotal += d.DropWeight;
        }
    }

    public Item[] GetLevelupOptions(int count, PlayerController player)
    {
        var items = m_DropTable.Items.Where(player.CanPickup);
        var result = new Item[count];
        int rolls = 0;
        for (int i = 0; i < count && rolls++ < 100; i++)
        {
            var item = items.ElementAt(Random.Range(0, items.Count()));
            if (result.Contains(item))
            {
                i--;
            }
            else
            {
                result[i] = item;
            }
        }

        return result;
    }

    public void DropReward(Enemy enemy)
    {
        Pickup pickup = null;
        if(Random.value < m_DropTable.PickupChance)
        {
            pickup = RollDrop();
        } else
        {
            pickup = m_DropTable.ExperienceGems[Random.Range(0, m_DropTable.ExperienceGems.Length)];
        }

        var instance = GameObject.Instantiate(pickup);
        instance.transform.position = enemy.transform.position;
        instance.gameObject.SetActive(true);
    }

    Pickup RollDrop()
    {
        var r = Random.Range(0, m_DropWeightTotal);
        var s = 0f;
        foreach(var d in m_DropTable.Drops)
        {
            s += d.DropWeight;
            if(s > r)
            {
                return d.Item;
            }
        }

        throw new System.InvalidOperationException("Failed to roll a drop!");
    }
}
