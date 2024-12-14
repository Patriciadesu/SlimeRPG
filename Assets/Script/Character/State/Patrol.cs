using Unity.VisualScripting;
using UnityEngine;

public class Patrol : State
{
    private PolygonCollider2D spawnCollider;
    private float maxPatrolTime;
    private Vector2 patrolPos;

    public Patrol(Enemy _enemy, float _chaseDistance, float _attackDistance) : base(_enemy, _chaseDistance, _attackDistance)
    {

    }

    protected override void Enter()
    {
        spawnCollider = enemy.transform.parent.GetComponent<PolygonCollider2D>();
        maxPatrolTime = Random.Range(2f, 5f);
        patrolPos = RandomPatrolPosition();

        animator.SetBool("isWalking", true);

        base.Enter();
    }

    protected override void Exit()
    {
        base.Exit();
    }

    protected override void Update()
    {
        base.Update();

        var enemyPos = enemy.transform.position;
        var direction = patrolPos - enemyPos.ConvertTo<Vector2>();

        if (Player.Instance != null)
        {
            var plrPos = Player.Instance.transform.position;

            float distance = (plrPos - enemyPos).magnitude;

            if (distance <= chaseDistance)
            {
                nextState = new Chase(enemy, chaseDistance, attackDistance);
                _event = EVENT.EXIT;
            }
        }
        else if (runTime <= maxPatrolTime && direction.magnitude > 0.05f)
        {
            enemy.Move(direction);
        }
        else
        {
            nextState = new Idle(enemy, chaseDistance, attackDistance);
            _event = EVENT.EXIT;
        }
    }

    private Vector2 RandomPatrolPosition()
    {
        if (spawnCollider == null || spawnCollider.points.Length == 0) return Vector3.zero;

        Vector2 randomPoint;
        do
        {
            Vector2 min = spawnCollider.bounds.min;
            Vector2 max = spawnCollider.bounds.max;
            randomPoint = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        } while (!IsPointInPolygon(randomPoint, spawnCollider.points, spawnCollider.transform));

        return randomPoint;
    }

    private bool IsPointInPolygon(Vector2 point, Vector2[] polygonPoints, Transform polygonTransform)
    {
        Vector2 localPoint = polygonTransform.InverseTransformPoint(point);
        int j = polygonPoints.Length - 1;
        bool inside = false;

        for (int i = 0; i < polygonPoints.Length; i++)
        {
            if (polygonPoints[i].y < localPoint.y && polygonPoints[j].y >= localPoint.y || polygonPoints[j].y < localPoint.y && polygonPoints[i].y >= localPoint.y)
            {
                if (polygonPoints[i].x + (localPoint.y - polygonPoints[i].y) / (polygonPoints[j].y - polygonPoints[i].y) * (polygonPoints[j].x - polygonPoints[i].x) < localPoint.x)
                {
                    inside = !inside;
                }
            }
            j = i;
        }

        return inside;
    }
}
