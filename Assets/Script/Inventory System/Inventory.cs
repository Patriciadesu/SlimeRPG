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
        //Wait
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
            }
        }
    }

    public void DeselectedAllSlot(){
        foreach(InventorySlot i in itemSlots){
            i.Deselected();
        }
    }
}
