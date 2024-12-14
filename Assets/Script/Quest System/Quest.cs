using System.Collections.Generic;
using static QuestManager;

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

    public Quest(sQuest data)
    {
        this.questID = data._id;
        this.name = data.name;
        this.description = data.description;
        this.moneyReward = data.reward.coin;
        this.expReward = data.reward.xp;

        this.objectives = new List<QuestObjective>();

        foreach (string objectiveName in data.Objective)
        {
            QuestObjective objective = new QuestObjective(objectiveName, "", 0);
            this.objectives.Add(objective);
        }
    }

}
