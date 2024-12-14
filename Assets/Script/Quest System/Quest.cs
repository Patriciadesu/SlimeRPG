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

    public Quest(string _questID, string _name, string _description, int _moneyReward, int _expReward)
    {
        this.questID = _questID;
        this.name = _name;
        this.description = _description;
        this.moneyReward = _moneyReward;
        this.expReward = _expReward;
        this.objectives = new List<QuestObjective>();
    }
}
