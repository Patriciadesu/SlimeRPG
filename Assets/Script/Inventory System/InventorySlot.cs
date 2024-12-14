using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour , IPointerClickHandler
{
    //Slot Data
    public Item item;
    public int itemCount;
    public bool isFull = false;
    public bool hasItem = false;
    public bool isSelected = false;

    //Item Display
    public TextMeshProUGUI itemName;
    public Image itemImage;
    public TextMeshProUGUI itemCountText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    public void AddItem(Item item)
    {
        if (hasItem && this.item != item) return;

        this.item = item;
        hasItem = true;
        itemCount++;

        isFull = itemCount >= item.maxItemCount;

        UpdateDisplay();
    }


    public void RemoveItem(int amount = 1)
    {
        if (!hasItem) return;

        itemCount -= amount;

        if (itemCount <= 0)
        {
            ClearSlot();
        }
        else
        {
            isFull = itemCount >= item.maxItemCount;
            UpdateDisplay();
        }
    }


    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        hasItem = false;
        isFull = false;

        if (itemName != null) itemName.text = string.Empty;
        if (itemCountText != null) itemCountText.text = string.Empty;
        if (itemImage != null) itemImage.sprite = null;

        UpdateDisplay();
    }



    public void UpdateDisplay()
    {
        if (item != null)
        {
            itemImage.sprite = item.itemSprite;
            itemName.text = item.itemName;
            itemCountText.text = itemCount.ToString();
        }
        else
        {
            itemImage.sprite = null;
            itemName.text = string.Empty;
            itemCountText.text = string.Empty;
        }
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, hasItem ? 1f : 0f);
    }


    public void Selected()
    {
        Inventory.Instance.DeselectedAllSlot();
        this.isSelected = true;
        Inventory.Instance.selectedSlot = this;
        selectedShader.SetActive(true);
        Inventory.Instance.UpdateSelectedItemDisplay(item);
    }


    public void Deselected()
    {
        if (selectedShader != null)
            selectedShader.SetActive(false);

        isSelected = false;
        Inventory.Instance.selectedSlot = null;
        UpdateDisplay();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left){
            Selected();
        }
    }
}
