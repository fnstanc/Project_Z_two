using System;
using System.Collections.Generic;
using UnityEngine;


public class MonsterIdleState : PlayerIdleState
{
    public MonsterIdleState(BaseEntity agent, StateType type) : base(agent, type)
    {
        this.sayCoolDown = 20f;
        dyAgent = this.agent as EntityDynamicActor;
        words.Add("德玛西亚 永世长存");
        words.Add("秋水共长天一色,落霞与孤鹜齐飞! 秋水共长天一色,落霞与孤鹜齐飞!");
        words.Add("what are you fucking talking!");
    }
}

