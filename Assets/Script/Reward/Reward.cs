using System;
using System.Collections.Generic;

[Serializable]
public struct Reward
{
    public string rewardID;
    public List<ItemReward> items;
    public int exp;
    public int coin;
}

public struct ItemReward
{
    public string itemID;
    public float dropRate;
}


