using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<Item> itemData;

    void Start()
    {
        
    }

    public void UpdateItemData()
    {
        List<QuestManager.sItem> sItems = new List<QuestManager.sItem>(); // sItems = getitems from database etc etc 

        for (int i = 0; i < itemData.Count; i++)
        {
            for (int j = 0; j < sItems.Count; j++)
            {
                if (itemData[i].itemID == sItems[j]._id)
                {
                    itemData[i].itemName = sItems[j].name;
                    // update more variables in here.
                }
            }
        }
    }

    public Item GetItemFromID(string id)
    {
        if (itemData == null || itemData.Count == 0)
        {
            Debug.LogError("Item Data is empty.");
            return null;
        }

        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemData[i].itemID == id)
            {
                return itemData[i];
            }
        }
        
        Debug.LogError("This Item ID does not exist in the data.");
        return null;
    }
}
