using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : UIPanel
{
    [SerializeField] Image selectedItemIcon;
    [SerializeField] TextMeshProUGUI selectedItemName;
    [SerializeField] TextMeshProUGUI selectedItemCount;
    [SerializeField] TextMeshProUGUI selectedItemDescription;
    [SerializeField] InventorySlotUI slotPrefab;
    [SerializeField] Transform gridRoot;

    Guid currentSelectedItem;
    List<InventorySlotUI> slots = new();
    protected override void OnShowingChanged(bool showing)
    {
        if (showing)
        {
            OnShowing();
        } else
        {
            OnHiding();
        }
    }

    void OnShowing()
    {
        var invSlots = Game.Model.Inventory.Slots;
        invSlots.ItemChanged -= UpdateSlot;
        invSlots.ItemChanged += UpdateSlot;

        UpdateInventorySlots();
    }

    void OnHiding()
    {
        Game.Model.Inventory.Slots.ItemChanged -= UpdateSlot;
    }

    private void Awake()
    {
        ClearItemInfo();

        var invSlots = Game.Model.Inventory.Slots;
        for(int i=0;i<24;i++)
        {
            var slot = Instantiate(slotPrefab);
            slot.transform.SetParent(gridRoot);

            slot.ClickedSlot += OnClickedSlot;

            slots.Add(slot);
        }
        UpdateInventorySlots();
    }

    private void OnDestroy()
    {
        Game.Model.Inventory.Slots.ItemChanged -= UpdateSlot;
    }

    void UpdateInventorySlots()
    {
        var invSlots = Game.Model.Inventory.Slots;
        for (int i = 0; i < 24; i++)
        {
            if (i < invSlots.Count)
            {
                UpdateSlot(i, invSlots[i]);
            }
            else
            {
                UpdateSlot(i, Guid.Empty);
            }
        }
    }

    void UpdateSlot(int index, Guid id)
    {
        slots[index].SetItem(id);
    }

    void OnClickedSlot(Guid id)
    {
        currentSelectedItem = id;
        if (id!=Guid.Empty)
        {
            ShowItemInfo(id);
        }
    }

    void ClearItemInfo()
    {
        selectedItemCount.text = "0";
        selectedItemName.text = "-";
        selectedItemDescription.text = "-";
        selectedItemIcon.enabled = false;
    }

    void ShowItemInfo(Guid id)
    {
        var item = Game.Model.Items[id];
        var data = DataService.GetData<ItemDataCollection>().Get(item.Key);

        selectedItemIcon.enabled = true;
        selectedItemIcon.sprite = data.Icon;
        selectedItemName.text = data.DisplayName;
        selectedItemCount.text = item.Count.ToString();
        selectedItemDescription.text = data.Description;
    }

}
