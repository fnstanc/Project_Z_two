using System;
using System.Collections.Generic;
using ChuMeng;

public class WorkingDataFactroy
{
    public static WorkingData createData(BaseEntity agent)
    {
        WorkingData wd = new WorkingData();
        WorkingDataConfigData dt = getConfigById(agent.getWorkingDataId());
        wd.dyAgent = agent as EntityDynamicActor;
        wd.orgPos = agent.CacheTrans.position;
        wd.seekTime = (float)dt.seekTime;
        wd.seekRange = (float)dt.seekRange;
        wd.partolRange = (float)dt.partolRange;
        return wd;
    }

    private static WorkingDataConfigData getConfigById(int id)
    {
        for (int i = 0; i < GameData.WorkingDataConfig.Count; i++)
        {
            if (GameData.WorkingDataConfig[i].tempId == id)
                return GameData.WorkingDataConfig[i];
        }
        return null;
    }

}

