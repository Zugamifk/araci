using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Button slotButton;

    Guid currentItemGuid;

    public event Action<Guid> ClickedSlot;

    public void SetItem(Guid id)
    {
        currentItemGuid = id;
        if (currentItemGuid == Guid.Empty)
        {
            slotButton.enabled = false;
            Clear();
        } else
        {
            slotButton.enabled = true;
            ShowItemInfo();
        }
    }

    void Clear()
    {
        itemIcon.sprite = null;
        countText.text = string.Empty;
    }

    void ShowItemInfo()
    {
        var item = Game.Model.Items[id];
        var data = DataService.GetData<ItemDataCollection>().Get(item.Key);
        itemIcon.sprite = data.Icon;
        countText.text = item.Count.ToString();
    }

    public void OnClicked()
    {
        ClickedSlot?.Invoke(currentItemGuid);
    }
}
