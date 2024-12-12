using UnityEngine;

public class Attack : State
{
    public Attack(Enemy _enemy, float _chaseDistance, float _attackDistance) : base(_enemy, _chaseDistance, _attackDistance)
    {

    }

    protected override void Enter()
    {
        enemy.Move(Vector2.zero);
        enemy.Attack();

        base.Enter();
    }

    protected override void Exit()
    {
        base.Exit();
    }

    protected override void Update()
    {
        base.Update();

        // wait for animation ended
        // set nextState value
        if (runTime >= 3)
        {
            var enemyPos = enemy.transform.position;
            var plrPos = Player.Instance.transform.position;

            float distance = (plrPos - enemyPos).magnitude;

            if (distance <= chaseDistance)
            {
                nextState = new Chase(enemy, chaseDistance, attackDistance);
                _event = EVENT.EXIT;
            }
            else
            {
                nextState = new Patrol(enemy, chaseDistance, attackDistance);
                _event = EVENT.EXIT;
            }
        }
    }
}