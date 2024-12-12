using UnityEngine;

public class Item : MonoBehaviour , ICollectables
{
    private string itemName;
    private int itemCount;
    private int price;
    private string description;

    public void Collect()
    {
        throw new System.NotImplementedException();
    }
}
