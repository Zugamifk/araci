using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemInfo : MonoBehaviour
{
    [SerializeField]
    Image m_IconImage;
    [SerializeField]
    Text m_Level;

    public void SetItem(Item item)
    {
        m_IconImage.sprite = item.Icon;
        SetLevel(1);
    }

    public void SetLevel(int level)
    {
        m_Level.text = $"{level+1}";
    }
}
