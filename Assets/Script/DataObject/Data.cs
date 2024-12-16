using System;
using System.Collections.Generic;

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
public class sUserItem
{
    public string _id;
    public int amount;
}

[Serializable]
public class sItem
{
    public string _id;
    public string name;
    public int amount;
}

[Serializable]
public class sBoostEvent
{
    public string _id;
    public string name;
    public string[] activatedDays;
    public int activatedHour;
    public float duration;

    public float expMultiplier;
    public float coinMultiplier;
    public float itemDropRateMultiplier;
}

[Serializable]
public class sBossEvent
{
    public string _id;
    public string name;
    public string[] activatedDays;
    public int activatedHour;
    public float duration;

    public string bossSpawner;

}

[Serializable]
public class sSpawner
{
    public string name;
    public float range;
    public float delay;
    public string[] enemyIDs;
    public int maxEnemy;
    public sSpawnPos spawnPos;
}

[Serializable]
public class sSpawnPos
{
    public float x;
    public float y;
}