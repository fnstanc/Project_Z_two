using System;
using System.Collections.Generic;
using UnityEngine;

public enum ATK_Type
{
    hitBack = 1,
    hitAir,
}

public enum BulletType
{
    normal = 1,
}

public class BulletInfo
{
    public EntityDynamicActor dyAgent;
    public Vector3 dir
    {
        get
        {
            return dyAgent.CacheTrans.forward;
        }
    }
    public Vector3 initPos
    {
        get
        {
            return dyAgent.CacheTrans.position;
        }
    }
    public Vector3 boxSize;
    public double lifeTime;
    public float bulletSpeed;
    public ATK_Type atkType;
    public BulletType bulletType;
    public float hitDis;
}

public class BulletFactroy
{
    public static void createBullet(EntityDynamicActor agent, int bulletId)
    {
        BulletInfo info = new BulletInfo();
        BulletConfigConfig dt = BulletConfigConfig.Get(bulletId);
        if (dt == null) return;
        info.dyAgent = agent;
        info.lifeTime = dt.bulletLife;
        info.bulletSpeed = (float)dt.bulletSpeed;
        info.atkType = (ATK_Type)dt.atkType;
        info.bulletType = (BulletType)dt.bulletType;
        info.hitDis = (float)dt.atkDistance;
        string[] strs = dt.bulletSize.Split(',');
        info.boxSize = new Vector3(float.Parse(strs[0]), float.Parse(strs[0]), float.Parse(strs[0]));
        GameObject go = new GameObject(info.dyAgent.Name + " --> bullet");
        BaseBullet BB = go.AddComponent(getBulletByType(info.bulletType)) as BaseBullet;
        BB.setBulletInfo(info);
    }

    private static Type getBulletByType(BulletType type)
    {
        Type t = null;
        switch (type)
        {
            case BulletType.normal:
                t = typeof(BulletNormal);
                break;
            default:
                t = typeof(BulletNormal);
                break;
        }
        return t;
    }
}

