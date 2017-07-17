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

    public override void onDamage(DamageData dt)
    {
        if (dt.damage != 0)
        {
            this.HP -= dt.damage;
            Message msg = new Message(MsgCmd.On_Take_Damage, this);
            msg["data"] = dt;
            msg.Send();
        }
        EffectMgr.Instance.createEffect(40003, new EffectInfo(new Vector3(0, 1.6f, 0), this.CacheTrans));
        this.onChangeColor();
        if (this.HP <= 0)
        {
            clear();
            onChangeState(StateType.die);
        }
        else
        {
            onChangeState(StateType.onHit, new FSMArgs(dt));
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

    public override void onReSpawn()
    {
        base.onReSpawn();
        this.CC.enabled = true;
        this.onChangeState(StateType.spawn);
    }

    public override void onDispose()
    {
        base.onDispose();
        this.onChangeState(StateType.idle);
        this.fsm = null;
    }

}

