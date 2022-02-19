using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : IDamageable
{
    int m_CurrentHealth;

    PlayerData m_PlayerData;
    int m_Experience;
    int m_Level;
    PlayerData.LevelData m_LevelData;

    Dictionary<Item, ItemState> m_ItemToItemState = new Dictionary<Item, ItemState>();
    Dictionary<Type, Item> m_ItemTypeToItem = new Dictionary<Type, Item>();
    IEnumerable<AttackItem> m_Attacks => m_ItemToItemState.Keys.Select(i => i as AttackItem).Where(a => a != null);

    public PlayerData.LevelData LevelData => m_LevelData;

    public int Health => m_CurrentHealth;

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
            var state = m_ItemToItemState[a];
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
        return !m_ItemToItemState.ContainsKey(item) || m_ItemToItemState[item].Level < item.MaxLevel;
    }
    
    public int GetItemLevel(Item item)
    {
        if (m_ItemToItemState.ContainsKey(item))
        {
            return m_ItemToItemState[item].Level;
        } else
        {
            return 0;
        }
    }

    public void PickUp(Item item)
    {
        if (m_ItemToItemState.ContainsKey(item))
        {
            if(m_ItemToItemState[item].Level >= item.MaxLevel)
            {
                throw new System.InvalidOperationException($"Item already at max level! Can't pickup {item.Name}, level is {m_ItemToItemState[item]}");
            }

            m_ItemToItemState[item].Level++;
        }
        else
        {
            m_ItemToItemState.Add(item, item.GetNewState());
            m_ItemTypeToItem.Add(item.GetType(), item);
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


    public void DoDamage(int damage)
    {
        m_CurrentHealth -= damage;
        Services.Find<UI>().SetCurrentHealth(m_CurrentHealth);
    }

    T GetItem<T>() where T : Item
    {
        if (m_ItemTypeToItem.ContainsKey(typeof(T)))
        {
            return (T)m_ItemTypeToItem[typeof(T)];
        } else
        {
            return null;
        }
    }

    float GetNextInterval(float interval)
    {
        return CalculateAttackInterval(interval);
    }

    public int CalculateDamage(int baseDamage)
    {
        float multiplier = 1;
        var tiger = GetItem<Tiger>();
        if (tiger != null)
        {
            multiplier += tiger.Multipliers[m_ItemToItemState[tiger].Level];
        }
        return Mathf.RoundToInt(baseDamage * multiplier);
    }

    public float CalculateRadius(float radius)
    {
        float multiplier = 1;
        var dragon = GetItem<Dragon>();
        if (dragon != null)
        {
            multiplier += dragon.Multipliers[m_ItemToItemState[dragon].Level];
        }
        return radius * multiplier;
    }

    public float CalculateAttackInterval(float baseInterval)
    {
        float multiplier = 1;
        var spider = GetItem<Spider>();
        if (spider != null)
        {
            multiplier *= spider.Multipliers[m_ItemToItemState[spider].Level];
        }
        return baseInterval * multiplier;
    }
}
