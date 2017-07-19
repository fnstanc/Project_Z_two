using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    none = -1,
    idle,
    run,
    onHit,
    die,
    spawn, 
    skill,
    dodge,//闪避
    baseCombo,

    //ExtendState
    yasuoRSkill = 100,
}

//状态基类
public abstract class FSMState
{
    protected StateType SType;
    protected BaseEntity agent;
    protected FSMArgs args;


    public FSMState(BaseEntity agent, StateType type)
    {
        this.agent = agent;
        this.SType = type;
        init();
    }

    //1赋值状态类型(构造的时候赋值也好) 2添加可切换列表
    public virtual void init() { }


    public virtual void onEnter()
    {
        Debug.Log(this.agent.UID + "<color=yellow>   进入状态 ->  </color>" + this.SType.ToString());
    }

    public virtual void onUpdate()
    {


    }

    public virtual void onExit()
    {
        Debug.Log(this.agent.UID + "<color=red>   退出状态 ->  </color>" + this.SType.ToString());
    }

    public virtual void setArgs(FSMArgs args)
    {
        this.args = args;
    }

    protected virtual void onRelease()
    {
        EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
        if (dyAgent != null)
            dyAgent.onChangeState(StateType.idle);
    }

    //beHit -> attack 
    //当前状态是否可以切换到B状态
    public virtual bool isCanChangeTo(StateType type)
    {
        bool isCan = true;

        return isCan;
    }
}

