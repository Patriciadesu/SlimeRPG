using UnityEngine;

public class Attack : State
{
    public Attack(Enemy _thisChar, NormalAttack _normalAttack, ActiveSkill[] _skills, float _chaseDistance) : base(_thisChar, _normalAttack, _skills, _chaseDistance)
    {

    }

    protected override void Enter()
    {
        _event = EVENT.UPDATE;
    }

    protected override void Exit()
    {
        var enemyPos = thisChar.transform.position;
        var plrPos = Player.Instance.transform.position;

        float distance = (plrPos - enemyPos).magnitude;

        if (distance <= chaseDistance)
        {
            nextState = new Patrol(thisChar);
            _event = EVENT.EXIT;
        }
    }

    protected override void Update()
    {
        _event = EVENT.UPDATE;
    }
}