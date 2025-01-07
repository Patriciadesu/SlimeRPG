using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    public List<Reward> rewards = new List<Reward>();
    [SerializeField] private GameObject itemContainerPrefab;
    public float expBoostRate = 1f;
    public float coinBoostRate = 1f;
    public float dropBoostRate = 1f;
    
    /*
    public void GetAllRewards()
    {
        //rewards = get from databse;
    }
    */

    public void GiveReward(string rewardID)
    {
        Reward rewardToGive = rewards.Find(r => r.rewardID.Equals(rewardID));

        Debug.Log(rewardToGive + " " + rewardToGive.rewardID);

        if (string.IsNullOrEmpty(rewardToGive.rewardID)) return;

        for (int i = 0; i < rewardToGive.items.Count; i++)
        {
            if (Random.Range(0, 1) > rewardToGive.items[i].dropRate * dropBoostRate)
            {
                continue;
            }

            Inventory.Instance.AddItem(ItemManager.Instance.GetItemFromID(rewardToGive.items[i].itemID));
        }

        Player.Instance.Exp += rewardToGive.exp * expBoostRate;
        Player.Instance.coin += rewardToGive.coin * coinBoostRate;
    }

    public void DropReward(string rewardID, Transform position)
    {
        Reward rewardToGive = rewards.Find(r => r.rewardID.Equals(rewardID));

        Debug.Log(rewardToGive + " " + rewardToGive.rewardID);

        if (string.IsNullOrEmpty(rewardToGive.rewardID)) return;

        for (int i = 0; i < rewardToGive.items.Count; i++)
        {
            if (Random.Range(0, 1) > rewardToGive.items[i].dropRate * dropBoostRate)
            {
                continue;
            }

            GameObject itemObject = Instantiate(itemContainerPrefab, position);
            ItemContainer itemContainer = itemObject.GetComponent<ItemContainer>();
            itemContainer.item = ItemManager.Instance.GetItemFromID(rewardToGive.items[i].itemID); 
        }

        Player.Instance.Exp += rewardToGive.exp * expBoostRate;
        Player.Instance.coin += rewardToGive.coin * coinBoostRate;
    }
}
