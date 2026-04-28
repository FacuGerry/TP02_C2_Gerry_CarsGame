using UnityEngine;
using UnityEngine.AI;

public class StateIdle : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, NavMeshAgent agent, GameObject player)
    {
        base.Initialize(animator, rigidbody, npcPatrol, agent, player);
        state = StateType.Idle;
    }
}
