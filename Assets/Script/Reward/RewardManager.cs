using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public List<Reward> rewards = new List<Reward>();
    
    public void GetAllRewards()
    {
        //rewards = get from databse;
    }

    public void GiveReward(int rewardIndex)
    {
        Reward rewardToGive = rewards[rewardIndex];
        
        for (int i = 0; i < rewardToGive.items.Count; i++)
        {
            if (Random.Range(0, 1) > rewardToGive.items[i].dropRate)
            {
                continue;
            }

            Inventory.Instance.AddItem(ItemManager.Instance.GetItemFromID(rewardToGive.items[i].itemID));
        }

        Player.Instance.exp += rewardToGive.exp;
        Player.Instance.coin += rewardToGive.coin;
    }
}
