using UnityEngine;

public class ItemContainer : MonoBehaviour , ICollectables
{
    public Item item;

    public void Collect()
    {
        Inventory.Instance.AddItem(item);
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                Collect();
            }
        }
    }

    void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }
}
