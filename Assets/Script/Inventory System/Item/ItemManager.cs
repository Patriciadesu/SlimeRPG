using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public List<Item> itemData;

    public void GetItemData()
    {
        // Should get the item data from database. For now, just set the data yourself in the inspector.
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
