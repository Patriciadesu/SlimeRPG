using System.Collections.Generic;

public class sQuest
{
    public string _id;
    public string name;
    public string description;
    public string reward;
    public List<string> Objective;
}

public class sObjective
{
    public string _id;
    public string name;
    public string enemyID;
    public int requiredAmount;
}


public class sReward
{
    public string _id;
    public string name;
    public int coin;
    public int xp;
    public List<sItem> item;
}

public class sItem
{
    public string _id;
    public string name;
    public int amount;
}