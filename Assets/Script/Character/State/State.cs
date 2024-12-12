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
    protected float runTime;

    protected Enemy enemy;
    protected float chaseDistance;
    protected Rigidbody2D rigidbody;

    protected State nextState;

    public State(Enemy _enemy, float _chaseDistance)
    {
        enemy = _enemy;
        chaseDistance = _chaseDistance;
        rigidbody = _enemy.GetComponent<Rigidbody2D>();
    }

    protected virtual void Enter()
    {
        _event = EVENT.UPDATE;
        Debug.Log("ENTER STATE");
    }

    protected virtual void Update()
    {
        _event = EVENT.UPDATE;
    }

    protected virtual void Exit()
    {
        _event = EVENT.UPDATE;
        Debug.Log("EXIT STATE");
    }

    public State Process()
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

        runTime += Time.fixedDeltaTime;

        return this;
    }
}
