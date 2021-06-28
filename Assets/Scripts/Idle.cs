using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject npc, NavMeshAgent agent, Animator animator, Transform player)
        : base(npc, agent, animator, player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        animator.SetTrigger("isIdle"); 
        base.Enter();
    }
    
    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 100) < 10)
        {
            nextState = new Patrol(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}
