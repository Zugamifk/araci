using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    Text m_HealthText;
    [SerializeField]
    Image m_HealthBar;

    [SerializeField]
    Text m_ExperienceText;
    [SerializeField]
    Image m_ExperienceBar;

    [SerializeField]
    Transform m_EquippedItemsList;
    [SerializeField]
    EquippedItemInfo m_EquippedItemTemplate;

    [SerializeField]
    GameObject m_LevelupPanel;
    [SerializeField]
    LevelUpOptionButton[] m_LevelupOptionButtons;

    [SerializeField]
    TextParticle m_TextParticleTemplate;
    [SerializeField]
    RectTransform m_TextParticlesRoot;

    int m_MaxHealth=1;
    int m_CurrentHealth;

    int m_MaxExperience= 1;
    int m_CurrentExperience;

    Dictionary<Item, EquippedItemInfo> m_EquippedItems = new Dictionary<Item, EquippedItemInfo>();

    event Action<Item> m_OnChoseItem;

    private void Start()
    {
        for (int i = 0; i < m_LevelupOptionButtons.Length; i++)
        {
            m_LevelupOptionButtons[i].SelectedItem += OnLevelUp;
        }
    }

    public void SetMaxHealth(int health)
    {
        m_MaxHealth = health;
        UpdateHealth();
    }

    public void SetCurrentHealth(int health)
    {
        m_CurrentHealth = health;
        UpdateHealth();
    }

    void UpdateHealth()
    {
        m_HealthText.text= $"{m_CurrentHealth}/{m_MaxHealth}";
        m_HealthBar.fillAmount = (float)m_CurrentHealth / (float)m_MaxHealth;
    }

    public void SetCurrentExperience(int experience)
    {
        m_CurrentExperience = experience;
        UpdateExperience();
    }

    public void SetLevelData(PlayerData.LevelData levelData)
    {
        m_MaxExperience = levelData.NextLevelExperience;
        UpdateExperience();
    }

    void UpdateExperience()
    {
        m_ExperienceText.text = $"{m_CurrentExperience}/{m_MaxExperience}";
        m_ExperienceBar.fillAmount = (float)m_CurrentExperience / (float)m_MaxExperience;
    }

    public void SetPickLevelupCallback(Action<Item> onLevelUp)
    {
        m_OnChoseItem = onLevelUp;
    }

    void OnLevelUp(Item item)
    {
        m_LevelupPanel.SetActive(false);
        m_OnChoseItem.Invoke(item);
    }

    public void ShowLevelupPanel(PlayerController player, Item[] items)
    {
        for(int i=0;i<m_LevelupOptionButtons.Length; i++)
        {
            if (i < items.Length && items[i]!=null)
            {
                m_LevelupOptionButtons[i].SetItem(items[i], player.GetItemLevel(items[i]));
                m_LevelupOptionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                m_LevelupOptionButtons[i].gameObject.SetActive(false);
            }
        }
        m_LevelupPanel.SetActive(true);
    }

    public void AddItem(Item item, int level)
    {
        EquippedItemInfo ui;
        if(!m_EquippedItems.TryGetValue(item, out ui))
        {
            ui = Instantiate(m_EquippedItemTemplate);
            ui.transform.SetParent(m_EquippedItemsList);
            ui.gameObject.SetActive(true);
            m_EquippedItems.Add(item, ui);
        }

        ui.SetItem(item);
        ui.SetLevel(level);
    }

    public void SpawnDamageCounter(int amount, Vector3 position)
    {
        var text = Instantiate(m_TextParticleTemplate, m_TextParticlesRoot);
        text.gameObject.SetActive(true);
        text.Play(amount.ToString(), position);
    }
}
