using System;

[Serializable]
public class sQuest
{
    public string _id;
    public string name;
    public string description;
    public string reward;
    public List<string> Objective;
}
[Serializable]
public class sObjective
{
    public string _id;
    public string name;
    public string enemyID;
    public int requiredAmount;
}

[Serializable]
public class sReward
{
    public string _id;
    public string name;
    public int coin;
    public int xp;
    public List<sItem> item;
}
[Serializable]
public class sItem
{
    public string _id;
    public string name;
    public int amount;
}