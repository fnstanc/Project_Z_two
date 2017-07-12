using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : FSMState
{
    private EntityDynamicActor dyAgent;

    public PlayerRunState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        if (dyAgent != null)
        {
            dyAgent.anim.CrossFade("walk", 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Loop;
        }
    }

    public override bool isCanChangeTo(StateType type)
    {
        return type == StateType.run ? false : true;
    }
}

