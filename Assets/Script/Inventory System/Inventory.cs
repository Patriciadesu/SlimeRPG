using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;

    public static Inventory Instance { get; private set; }
    public List<InventorySlot> itemSlots;

    //Selected
    public InventorySlot selectedSlot;

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
            /*itemSlots[i] = new InventorySlot();*/
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
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if ( !itemSlots[i].hasItem || itemSlots[i].item == item){
                if(itemSlots[i].isFull) return;
                itemSlots[i].AddItem(item);
                SortItems();
            }
        }
    }

    public void DeselectedAllSlot(){
        foreach(InventorySlot i in itemSlots){
            i.Deselected();
        }
    }
}
