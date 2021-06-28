using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private State _currentState;
    public Transform player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentState = new Idle(gameObject, _agent, _animator, player);
    }

    private void Update()
    {
        _currentState = _currentState.Process(); 
    }
}
