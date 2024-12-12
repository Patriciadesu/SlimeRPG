using UnityEngine;
using UnityEngine.Events;

public abstract class State
{
    public UnityEvent onEventChanged;

    public enum STATE
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }

    public STATE _state;
    protected EVENT _event;

    protected Enemy thisChar;
    protected NormalAttack normalAttack;
    protected ActiveSkill[] skills;
    protected float chaseDistance;

    protected State nextState;

    public State(Enemy _thisChar, NormalAttack _normalAttack, ActiveSkill[] _skills, float _chaseDistance)
    {
        thisChar = _thisChar;
        normalAttack = _normalAttack;
        skills = _skills;
        chaseDistance = _chaseDistance;
    }

    protected abstract void Enter();

    protected abstract void Update();

    protected abstract void Exit();

    protected State Process()
    {
        switch (_event)
        {
            case EVENT.ENTER:
                break;
            case EVENT.UPDATE:
                break;
            case EVENT.EXIT:
                Exit();
                onEventChanged?.Invoke();
                return nextState;
        }

        return this;
    }
}
