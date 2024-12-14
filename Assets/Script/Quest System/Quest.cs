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

    public Quest(string _questID, string _name, string _description, int _moneyReward, int _expReward, List<QuestObjective> _objectives)
    {
        this.questID = _questID;
        this.name = _name;
        this.description = _description;
        this.moneyReward = _moneyReward;
        this.expReward = _expReward;
        objectives = _objectives ?? new List<QuestObjective>();
    }
    public void AddObjective(QuestObjective objective)
    {
        objectives.Add(objective);
    }
}
