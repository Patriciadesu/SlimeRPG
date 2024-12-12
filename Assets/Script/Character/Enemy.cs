using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy closestEnemy
    {
        get
        {
            var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            var plrPos = Player.Instance.transform.position;
            float distance = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach (var enemy in enemies)
            {
                var enemyPos = enemy.transform.position;
                enemyPos.z = 0;

                float newDistance = (plrPos - enemyPos).magnitude;

                if (newDistance < distance)
                {
                    distance = newDistance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }

    public string ID { get { return _id; } }
    [SerializeField] private string _id;

    [SerializeField] private ActiveSkill[] skills;
    [SerializeField] private string rewardID;

    private State state;

    private void Awake()
    {
        state = new Patrol();
    }

    private void FixedUpdate()
    {

    }
}
