using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntityMonster : EntityDynamicActor
{

    protected BTTree treeAI;

    public override void onStart()
    {
        base.onStart();
        this.CacheObj.layer = 10;
    }

    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        fsm = new MonsterFSM(this);
        onChangeState(StateType.spawn);
        treeAI = new MonsterBTAI();
        dt = WorkingDataFactroy.createData(this);
    }
    private WorkingData dt;
    public override void onUpdate()
    {
        base.onUpdate();
        if (treeAI != null)
            treeAI.root.onTick(dt);
    }

    public override void onDamage(float damage)
    {
        this.HP -= damage;
        if (this.HP <= 0)
        {
            onChangeState(StateType.die);
            Message msg = new Message(MsgCmd.On_Change_Value, this);
            msg["type"] = BType.money;
            msg["val"] = 10 * (int)this.SonType;
            msg.Send();
            Message msg2 = new Message(MsgCmd.On_Change_Value, this);
            msg2["type"] = BType.score;
            msg2["val"] = 10 * (int)this.SonType;
            msg2.Send();
            Message msg3 = new Message(MsgCmd.On_Change_Value, this);
            msg3["type"] = BType.energy;
            msg3["val"] = 100 * (int)this.SonType;
            msg3.Send();
            BaseEntity agent = EntityMgr.Instance.getEntityById(1008611);
            EffectInfo info = new EffectInfo(this.CacheTrans.position, agent);
            EffectMgr.Instance.createEffect(20001, info);
            clear();
        }
        else
        {
            onChangeState(StateType.onHit);
        }
    }

    public void onChangeColor(string colorName, Color color)
    {
        this.GetComponentInChildren<MeshRenderer>().material.SetColor(colorName, color);
    }

    private void clear()
    {
        this.CC.enabled = false;
    }

    public override void onDispose()
    {
        base.onDispose();
        this.onChangeState(StateType.idle);
        this.fsm = null;
    }

}

