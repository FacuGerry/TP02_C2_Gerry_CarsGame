using UnityEngine;
using UnityEngine.AI;

public abstract partial class EnemyStates 
{
    public StateType state;
    protected Animator _anim;
    protected Rigidbody _rb;
    protected NpcController _patrol;
    protected NavMeshAgent _agent;
    protected GameObject _player;

    protected static readonly int _state = Animator.StringToHash("State");

    public virtual void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, NavMeshAgent agent, GameObject player)
    {
        _anim = animator;
        _rb = rigidbody;
        _patrol = npcPatrol;
        _agent = agent;
        _player = player;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Enter to " + state);
    }

    public virtual void OnUpdate() { }

    public virtual void OnExit()
    {
        Debug.Log("Exit from " + state);
    }
}