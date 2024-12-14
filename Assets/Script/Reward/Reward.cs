using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Reward
{
    public string rewardID;
    public float exp;
    public float coin;
    public List<ItemReward> items;
}

[Serializable]
public struct ItemReward
{
    public string itemID;
    [Range(0f, 1f)] public float dropRate;
}


