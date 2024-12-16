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
    }

    private void Update()
    {
        state = state.Process();
    }

    public void Move(Vector2 velocity)
    {
        rb2D.linearVelocity = Vector2.Lerp(rb2D.linearVelocity, velocity.normalized * speed * 2.5f, Time.fixedDeltaTime * 5);

        float currentX = transform.rotation.eulerAngles.x;
        float currentZ = transform.rotation.eulerAngles.z;

        if (velocity.x > 0)
            transform.rotation = Quaternion.Euler(currentX, 0, currentZ);
        else if (velocity.x < 0)
            transform.rotation = Quaternion.Euler(currentX, 180, currentZ);
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
