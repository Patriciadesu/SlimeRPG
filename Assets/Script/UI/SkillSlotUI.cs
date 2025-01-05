using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    public static SkillSlotUI Instance;
    [SerializeField] private Image normalAttackImg;
    [SerializeField] private Image activeSkill1Img;
    [SerializeField] private Image activeSkill2Img;
    [SerializeField] private Image mobilitySkillImg;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public void SetSkillImage(Skill skill, Player.SkillSlotType skillSlotType)
    {
        switch (skillSlotType)
        {
            case Player.SkillSlotType.NormalAttack:
                if (skill is NormalAttack)
                {
                    normalAttackImg.sprite = skill.SkillSprite;
                }
                break;
            case Player.SkillSlotType.ActiveSkill1:
                if (skill is ActiveSkill)
                {
                    activeSkill1Img.sprite = skill.SkillSprite;
                }
                break;
            case Player.SkillSlotType.ActiveSkill2:
                if (skill is ActiveSkill)
                {
                    activeSkill2Img.sprite = skill.SkillSprite;
                }
                break;
            case Player.SkillSlotType.Mobility:
                if (skill is Mobility)
                {
                    mobilitySkillImg.sprite = skill.SkillSprite;
                }
                break;
        }
    }
}
