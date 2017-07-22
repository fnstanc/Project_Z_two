using System;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    none = 0,
    normal,
    pickUp,
    shadow,
    waitToDispose,
}

public class EffectInfo
{
    public EffectConfigConfig config;
    public BaseEntity agent;
    public Transform parent;
    public Vector3 initPos;
    public Vector3 initAngle = new Vector3(0, 0, 0);
    public Vector3 initScale = new Vector3(1, 1, 1);
    public EffectInfo(Vector3 pos)
    {
        this.initPos = pos;
    }
    public EffectInfo(Vector3 pos, BaseEntity agent)
    {
        this.initPos = pos;
        this.agent = agent;
    }
    public EffectInfo(Vector3 pos, Transform parent)
    {
        this.initPos = pos;
        this.parent = parent;
    }
    public EffectInfo(Vector3 pos, Vector3 angle, Transform parent)
    {
        this.initPos = pos;
        this.parent = parent;
        this.initAngle = angle;
    }
    public EffectInfo(Vector3 pos, Vector3 angle, Vector3 scale, Transform parent)
    {
        this.initPos = pos;
        this.parent = parent;
        this.initScale = scale;
        this.initAngle = angle;
    }
}

public class EffectMgr : Singleton<EffectMgr>
{
    private Dictionary<int, BaseEffect> createdEff = null;
    public override void init()
    {
        base.init();
        createdEff = new Dictionary<int, BaseEffect>();
    }

    private string effectPathPre = "Effect/";
    public void createEffect(int effId)
    {
        createEffect(effId, null);
    }

    public int createEffect(int effId, EffectInfo info)
    {
        EffectConfigConfig config = EffectConfigConfig.Get(effId);
        int uid = -1;
        if (config != null)
        {
            uid = MathUtils.get32UID();
            info.config = config;
            GameObject cacheGo = PoolMgr.Instance.getObj(config.tempId + config.path);
            if (cacheGo == null)
            {
                ResMgr.Instance.load(effectPathPre + config.path, (obj) =>
                  {
                      GameObject go = obj as GameObject;
                      BaseEffect be = go.AddComponent(getType((EffectType)config.effectType)) as BaseEffect;
                      be.id = uid;
                      be.setInfo(info);
                      if (!createdEff.ContainsKey(uid))
                          createdEff.Add(uid, be);
                  });
            }
            else
            {
                BaseEffect be = cacheGo.GetComponent<BaseEffect>();
                if (be != null)
                {
                    be.setInfo(info);
                    cacheGo.SetActive(true);
                    uid = be.id;
                }
            }
        }
        return uid;
    }

    public void disposeEffect(int id)
    {
        if (createdEff.ContainsKey(id))
        {
            createdEff[id].onDispose();
        }
    }


    public Type getType(EffectType type)
    {
        Type t = null;
        switch (type)
        {
            case EffectType.normal:
                t = typeof(NormalEffect);
                break;
            case EffectType.pickUp:
                t = typeof(PickUpEffect);
                break;
            case EffectType.shadow:
                t = typeof(AfterImageEffects);
                break;
            case EffectType.waitToDispose:
                t = typeof(WaitToDisposeEffect);
                break;
        }
        return t;
    }


}