using System.Collections.Generic;

[System.Serializable]
public struct Quest
{
    public string questID;
    public string name;
    public string description;
    public List<QuestObjective> objectives;
    public int moneyReward;
    public int expReward;
    //public item reward;
}
