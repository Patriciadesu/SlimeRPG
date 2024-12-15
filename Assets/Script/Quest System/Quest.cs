using JetBrains.Annotations;
using System.Collections.Generic;
using static QuestManager;

[System.Serializable]
public struct Quest
{
    public string questID;
    public string name;
    public string description;
    public List<QuestObjective> objectives;
    //public item reward;

    public Quest(sQuest data)
    {
        this.questID = data._id;
        this.name = data.name;
        this.description = data.description;
        //this.moneyReward = data.reward.coin;
        //this.expReward = data.reward.xp;

        this.objectives = new List<QuestObjective>();
    }
}
