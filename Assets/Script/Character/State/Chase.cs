using UnityEngine;

public class Chase : State
{
    public Chase(Enemy _enemy, float _chaseDistance, float _attackDistance) : base(_enemy, _chaseDistance, _attackDistance)
    {

    }

    protected override void Enter()
    {
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

        var direction = (plrPos - enemyPos).normalized;
        float distance = (plrPos - enemyPos).magnitude;

        if (distance <= attackDistance)
        {
            nextState = new Attack(enemy, chaseDistance, attackDistance);
            _event = EVENT.EXIT;
        }
        else if (distance <= chaseDistance)
        {
            enemy.Move(direction);
        }
        else
        {
            nextState = new Idle(enemy, chaseDistance, attackDistance);
            _event = EVENT.EXIT;
        }
    }
}