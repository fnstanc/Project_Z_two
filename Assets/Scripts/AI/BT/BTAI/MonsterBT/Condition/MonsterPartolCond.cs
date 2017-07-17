using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPartolCond : BTConditionNode
{


    public override bool check(WorkingData wd)
    {
        return wd.dyAgent.SType != StateType.onHit;
    }

}
