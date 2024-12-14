using UnityEngine;

public class SkillContainer : MonoBehaviour , ICollectables
{
    public Skill skill;
    public void Collect()
    {
        // Player.Instance.AddSkill(skill);
    }

    void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = skill.SkillSprite;
    }
}
