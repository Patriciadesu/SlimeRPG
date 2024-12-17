using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField] protected int level = 1;
    public Sprite SkillSprite;
    public string Name = string.Empty;
    public string Description = string.Empty;
    public string skillID;
    public bool Have = false;
    [SerializeField] private float _price = 0f;
    public float Price { get => _price; }
    public float coolDown;
    public bool isActive;

    public static T GetSkillByLevel<T>(int level) where T : Skill
    {
        T[] skills;

        if (SkillManager.Instance != null)
            skills = SkillManager.Instance.skills as T[];
        else
            skills = Resources.LoadAll<T>("Skill");

        return skills.FirstOrDefault(s => s.level == level);
    }

    public static T GetMaxLevel<T>(bool have) where T : Skill
    {
        T[] skills;

        if (SkillManager.Instance != null)
            skills = SkillManager.Instance.skills as T[];
        else
            skills = Resources.LoadAll<T>("Skill");

        if (have)
            return skills.Where(s => s.Have).OrderByDescending(s => s.level).FirstOrDefault();
        else
            return skills.OrderByDescending(s => s.level).FirstOrDefault();
    }

    public abstract IEnumerator OnUse();
}
