using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    //缓存gameObj trans
    private GameObject cacheObj;
    public GameObject CacheObj
    {
        get
        {
            if (cacheObj == null)
            {
                cacheObj = this.gameObject;
            }
            return cacheObj;
        }
    }
    private Transform cacheTrans;
    public Transform CacheTrans
    {
        get
        {
            if (cacheTrans == null)
            {
                cacheTrans = this.transform;
            }
            return cacheTrans;
        }
    }
    //唯一ID
    private int uid;
    public int UID
    {
        get
        {
            return this.uid;
        }
        set
        {
            this.uid = value;
        }
    }
    //实体类型
    private EntityType eType;
    public EntityType EType
    {
        get
        {
            return this.eType;
        }
        set
        {
            this.eType = value;
        }
    }
    //子类型
    private EntitySonType sonType;
    public EntitySonType SonType
    {
        get
        {
            return sonType;
        }
        set
        {
            this.sonType = value;
        }
    }

    protected BillBoardWidget BillBoard;
    protected float OrgHP;
    private float hp;
    public float HP
    {
        get
        {
            return float.Parse(this.BB.getValue(Attr.hp.ToString()).ToString());
        }
        protected set
        {
            this.BB.onValueChange(Attr.hp.ToString(), value);
            if (this.BillBoard != null)
                this.BillBoard.setFloatByType(PartType.bloodPart, (value / this.OrgHP) < 0 ? 0 : value / this.OrgHP);
        }
    }
    public string Name;
    protected List<int> Skills = null;
    protected EntityInfo info = null;
    protected BlackBoard BB = null;

    private void Awake()
    {
        onAwake();
    }

    public virtual void onAwake()
    {

    }

    private void Start()
    {
        onStart();
    }
    public virtual void onStart()
    {

    }

    private void Update()
    {
        onUpdate();
    }
    public virtual void onUpdate()
    {

    }

    //当实体创建
    public virtual void onCreate(EntityInfo data)
    {
        BB = new BlackBoard();
        this.info = data;
        this.EType = data.Type;
        BB.onValueChange(Attr.entityType.ToString(), data.Type);
        this.HP = data.HP;
        BB.onValueChange(Attr.hp.ToString(), data.HP);
        this.OrgHP = data.HP;
        BB.onValueChange(Attr.orgHP.ToString(), data.HP);
        this.Name = data.Name;
        BB.onValueChange(Attr.name.ToString(), data.Name);
        this.UID = data.UID;
        BB.onValueChange(Attr.uid.ToString(), data.UID);
        this.SonType = data.SonType;
        BB.onValueChange(Attr.entitySonType.ToString(), data.SonType);
        this.CacheTrans.position = data.SpawnPos;
        this.Skills = new List<int>(data.Skills);
        BB.onValueChange(Attr.skillLst.ToString(), new List<int>(data.Skills));
    }
    #region 黑板操作
    //实体大部分信息放在黑板中
    public T getAttr<T>(string type)
    {
        return this.BB.getValue<T>(type);
    }
    public object getAttr(string type)
    {
        return this.BB.getValue(type);
    }
    public void onAttrChange(string type, object val)
    {
        this.BB.onValueChange(type, val);
    }
    public void onAttrAdd(string type, float val)
    {
        this.BB.onAddValue(type, val);
    }
    public void addAttrHandler(string type, Action<object> handler)
    {
        this.BB.addValueHandler(type, handler);
    }
    public void removeAttrHandler(string type, Action<object> handler)
    {
        this.BB.removeValueHandler(type, handler);
    }
    public void removeAttrHandlerByType(string type, bool isRemoveAll = false)
    {
        this.BB.removeAllValueHandlerByType(type, isRemoveAll);
    }

    #endregion


    public virtual int getWorkingDataId()
    {
        return this.info.workingDataId;
    }

    //实体受伤
    public virtual bool isCanAttack()
    {
        return true;
    }

    public virtual void onDamage(DamageData dt)
    {

    }



    //模型颜色改变
    private Renderer[] render;
    private Tweener tweener;
    public virtual void onChangeColor()
    {
        if (tweener == null)
        {
            tweener = DOTween.To((progress) =>
            {
                //0,0,0,1 -> 1,0,0,1
                if (render == null)
                {
                    render = this.CacheObj.GetComponentsInChildren<Renderer>();
                }
                if (render != null && render.Length > 0)
                {
                    for (int j = 0; j < render.Length; j++)
                    {
                        render[j].material.SetColor("_EmissionColor", new Color(progress, 0, 0));
                    }
                }
            }, 0, 1, 0.5f).OnComplete(() =>
            {
                DOTween.To((progress2) =>
                {
                    if (render == null)
                    {
                        render = this.CacheObj.GetComponentsInChildren<Renderer>();
                    }
                    if (render != null && render.Length > 0)
                    {
                        for (int j = 0; j < render.Length; j++)
                        {
                            render[j].material.SetColor("_EmissionColor", new Color(progress2, 0, 0));
                        }
                    }
                }, 1, 0, 0.5f).OnComplete(() =>
                {
                    tweener.Kill(false);
                    tweener = null;
                });
            });
        }
    }

    private void OnDestroy()
    {
        onDispose();
    }
    public virtual void onDispose()
    {
        this.BB.removeAllValueHandlerByType("", true);
    }

    public virtual void onReSpawn()
    {
        this.HP = float.Parse(this.getAttr(Attr.orgHP.ToString()).ToString());
    }

}

