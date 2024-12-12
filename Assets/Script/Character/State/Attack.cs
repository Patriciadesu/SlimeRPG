using UnityEngine;

public class Attack : State
{
    public Attack(Enemy _enemy, float _chaseDistance) : base(_enemy, _chaseDistance)
    {

    }

    protected override void Enter()
    {
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
            nextState = new Patrol(enemy, chaseDistance);
            _event = EVENT.EXIT;
        }
    }
}