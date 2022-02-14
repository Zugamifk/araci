using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpOptionButton : MonoBehaviour
{
    [SerializeField]
    Image m_ItemImage;
    [SerializeField]
    Text m_TitleText;
    [SerializeField]
    Text m_DescriptionText;

    Item m_item;

    public event Action<Item> SelectedItem;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void SetItem(Item item, int level)
    {
        m_ItemImage.sprite = item.Icon;
        m_TitleText.text = item.name;
        m_DescriptionText.text = item.Levels[level].Description;

        m_item = item;
    }

    void OnClick()
    {
        SelectedItem.Invoke(m_item);
    }
}
