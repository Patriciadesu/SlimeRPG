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
        // Reward rewardToGive = rewards[rewardIndex];
        // player.items.Add(rewardToGive.Items]);
        // player.exp += rewardToGive.exp;
        // player.coins += rewardToGive.coins;
    }
}
