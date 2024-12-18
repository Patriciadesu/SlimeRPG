using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public int Level { get => level; }
    [SerializeField] protected int level = 1;

    public Sprite SkillSprite;
    public string Name = string.Empty;
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

    public static Skill GetMaxLevel(System.Type skillType, bool have)
    {
        Skill[] skills;

        if (SkillManager.Instance != null)
            skills = SkillManager.Instance.skills.Where(s => s.GetType() == skillType).ToArray();
        else
            skills = Resources.LoadAll<Skill>("Skill").Where(s => s.GetType() == skillType).ToArray();

        if (have)
            return skills.Where(s => s.Have).OrderByDescending(s => s.level).FirstOrDefault();
        else
            return skills.OrderByDescending(s => s.level).FirstOrDefault();
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

    public static T[] GetAllLevels<T>(bool have) where T : Skill
    {
        T[] skills;

        if (SkillManager.Instance != null)
            skills = SkillManager.Instance.skills as T[];
        else
            skills = Resources.LoadAll<T>("Skill");

        if (have)
            return skills.Where(s => s.Have).OrderBy(s => s.level).ToArray();
        else
            return skills.OrderBy(s => s.level).ToArray();
    }

    public static Skill[] GetAllLevels(System.Type skillType, bool have)
    {
        Skill[] skills;

        if (SkillManager.Instance != null)
            skills = SkillManager.Instance.skills.Where(s => s.GetType() == skillType).ToArray();
        else
            skills = Resources.LoadAll<Skill>("Skill").Where(s => s.GetType() == skillType).ToArray();

        if (have)
            return skills.Where(s => s.Have).OrderBy(s => s.level).ToArray();
        else
            return skills.OrderBy(s => s.level).ToArray();
    }

    public abstract IEnumerator OnUse();
}
