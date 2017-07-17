using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPartolAct : BTActionNode
{
    private EntityDynamicActor dyAgent;

    private float nextSeekTime;
    private Vector3 nextPartolPoint;
    private float partolTime = 100;
    private float nextPartolTime = 0;
    private bool isDoPartol = false;
    public bool IsDoPartol
    {
        get
        {
            return this.isDoPartol;
        }
        set
        {
            if (this.isDoPartol != value)
            {
                this.isDoPartol = value;
                if (this.isDoPartol)
                    this.dyAgent.onChangeState(StateType.run);
                else
                    this.dyAgent.onChangeState(StateType.idle);
            }
        }
    }

    public override bool canDo(WorkingData wd)
    {
        return wd.dyAgent.SType != StateType.onHit;
    }

    public override void onEnter(WorkingData wd)
    {
        dyAgent = wd.dyAgent;
        nextSeekTime = wd.seekTime + Time.timeSinceLevelLoad;
        nextPartolPoint = getPartolPoint(wd);
    }

    public override ActionStatus onUpdate(WorkingData wd)
    {

        if (Time.timeSinceLevelLoad >= nextSeekTime && wd.dyAgent.Target == null)
        {
            List<BaseEntity> players = EntityMgr.Instance.getEntityByType(EntityType.player);
            if (players != null && players.Count > 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (Vector3.Distance(players[i].CacheTrans.position, wd.dyAgent.CacheTrans.position) <= wd.seekRange)
                    {
                        wd.dyAgent.Target = players[i];
                        return ActionStatus.finished;
                    }
                }
            }
            nextSeekTime = wd.seekTime + Time.timeSinceLevelLoad;
        }

        if (wd.dyAgent.SType == StateType.onHit)
            return ActionStatus.finished;

        if (Time.timeSinceLevelLoad >= nextPartolTime)
        {
            if (Vector3.Distance(wd.dyAgent.CacheTrans.position, nextPartolPoint) > 1f)
            {
                this.dyAgent.onChangeState(StateType.run);
                wd.dyAgent.CacheTrans.LookAt(nextPartolPoint, Vector3.up);
                wd.dyAgent.moveTo((nextPartolPoint - wd.dyAgent.CacheTrans.position).normalized);
            }
            else
            {
                nextPartolPoint = getPartolPoint(wd);
                nextPartolTime = partolTime + Time.timeSinceLevelLoad;
            }
        }
        else
        {
            this.dyAgent.onChangeState(StateType.idle);
        }
        return ActionStatus.running;
    }

    public override void onExit(WorkingData wd)
    {

    }




    //get partol point
    public Vector3 getPartolPoint(WorkingData wd)
    {
        float xz = UnityEngine.Random.Range(-wd.partolRange, wd.partolRange);
        return new Vector3(wd.orgPos.x + xz, wd.orgPos.y, wd.orgPos.z + xz);
    }

}

