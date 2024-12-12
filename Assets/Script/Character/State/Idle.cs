using System.Collections;
using UnityEngine;

public class Idle : State
{
    private float maxIdleTime;

    public Idle(Enemy _enemy, float _chaseDistance, float _attackDistance) : base(_enemy, _chaseDistance, _attackDistance)
    {

    }

    protected override void Enter()
    {
        maxIdleTime = Random.Range(1.5f, 5f);

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
        var plrPos = Player.Instance.transform.position;

        float distance = (plrPos - enemyPos).magnitude;

        if (distance <= chaseDistance)
        {
            nextState = new Chase(enemy, chaseDistance, attackDistance);
            _event = EVENT.EXIT;
        }
        else if (runTime <= maxIdleTime)
        {
            enemy.Move(Vector2.zero);
        }
        else
        {
            nextState = new Patrol(enemy, chaseDistance, attackDistance);
            _event = EVENT.EXIT;
        }
    }
}