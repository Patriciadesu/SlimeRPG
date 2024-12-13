using UnityEngine;

public class ItemContainer : MonoBehaviour , ICollectables
{
    public Item item;

    public void Collect()
    {
        Inventory.Instance.AddItem(item);
        Destroy(this);
    }

    void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }
}
