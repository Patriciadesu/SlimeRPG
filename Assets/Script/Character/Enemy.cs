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

    public int enemyID;

    [SerializeField] private ActiveSkill[] skills;
    [SerializeField] private float chaseDistance;
    [SerializeField] private string rewardID;

    private State state;

    private void Awake()
    {
        state = new Patrol(this, chaseDistance);
    }

    private void FixedUpdate()
    {
        state = state.Process();
    }

    public void Move(Vector2 velocity)
    {
        rb2D.linearVelocity = velocity;
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

    protected override void Die()
    {
        base.Die();
    }
}
