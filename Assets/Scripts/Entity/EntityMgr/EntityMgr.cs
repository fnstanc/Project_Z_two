using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo
{
    public int UID;
    public string Path;
    public EntityType Type;
    public EntitySonType SonType;
    public string Name;
    public int TempId;
    public int HP;
    public float NameHeight;
    public Vector3 SpawnPos;
    public List<int> Skills;
    public List<int> comboSkills;
    public int workingDataId;

    public EntityInfo()
    {
        Skills = new List<int>();
        comboSkills = new List<int>();
    }

}

public class EntityMgr : Singleton<EntityMgr>
{
    //实体静态数据
    private Dictionary<int, EntityInfo> dictInfo = null;
    //实体管理列表
    private Dictionary<int, BaseEntity> dictEntityById = null;
    private Dictionary<EntityType, List<BaseEntity>> dictEntityByType = null;

    public override void init()
    {
        dictEntityById = new Dictionary<int, BaseEntity>();
        dictEntityByType = new Dictionary<EntityType, List<BaseEntity>>();
        dictInfo = new Dictionary<int, EntityInfo>();
        loadStaticData();
    }


    //创建entity
    public void createEntity(int tempId, int uid, Action loadFinished = null)
    {
        EntityInfo data = null;
        if (dictInfo.ContainsKey(tempId))
        {
            data = dictInfo[tempId];
            data.UID = uid;
        }
        if (data == null)
        {
            return;
        }
        onCreate(data, loadFinished);
    }
    private string resPre = "Entity/";
    private string stPre = "file://" + Application.dataPath + "/StreamingAssets/";
    private void onCreate(EntityInfo data, Action loadFinished)
    {
        AssetInfo info = new AssetInfo(data.Path, resPre + data.Path, stPre + data.Path + ".assetbundle");
        ResMgr.Instance.load(info, (obj) =>
         {
             GameObject go = obj as GameObject;
             BaseEntity be = go.GetComponent<BaseEntity>();
             if (be == null)
             {
                 be = go.AddComponent(getType(data.Type)) as BaseEntity;
             }
             be.onCreate(data);
             this.addEntity(be);
             if (loadFinished != null) loadFinished();
         }, null, LoadType.coroutine);
    }

    private Type getType(EntityType type)
    {
        Type t = null;
        switch (type)
        {
            case EntityType.staticActor:
                t = typeof(EntityCrystal);
                break;
            case EntityType.player:
                t = typeof(EntityMainPlayer);
                break;
            case EntityType.monster:
                t = typeof(EntityMonster);
                break;
        }
        return t;
    }
    //添加entity
    public void addEntity(BaseEntity entity)
    {
        if (!dictEntityById.ContainsKey(entity.UID))
        {
            dictEntityById.Add(entity.UID, entity);
        }
        if (!dictEntityByType.ContainsKey(entity.EType))
        {
            dictEntityByType.Add(entity.EType, new List<BaseEntity>());
        }
        if (!dictEntityByType[entity.EType].Contains(entity))
        {
            dictEntityByType[entity.EType].Add(entity);
        }
    }
    //移除entity
    public void removeEntity(BaseEntity entity)
    {
        if (entity == null || !dictEntityById.ContainsKey(entity.UID))
            return;

        if (dictEntityById.ContainsKey(entity.UID))
        {
            dictEntityById.Remove(entity.UID);
        }
        if (dictEntityByType.ContainsKey(entity.EType))
        {
            if (dictEntityByType[entity.EType].Contains(entity))
            {
                dictEntityByType[entity.EType].Remove(entity);
            }
        }
        EntityType type = entity.EType;
        GameObject.DestroyObject(entity.CacheObj);
        switch (type)
        {
            case EntityType.player:
                break;
            case EntityType.monster:
                Message Die_Monster_Entity = new Message(MsgCmd.Die_Monster_Entity, this);
                Die_Monster_Entity.Send();
                break;
            case EntityType.staticActor:
                Message Die_Crystal_Entity = new Message(MsgCmd.Die_Crystal_Entity, this);
                Die_Crystal_Entity.Send();
                break;
        }
    }
    //移除entity by type
    public void removeEntityByType(EntityType type)
    {
        if (!dictEntityByType.ContainsKey(type))
        {
            return;
        }
        List<BaseEntity> lst = new List<BaseEntity>(dictEntityByType[type]);
        for (int i = 0; i < lst.Count; i++)
        {
            GameObject.DestroyObject(lst[i].CacheObj);
        }
    }
    //移除所有实体
    public void removeAllEntity()
    {
        if (dictEntityById != null)
            foreach (var item in dictEntityById)
            {
                item.Value.onDispose();
            }
        dictEntityById.Clear();
        dictEntityByType.Clear();
    }

    //获取实体根据id
    public BaseEntity getEntityById(int uid)
    {
        if (dictEntityById.ContainsKey(uid))
        {
            return dictEntityById[uid];
        }
        return null;
    }
    public T getEntityById<T>(int uid) where T : BaseEntity
    {
        T entity = default(T);
        if (dictEntityById.ContainsKey(uid))
        {
            entity = dictEntityById[uid] as T;
        }
        return entity;
    }

    //获取实体根据type
    public List<BaseEntity> getEntityByType(EntityType type)
    {
        if (dictEntityByType.ContainsKey(type))
        {
            return dictEntityByType[type];
        }
        return null;
    }
    //获取主角
    public BaseEntity getMainPlayer()
    {
        return getEntityById(1008611);
    }
    //是否是猪脚
    public bool isMainPlayer(BaseEntity entity)
    {
        return entity.UID == getMainPlayer().UID;
    }
    public bool isMainPlayer(int uid)
    {
        return uid == getMainPlayer().UID;
    }

    //加载静态数据
    private void loadStaticData()
    {
        ModelConfigConfig[] confs = ModelConfigConfig.GetValues();
        for (int i = 0; i < confs.Length; i++)
        {
            EntityInfo data = new EntityInfo();
            data.TempId = confs[i].tempId;
            data.Name = confs[i].name;
            data.Type = (EntityType)confs[i].type;
            data.SonType = (EntitySonType)confs[i].sonType;
            data.Path = confs[i].loadPath;
            data.HP = confs[i].hp;
            data.NameHeight = confs[i].nameHeight;
            //出生点
            data.SpawnPos = ConfigUtils.getVector3(confs[i].spawnPos);
            //技能
            data.Skills.AddRange(ConfigUtils.getIntLst(confs[i].skills));
            //combo
            data.comboSkills.AddRange(ConfigUtils.getIntLst(confs[i].comboSkills));
            data.workingDataId = confs[i].wdID;
            dictInfo.Add(confs[i].tempId, data);
        }
    }
}

