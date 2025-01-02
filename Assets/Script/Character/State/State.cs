using UnityEngine;
using UnityEngine.Events;

public abstract class State
{
    public UnityEvent onEventChanged;
    public UnityAction<Vector2> onMoveTick;

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

    protected EVENT _event = EVENT.ENTER;
    protected float runTime;

    protected Enemy enemy;
    protected float chaseDistance;
    protected float attackDistance;
    protected Rigidbody2D rigidbody;
    protected Animator animator;

    protected State nextState;

    public State(Enemy _enemy, float _chaseDistance, float _attackDistance)
    {
        enemy = _enemy;
        chaseDistance = _chaseDistance;
        attackDistance = _attackDistance;
        rigidbody = _enemy.GetComponent<Rigidbody2D>();
        animator = _enemy.GetComponent<Animator>();
    }

    protected virtual void Enter()
    {
        _event = EVENT.UPDATE;
        //Debug.Log("ENTER STATE " + GetType().Name);
    }

    protected virtual void Update()
    {
        _event = EVENT.UPDATE;
    }

    protected virtual void Exit()
    {
        _event = EVENT.UPDATE;
        //Debug.Log("EXIT STATE " + GetType().Name);
    }

    public State Process()
    {
        switch (_event)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                onEventChanged?.Invoke();
                return nextState;
        }

        runTime += Time.deltaTime;

        return this;
    }
}
