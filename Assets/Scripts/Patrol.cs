using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex = -1;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator animator, Transform player)
        : base(npc, agent, animator, player)
    {
        name = STATE.PATROL;
        this.agent.speed = 2;
        this.agent.isStopped = false;
    }


    public override void Enter()
    {
        currentIndex = 0;
        animator.SetTrigger("isWalking");
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(
                GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
        }

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isWalking");
        base.Exit();
    }
}
