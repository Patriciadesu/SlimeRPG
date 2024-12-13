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
        
        for (int i = 0; i < rewardToGive.itemIDs.Count; i++)
        {
            Inventory.Instance.AddItem(rewardToGive.itemIDs[i]);
        }
        Player.Instance.exp += rewardToGive.exp;
        Player.Instance.coin += rewardToGive.coin;
    }
}
