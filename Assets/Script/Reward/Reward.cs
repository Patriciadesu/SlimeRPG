using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Reward
{
    public string rewardID;
    public float exp;
    public float coin;
    public List<DropRate> items;
}

[Serializable]
public struct DropRate
{
    public string itemID;
    [Range(0f, 1f)] public float dropRate;
}


