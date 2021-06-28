using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public Pursue(GameObject npc, NavMeshAgent agent, Animator animator, Transform player)
        : base(npc, agent, animator, player)
    {
        name = STATE.PURSUE;
        agent.speed = 5f;
        agent.isStopped = false; 
    }


    public override void Enter()
    {
        animator.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, animator, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, animator, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isRunning");
        base.Exit();
    }
}
