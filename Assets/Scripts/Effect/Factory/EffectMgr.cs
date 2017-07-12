using System;
using System.Collections.Generic;
using ChuMeng;
using UnityEngine;

public enum EffectType
{
    none = 0,
    normal,
    pickUp,
    shadow,
}

public class EffectInfo
{
    public EffectConfigData config;
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
    private Dictionary<int, EffectConfigData> dictEff = null;
    private Dictionary<int, BaseEffect> createdEff = null;
    private int count = 1;
    public override void init()
    {
        base.init();
        dictEff = new Dictionary<int, EffectConfigData>();
        createdEff = new Dictionary<int, BaseEffect>();
        List<EffectConfigData> lst = GameData.EffectConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            dictEff.Add(lst[i].tempId, lst[i]);
        }
    }

    private string effectPathPre = "Effect/";
    public void createEffect(int effId)
    {
        createEffect(effId, null);
    }

    public int createEffect(int effId, EffectInfo info)
    {
        if (dictEff.ContainsKey(effId))
        {
            EffectConfigData config = dictEff[effId];
            info.config = config;
            GameObject cacheGo = PoolMgr.Instance.getObj(config.tempId + config.path);
            if (cacheGo == null)
            {
                ResMgr.Instance.load(effectPathPre + config.path, (obj) =>
                  {
                      GameObject go = obj as GameObject;
                      BaseEffect be = go.AddComponent(getType((EffectType)config.effectType)) as BaseEffect;
                      be.id = count;
                      be.setInfo(info);
                      count++;
                      if (!createdEff.ContainsKey(count))
                          createdEff.Add(count, be);
                  });
            }
            else
            {
                BaseEffect be = cacheGo.GetComponent<BaseEffect>();
                if (be != null)
                {
                    be.setInfo(info);
                    cacheGo.SetActive(true);
                    count = be.id;
                }
            }
        }
        return count;
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
        }
        return t;
    }


}