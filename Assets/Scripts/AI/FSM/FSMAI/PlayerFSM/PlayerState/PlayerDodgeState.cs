using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : FSMState
{
    private float moveDis = 6f;
    private Vector3 moveDir = Vector3.zero;//enter获取摇杆数据
    private Vector3 orgPos = Vector3.zero;
    private int effectId = -1;
    protected EntityDynamicActor dyAgent;

    public PlayerDodgeState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        if (dyAgent != null)
        {
            dyAgent.sayWord("闪现 zzz !!!", true);
        }
        moveDir = dyAgent.CacheTrans.forward;
        orgPos = dyAgent.CacheTrans.position;
        effectId = EffectMgr.Instance.createEffect(this.args.skillData.skillEffectID, new EffectInfo(Vector3.zero, this.dyAgent.CacheTrans));
    }

    public override void onUpdate()
    {
        base.onUpdate();
        if ((orgPos - dyAgent.CacheTrans.position).magnitude < moveDis)
        {
            dyAgent.CacheTrans.Translate(moveDir, Space.World);
        }
        else
        {
            Debug.Log(Vector3.Distance(orgPos, dyAgent.CacheTrans.position));
            onRelease();
        }
    }

    public override void onExit()
    {
        base.onExit();
        if (effectId != -1)
        {
            EffectMgr.Instance.disposeEffect(effectId);
            Debug.Log("释放effect");
        }
    }



}

