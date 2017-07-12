using UnityEngine;
using System.Collections;

public class BTActionNode : BTLeaf
{
    private ActionStatus actStatus = ActionStatus.ready;

    sealed public override BTResult onTick(WorkingData wd)
    {
        BTResult result = BTResult.running;
        if (actStatus == ActionStatus.ready)
        {
            onEnter(wd);
            actStatus = ActionStatus.running;
        }
        if (actStatus == ActionStatus.running)
        {
            actStatus = onUpdate(wd);
            if (actStatus == ActionStatus.finished)
            {
                onExit(wd);
                actStatus = ActionStatus.ready;
                result = BTResult.success;
            }
        }
        return result;
    }

    public virtual void onEnter(WorkingData wd)
    {
    }
    public virtual ActionStatus onUpdate(WorkingData wd)
    {
        return ActionStatus.finished;
    }
    public virtual void onExit(WorkingData wd)
    {

    }
}
