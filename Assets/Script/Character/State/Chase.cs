using UnityEngine;

public class Chase : State
{
    public Chase(Enemy _thisChar, NormalAttack _normalAttack, ActiveSkill[] _skills, float _chaseDistance) : base(_thisChar, _normalAttack, _skills, _chaseDistance)
    {

    }

    protected override void Enter()
    {
        throw new System.NotImplementedException();
    }

    protected override void Exit()
    {
        throw new System.NotImplementedException();
    }

    protected override void Update()
    {
        throw new System.NotImplementedException();
    }
}