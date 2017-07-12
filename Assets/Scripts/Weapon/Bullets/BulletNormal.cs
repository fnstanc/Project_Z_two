using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletNormal : BaseBullet
{
    private float speed = 1;
    private float lifeTime = 0;
    private Vector3 dir = Vector3.zero;

    public override void onUpdate()
    {
        if (isInit)
        {
            this.CacheTrans.Translate(dir * speed);
        }
        if (Time.timeSinceLevelLoad >= lifeTime)
        {
            GameObject.DestroyObject(this.gameObject);
        }
    }

    protected override void initBullet()
    {
        BoxCollider col = this.CacheObj.AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = this.bulletInfo.boxSize;
        speed = this.bulletInfo.bulletSpeed;
        dir = this.bulletInfo.dir.normalized;
        this.CacheTrans.position = new Vector3(this.bulletInfo.dyAgent.CacheTrans.position.x, this.bulletInfo.dyAgent.CacheTrans.position.y + 1.3f, this.bulletInfo.dyAgent.CacheTrans.position.z);//this.bulletInfo.dyAgent.CacheTrans.position;
        lifeTime = Time.timeSinceLevelLoad + (float)this.bulletInfo.lifeTime;
        EffectMgr.Instance.createEffect(30001, new EffectInfo(Vector3.zero, this.CacheTrans));
    }

    public override void onBulletCrash(Collider col)
    {
        EntityDynamicActor atkTarget = col.GetComponent<EntityDynamicActor>();
        if (atkTarget != null && atkTarget.UID != this.bulletInfo.dyAgent.UID)
        {
            atkTarget.onDamage(11);
            this.bulletInfo.dyAgent.Target = atkTarget;
            this.bulletInfo.dyAgent.onChangeState(StateType.yasuoRSkill);
        }
    }

}

