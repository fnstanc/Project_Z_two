using System;
using System.Collections.Generic;

public class WorkingDataFactroy
{
    public static WorkingData createData(BaseEntity agent)
    {
        WorkingData wd = new WorkingData();
        WorkingDataConfigConfig dt = WorkingDataConfigConfig.Get(agent.getWorkingDataId());
        if (dt != null)
        {
            wd.dyAgent = agent as EntityDynamicActor;
            wd.orgPos = agent.CacheTrans.position;
            wd.seekTime = dt.seekTime;
            wd.seekRange = dt.seekRange;
            wd.partolRange = dt.partolRange;
        }
        return wd;
    }
}

