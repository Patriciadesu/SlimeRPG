using UnityEngine;

public class Patrol : State
{
    private float maxPatrolTime;
    private Vector2 patrolPos;

    public Patrol(Enemy _enemy, float _chaseDistance) : base(_enemy, _chaseDistance)
    {

    }

    protected override void Enter()
    {
        maxPatrolTime = Random.Range(1f, 3f);
        patrolPos = RandomPatrolPosition();
        base.Enter();
    }

    protected override void Exit()
    {
        base.Exit();
    }

    protected override void Update()
    {
        base.Update();


    }

    private Vector2 RandomPatrolPosition()
    {
        return Vector2.zero;
    }
}
