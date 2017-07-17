using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : FSMState
{
    protected EntityDynamicActor dyAgent;
    private float respawnTime = 6f;//复活时间
    private float exitTime = 0;

    public PlayerDieState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        if (dyAgent != null)
        {
            dyAgent.sayWord("德玛西亚 永世长存!");
            dyAgent.anim.CrossFade(getRandomDieAnim(), 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Once;
        }
        exitTime = Time.timeSinceLevelLoad + respawnTime;
    }

    public override void onUpdate()
    {
        if (Time.timeSinceLevelLoad > exitTime)
        {
            this.dyAgent.onReSpawn();
        }
    }

    private string getRandomDieAnim()
    {
        int index = UnityEngine.Random.Range(1, 4);
        return string.Format("{0}{1}", "die", index);
    }

    public override bool isCanChangeTo(StateType type)
    {
        return type == StateType.spawn ? true : false;
    }

}

