using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityDropCall : EntityStaticActor
{
    private float lifeTime = 2f;
    private bool isDie = false;


    public override void onUpdate()
    {
        if (isDie) return;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            onLifeOver();
        }
    }

    public override void onCreateEnd()
    {
        //创建姓名版 血条等..
        BillBoard = this.CacheObj.AddComponent<DropCallBillBoard>();
        BillBoard.onCreate(this.info);
    }

    private void onLifeOver()
    {
        isDie = true;
        Vector3 pos = this.CacheTrans.position;
        EffectMgr.Instance.createEffect(20001, new EffectInfo(pos, EntityMgr.Instance.getMainPlayer()));
        EntityMgr.Instance.removeEntity(this);
    }



}
