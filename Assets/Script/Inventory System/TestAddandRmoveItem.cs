using UnityEngine;

public class TestAddandRmoveItem : MonoBehaviour
{
    private Inventory inventory;
    public Item item;

    private void Start()
    {
         inventory = Object.FindFirstObjectByType<Inventory>();
    }
    public void AddItem(){ inventory.AddItem(item); }

    public void AddManyItem(int amounts) {  inventory.AddItem(item,amounts); }

    public void RemoveItem() { inventory.RemoveItem(item); }

    public void RemoveManyItem(int amounts) { inventory.RemoveItem(item,amounts);}
}
