using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, SLEEP
    };

    public enum EVENT 
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;
    protected State nextState;
    private readonly float visibleRange = 10.0f;
    private readonly float visibleAngle = 30.0f;
    private readonly float attackableDistance = 7.0f;

    public State(GameObject npc, NavMeshAgent agent, Animator animator, Transform player)
    {
        this.npc = npc;
        this.agent = agent;
        this.animator = animator;
        this.player = player;
        stage = EVENT.ENTER;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }
    
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }
    
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public virtual State Process()
    {
        if (stage == EVENT.ENTER)
            Enter();

        if (stage == EVENT.UPDATE)
            Update();

        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public virtual bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.position);

        if (direction.magnitude < visibleRange && angle < visibleAngle)
        {
            return true;
        }

        return false;
    }

    public virtual bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        if (direction.magnitude < attackableDistance)
        {
            return true;
        }

        return false;
    }
}
