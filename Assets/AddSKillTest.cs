using UnityEngine;

public class AddSKillTest : MonoBehaviour
{
    [SerializeField] private Skill skill;
    [SerializeField] private Player.SkillSlotType skillSlotType;

    public void AddSkill()
    {
        Player.Instance.AddSkill(skill , skillSlotType);
    }
}
