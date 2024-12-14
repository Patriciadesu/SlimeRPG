using System;
using System.Collections.Generic;
using UnityEngine;

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
    [Range(0f, 1f)] public float dropRate;
}


