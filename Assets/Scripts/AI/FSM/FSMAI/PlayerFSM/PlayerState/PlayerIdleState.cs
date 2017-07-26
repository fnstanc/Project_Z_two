using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : FSMState
{
    protected float sayCoolDown = 10f;
    protected List<string> words = new List<string>();

    protected EntityDynamicActor dyAgent;

    public PlayerIdleState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
        words.Add("死亡如风 常伴吾身!");
        words.Add("钟鼓馔玉不足贵,但愿长醉不复醒!钟鼓馔玉不足贵,但愿长醉不复醒!");
        words.Add("zzzzzzzzzzzz! ! !");
    }

    public override void onEnter()
    {
        base.onEnter();
        if (dyAgent != null)
        {
            dyAgent.anim.CrossFade("idle", 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Loop;
            dyAgent.activeWeaponTrail(false);
        }
    }

    public override void onUpdate()
    {
        sayCoolDown -= Time.deltaTime;
        if (sayCoolDown < 0)
        {
            sayCoolDown = 10f;
            int index = UnityEngine.Random.Range(0, 3);
            if (dyAgent != null)
            {
                dyAgent.sayWord(words[index]);    
            }
        }
    }

    public override bool isCanChangeTo(StateType type)
    {
        return type == StateType.idle ? false : true;
    }
}

