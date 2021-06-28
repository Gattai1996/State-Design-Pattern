using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    private readonly float rotationSpeed = 2f;
    private AudioSource shoot;

    public Attack(GameObject npc, NavMeshAgent agent, Animator animator, Transform player)
        : base(npc, agent, animator, player)
    {
        name = STATE.ATTACK;
        shoot = npc.GetComponent<AudioSource>();
    }


    public override void Enter()
    {
        animator.SetTrigger("isShooting");
        agent.isStopped = true;
        shoot.Play();
        base.Enter(); 
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0f;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, 
            Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isShooting");
        shoot.Stop();
        base.Exit();
    }
}
