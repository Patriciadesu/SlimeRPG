using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public class sPlayer
{
    public string _id;
    public string discordId;
    public int coin;
    public sPlayerSkill[] skillInventory;
    public sPlayerItem[] itemInventory;
    public sPlayerStat stats;
    public sPlayerPosition lastPos;
    public int lastScene;
    public string currentQuest;
    public sPlayerProgress[] questProgress;
    //public int __v;
}

[Serializable]
public class sPlayerProgress
{
    public string _id;
    public int currentProgress;
}

[Serializable]
public class sPlayerItem
{
    public string _id;
    public string amount;
}

[Serializable]
public class sPlayerSkill
{
    public string _id;
    public int level;
}

[Serializable]
public class sPlayerStat
{
    public int level;
    public float xp;
    public float currentHp;
}

[Serializable]
public class sPlayerPosition
{
    public float x;
    public float y;
}

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
    public int currentAmount;
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
[Serializable]
public class sEnemy{
    public string _id;
    public string name;
    public string rewardID;
}
[Serializable]
public class PlayerData
{
    public string id;
    public string fullname;
    public string discordId;
    public List<string> rank;
    public List<string> role;
}