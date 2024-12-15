using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkillContainer : MonoBehaviour , ICollectables
{
    public Skill skill;
    public void Collect()
    {
        // Player.Instance.AddSkill(skill);
    }

    void Awake()
    {
        GetSprite();
    }
    public void GetSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = skill.SkillSprite;
    }
    public void ChangeItem(Skill NewSkill)
    {
        skill = NewSkill;
    }
}
