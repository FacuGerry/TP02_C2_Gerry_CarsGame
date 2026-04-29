using UnityEngine;

public class StateIdle : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, GameObject player)
    {
        base.Initialize(animator, rigidbody, npcPatrol, player);
        state = StateType.Idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }
}
