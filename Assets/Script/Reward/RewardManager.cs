using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    public List<Reward> rewards = new List<Reward>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public void GetAllRewards()
    {
        //rewards = get from databse;
    }

    public void GiveReward(string rewardID)
    {
        Reward rewardToGive = rewards.Find(r => r.rewardID.Equals(rewardID));

        if (string.IsNullOrEmpty(rewardToGive.rewardID)) return;

        for (int i = 0; i < rewardToGive.items.Count; i++)
        {
            if (Random.Range(0, 1) > rewardToGive.items[i].dropRate)
            {
                continue;
            }

            Inventory.Instance.AddItem(ItemManager.Instance.GetItemFromID(rewardToGive.items[i].itemID));
        }

        Player.Instance.Exp += rewardToGive.exp;
        Player.Instance.coin += rewardToGive.coin;
    }
}
