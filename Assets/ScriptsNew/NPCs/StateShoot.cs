using UnityEngine;

public class StateShoot : EnemyStates
{
    public override void Initialize(Animator animator, Rigidbody rigidbody, NpcController npcPatrol, GameObject player)
    {
        base.Initialize(animator, rigidbody, npcPatrol, player);
        state = StateType.Shoot;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _anim.SetInteger(_state, (int)state);
    }

    public override void OnUpdate()
    {
        _patrol.EnableShooting(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        _patrol.EnableShooting(false);
    }
}