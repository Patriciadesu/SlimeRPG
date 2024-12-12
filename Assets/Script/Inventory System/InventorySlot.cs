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

    public void AddItem(Item item){
        this.item = item;
        hasItem = true;
        itemCount++;
        if(this.item.maxItemCount == itemCount) isFull = true;
        UpdateDisplay();
    }
    public void UpdateDisplay(){
        itemImage.sprite = item.itemSprite;
        itemName.text = item.itemName;
        itemCountText.text = itemCount.ToString();
    }

    public void Selected(){
        Inventory.Instance.DeselectedAllSlot();
        this.isSelected = true;
        Inventory.Instance.selectedSlot = this;
        //UpdateDisplay
    }

    public void Deselected(){
        this.isSelected = false;
        Inventory.Instance.selectedSlot = null;
        //UpdateDisplay
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left){ //IDK if this code is valid or not i cannot remember
            Selected();
        }
    }
}
