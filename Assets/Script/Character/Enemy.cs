using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Character
{
    public static Enemy closestEnemy
    {
        get
        {
            var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            float distance = Mathf.Infinity;
            Enemy closestEnemy = null;

            var plrPos = Player.Instance.transform.position;
            plrPos.z = 0;

            foreach (var enemy in enemies)
            {
                var enemyPos = enemy.transform.position;
                enemyPos.z = 0;

                float newDistance = (enemyPos - plrPos).magnitude;

                if (newDistance < distance)
                {
                    distance = newDistance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }

    public string id;

    [SerializeField] private EnemyAttack[] skills;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private string rewardID;

    private State state;

    protected override void Awake()
    {
        base.Awake();

        chaseDistance = Mathf.Max(chaseDistance, attackDistance);

        state = new Patrol(this, chaseDistance, attackDistance);
        state.onMoveTick += Move;
    }

    private void Update()
    {
        state = state.Process();
    }

    public Skill Attack()
    {
        var skillCanUse = skills.ToList().Find(s => s.isActive);

        if (skillCanUse != null)
        {
            StartCoroutine(SkillManager.Instance.UseSkill(this, skillCanUse));
            return skillCanUse;
        }
        else if (normalAttack != null && normalAttack.isActive)
        {
            StartCoroutine(SkillManager.Instance.UseSkill(this, (EnemyAttack)normalAttack));
            return normalAttack;
        }

        return null;
    }

    protected override void Die()
    {
        RewardManager.Instance.GiveReward(rewardID);
        GameManager.OnEnemyKilled(this);
        Spawner spawner = transform.parent.GetComponent<Spawner>();
        if(spawner != null){
            spawner.storedEnemy.Remove(this);
        }

        base.Die();
    }
}
