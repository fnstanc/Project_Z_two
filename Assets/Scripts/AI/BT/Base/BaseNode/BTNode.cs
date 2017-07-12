using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionStatus
{
    ready = 1,
    running,
    finished,
}

public enum BTResult
{
    success = 1,
    running,
    failure,
}

public class BTNode
{


    public virtual BTResult onTick(WorkingData wd)
    {
        return BTResult.success;
    }

    public virtual void addChild(BTNode node)
    {

    }
    public virtual void removeChild(BTNode node)
    {

    }

    public virtual void clear()
    {

    }

}
