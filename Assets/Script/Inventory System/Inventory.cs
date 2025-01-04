using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    //Use
    public List<Item> currentUseItems;

    public Player player;

    private Dictionary<System.Type, ActiveEffect> activeEffects = new Dictionary<System.Type, ActiveEffect>();


    private void Start()
    {
        player = player.GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuActivated = !menuActivated;
            Time.timeScale = menuActivated ? 0f : 1.0f;
            inventoryMenu.SetActive(menuActivated);
            DeselectedAllSlot();
        }

        List<System.Type> effectsToRemove = new List<System.Type>();

        // Update active effects
        foreach (var i in activeEffects)
        {
            ActiveEffect effect = i.Value;
            if (!effect.isActive) continue;

            effect.remainingTime -= Time.deltaTime;
            if (effect.remainingTime <= 0)
            {
                effect.item.RemoveEffect();
                effect.isActive = false;
                effectsToRemove.Add(i.Key);
            }
        }

        // Remove expired effects
        foreach (var type in effectsToRemove)
        {
            activeEffects.Remove(type);
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
        int targetIndex = 0;

        foreach (var slot in itemSlots)
        {
            if (slot.hasItem)
            {
                if (itemSlots[targetIndex] != slot)
                {
                    itemSlots[targetIndex].item = slot.item;
                    itemSlots[targetIndex].itemCount = slot.itemCount;
                    itemSlots[targetIndex].hasItem = true;
                    itemSlots[targetIndex].isFull = slot.isFull;

                    slot.ClearSlot();
                }
                targetIndex++;
            }
        }

        foreach (var slot in itemSlots)
        {
            slot.UpdateDisplay();
        }
    }


    public void GetItem(sUserItem[] items)
    {
        foreach (var sItem in items)
        {
            Item item = ItemManager.Instance.GetItemFromID(sItem._id);
            if (item == null)
            {
                Debug.LogError($"Item with ID {sItem._id} not found.");
                continue;
            }

            AddItem(item, sItem.amount);
        }
    }


    public void AddItem(Item item, int amount = 1)
    {
        if (amount <= 0) return;
        foreach (var slot in itemSlots)
        {
            if (!slot.hasItem || slot.item == item)
            {
                if (slot.isFull) continue;

                int remainingAmount = slot.AddItem(item, amount);

                if (IsSlotSelected(slot))
                {
                    UpdateSelectedItemDisplay(slot.item);
                }

                SortItems();

                if (remainingAmount <= 0) return;

                amount = remainingAmount;
            }
        }

        if (amount > 0)
        {
            Debug.LogWarning($"Inventory is full or no suitable slot available to add {amount} of {item.name}!");
        }
    }


    public void RemoveItem(Item item, int amount = 1)
    {
        if (amount <= 0) return;

        int totalAmountInInventory = 0;

        foreach (var slot in itemSlots)
        {
            if (slot.hasItem && slot.item == item)
            {
                totalAmountInInventory += slot.itemCount;
            }
        }


        if (amount > totalAmountInInventory)
        {
            Debug.LogError($"Cannot remove {amount} {item.name}(s). Only {totalAmountInInventory} available in inventory.");
            return;
        }

        for (int i = itemSlots.Count - 1; i >= 0; i--)
        {
            var slot = itemSlots[i];
            if (slot.hasItem && slot.item == item)
            {
                int amountToRemove = Mathf.Min(amount, slot.itemCount);

                slot.RemoveItem(amountToRemove);
                amount -= amountToRemove;

                if (IsSlotSelected(slot))
                {
                    UpdateSelectedItemDisplay(slot.item);
                }

                if (!slot.hasItem)
                {
                    ClearSelectedItemDisplay();
                }
                if (amount <= 0)
                {
                    SortItems();
                    return;
                }
            }
        }

        SortItems();
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

    #region OnUse
    public void Use(){
        if(selectedSlot== null) return;
        currentUseItems.Add(selectedSlot.item);
        selectedSlot.RemoveItem(1);
        foreach(UsableItem usableItem in currentUseItems){
            //check if the item is not currently
        }
    }

    public void UseItem(UsableItem item)
    {
        System.Type itemType = item.GetType();

        if (activeEffects.TryGetValue(itemType, out ActiveEffect existingEffect))
        {
            if (existingEffect.item.priority >= item.priority)
            {
                existingEffect.remainingTime = existingEffect.item.duration;
                return;
            }

            existingEffect.item.RemoveEffect();
            existingEffect.isActive = false;
        }

        ActiveEffect newEffect = new ActiveEffect(item);
        item.ApplyEffect();
        activeEffects[itemType] = newEffect;
    }

    public void PrintActiveEffects()
    {
        Debug.Log("Currently Active Effects:");
        foreach (var effect in activeEffects)
        {
            if (effect.Value.isActive)
            {
                Debug.Log($"{effect.Value.item.itemName}: {effect.Value.remainingTime:F1}s remaining");
            }
        }
    }

    #endregion
}
