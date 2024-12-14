using System.Collections;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    [SerializeField] private Skill[] skills;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        skills = Resources.LoadAll<Skill>("Skill");

        foreach (Skill skill in skills)
        {
            skill.isActive = true;
        }

        Instance = this;
    }

    public IEnumerator UseSkill(Skill skill)
    {
        yield return skill.OnUse();
        //yield return StartCoroutine(skill.OnUse());
    }

    public IEnumerator UseSkill(Enemy enemy, Skill skill)
    {
        yield return skill.OnUseForEnemy(enemy);
        //yield return StartCoroutine(skill.OnUse());
    }
}
