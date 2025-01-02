using UnityEngine;

public class Attack : State
{
    public Attack(Enemy _enemy, float _chaseDistance, float _attackDistance) : base(_enemy, _chaseDistance, _attackDistance)
    {

    }

    protected override void Enter()
    {
        onMoveTick?.Invoke(Vector2.zero);
        animator.SetBool("isWalking", false);

        onAttackTick?.Invoke();

        base.Enter();
    }

    protected override void Exit()
    {
        base.Exit();
    }

    protected override void Update()
    {
        base.Update();

        if (enemy.canAttack || runTime > 2.5f)
        {
            if (Player.Instance == null)
            {
                nextState = new Patrol(enemy, chaseDistance, attackDistance);
                _event = EVENT.EXIT;
                return;
            }

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
        else onMoveTick?.Invoke(Vector2.zero);
    }
}