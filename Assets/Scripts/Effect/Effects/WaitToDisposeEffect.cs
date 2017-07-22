using System;
using System.Collections.Generic;

public class WaitToDisposeEffect : NormalEffect
{


    public override void onDispose()
    {
        this.CacheTrans.SetParent(null);
        TimeMgr.Instance.setTimerHandler(new TimerHandler(lifeTime, () =>
        {
            PoolMgr.Instance.saveObj(this.gameObject, this.info.config.tempId + this.info.config.path);
            lifeTime = orgTime;
            isSetInfo = false;
        }));

    }

}

