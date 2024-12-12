using UnityEngine;

public class Chase : State
{
    public Chase(Enemy _enemy, float _chaseDistance) : base(_enemy, _chaseDistance)
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

        enemy.Move(direction);
    }
}