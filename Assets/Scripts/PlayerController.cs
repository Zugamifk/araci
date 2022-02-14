using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController
{
    int m_CurrentHealth;

    PlayerData m_PlayerData;
    int m_Experience;
    int m_Level;
    PlayerData.LevelData m_LevelData;

    Dictionary<Item, ItemState> m_Items = new Dictionary<Item, ItemState>();
    IEnumerable<AttackItem> m_Attacks => m_Items.Keys.Select(i => i as AttackItem).Where(a => a != null);

    public PlayerData.LevelData LevelData => m_LevelData;

    public PlayerController()
    {
        m_PlayerData = Services.Find<GameController>().PlayerData;
        m_CurrentHealth = m_PlayerData.MaxHealth;

        m_Level = 0;
        m_Experience = 0;
        m_LevelData = m_PlayerData.Levels[0];
    }

    public void Update(float time)
    {
        foreach(var a in m_Attacks)
        {
            var state = m_Items[a];
            var t = state.RemainingInterval;
            t -= time;
            if(t < 0)
            {
                a.Attack(state);
                t = GetNextInterval(a.BaseInterval);
            }
            state.RemainingInterval = t;
        }
    }

    public bool CanPickup(Item item)
    {
        return !m_Items.ContainsKey(item) || m_Items[item].Level < item.MaxLevel;
    }
    
    public int GetItemLevel(Item item)
    {
        if (m_Items.ContainsKey(item))
        {
            return m_Items[item].Level;
        } else
        {
            return 0;
        }
    }

    public void PickUp(Item item)
    {
        if (m_Items.ContainsKey(item))
        {
            if(m_Items[item].Level >= item.MaxLevel)
            {
                throw new System.InvalidOperationException($"Item already at max level! Can't pickup {item.Name}, level is {m_Items[item]}");
            }

            m_Items[item].Level++;
        }
        else
        {
            m_Items.Add(item, item.GetNewState());
        }
    }

    public bool GainExperience(int amount)
    {
        m_Experience += amount;
        Services.Find<UI>().SetCurrentExperience(m_Experience);

        var levelledUp = m_Experience >= m_LevelData.NextLevelExperience;
        if (levelledUp)
        {
            m_Level++;
            m_LevelData = m_PlayerData.Levels[m_Level];
        }
        return levelledUp;
    }


    public void Damage(int damage)
    {
        m_CurrentHealth -= damage;
        Services.Find<UI>().SetCurrentHealth(m_CurrentHealth);
    }

    float GetNextInterval(float interval)
    {
        return interval;
    }
}
