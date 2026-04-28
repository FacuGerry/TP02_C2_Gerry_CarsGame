using UnityEngine;
using UnityEngine.AI;

public class StateShoot : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, NavMeshAgent agent, GameObject player)
    {
        base.Initialize(animator, rigidbody, npcPatrol, agent, player);
        state = StateType.Shoot;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (_agent.isOnNavMesh)
            _agent.isStopped = true;
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        if (_rb.linearVelocity != Vector3.zero)
            _rb.linearVelocity = Vector3.zero;
        _patrol.EnableShooting(true);

        Vector3 playerPos = _player.transform.position;

        Quaternion lookAt = Quaternion.LookRotation(_player.transform.position - _patrol.transform.position);
        Quaternion correction = Quaternion.Euler(0, 45, 0);
        _patrol.transform.rotation = lookAt * correction;
    }

    public override void OnExit()
    {
        base.OnExit();
        _patrol.EnableShooting(false);
        _agent.isStopped = false;
    }
}