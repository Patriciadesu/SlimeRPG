using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    public Skill[] skills { get; private set; }


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
        if (skill != null)
            yield return skill.OnUse();
        //yield return StartCoroutine(skill.OnUse());
    }

    public IEnumerator UseSkill(Enemy enemy, EnemyAttack skill)
    {
        Debug.Log($"{enemy.name} use skill {skill.Name}");
        if (enemy != null && skill != null)
            yield return skill.OnUse(enemy);
        //yield return StartCoroutine(skill.OnUse());
    }

    public Skill GetSkillByID(string ID){
        if (skills == null || skills.Length == 0)
        {
            Debug.LogError("Item Data is empty.");
            return null;
        }

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].skillID == ID)
            {
                return skills[i];
            }
        }
        
        Debug.LogError("This Item ID does not exist in the data.");
        return null;
    }

}
