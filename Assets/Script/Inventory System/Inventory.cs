using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;

    public static Inventory Instance { get; private set; }
    public List<InventorySlot> itemSlots;

    //Selected
    public InventorySlot selectedSlot;

    // UI for Selected Item
    public Image selectedItemImage;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuActivated = !menuActivated;
            Time.timeScale = menuActivated ? 0f : 1.0f;
            inventoryMenu.SetActive(menuActivated);
            DeselectedAllSlot();
        }
    }


    public void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void SortItems()
    {
        List<InventorySlot> filledSlots = itemSlots.FindAll(slot => slot.hasItem);

        filledSlots.Sort((slot1, slot2) => string.Compare(slot1.item.itemName, slot2.item.itemName, StringComparison.Ordinal));

        int index = 0;
        foreach (var slot in filledSlots)
        {
            itemSlots[index] = slot;
            index++;
        }

        for (int i = index; i < itemSlots.Count; i++)
        {
            // Clear empty slots (if necessary)
        }

        foreach (var slot in itemSlots)
        {
            slot.UpdateDisplay();
        }
    }

    public void GetItem()
    {
        
    }

    public void AddItem(Item item)
    {
        foreach (var slot in itemSlots)
        {
            if (!slot.hasItem || slot.item == item)
            {
                if (slot.isFull) continue;

                slot.AddItem(item);

                if (IsSlotSelected(slot))
                {
                    UpdateSelectedItemDisplay(slot.item);
                }

                SortItems();
                return;
            }
        }

        Debug.LogWarning("Inventory is full or no suitable slot available to add the item!");
    }

    public void RemoveItem(Item item)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.hasItem && slot.item == item)
            {
                slot.RemoveItem(1);

                if (IsSlotSelected(slot))
                {
                    UpdateSelectedItemDisplay(slot.item);
                }

                if (!slot.hasItem)
                {
                    ClearSelectedItemDisplay();
                }

                return;
            }
        }

        Debug.LogWarning("Item not found in inventory for removal!");
    }

    private bool IsSlotSelected(InventorySlot slot)
    {
        return selectedSlot == slot;
    }


    public void DeselectedAllSlot()
    {
        foreach (InventorySlot slot in itemSlots)
        {
            slot.Deselected();
        }
        ClearSelectedItemDisplay();
    }

    public void UpdateSelectedItemDisplay(Item item)
    {
        if (item != null)
        {
            selectedItemImage.sprite = item.itemSprite;
            selectedItemImage.color = Color.white;
            selectedItemName.text = item.itemName;
            selectedItemDescription.text = item.description;
        }
        else
        {
            ClearSelectedItemDisplay();
        }
    }



    private void ClearSelectedItemDisplay()
    {
        selectedItemImage.sprite = null;
        selectedItemImage.color = new Color(1, 1, 1, 0);
        selectedItemName.text = "";
        selectedItemDescription.text = "";
    }
}
