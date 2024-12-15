using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class SkillContainer : MonoBehaviour , ICollectables
{
    public Skill skill;
    public TMP_Text PriceText;
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
        PriceText.text = skill.Price.ToString();

    }
    public void ChangeItem(Skill NewSkill)
    {
        skill = NewSkill;
    }
}
