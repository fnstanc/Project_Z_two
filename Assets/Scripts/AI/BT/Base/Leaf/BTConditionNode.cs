using System;
using System.Collections.Generic;
public class BTConditionNode : BTLeaf
{
    sealed public override BTResult onTick(WorkingData wd)
    {
        return check(wd) ? BTResult.success : BTResult.failure;
    }

    public virtual bool check(WorkingData wd)
    {
        return false;
    }
}