using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    protected BulletInfo bulletInfo;
    protected bool isInit = false;

    private Transform cacheTrans;
    public Transform CacheTrans
    {
        get
        {
            if (this.cacheTrans == null)
            {
                this.cacheTrans = this.transform;
            }
            return this.cacheTrans;
        }
    }

    private GameObject cacheObj;
    public GameObject CacheObj
    {
        get
        {
            if (this.cacheObj == null)
            {
                this.cacheObj = this.gameObject;
            }
            return this.cacheObj;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        onBulletCrash(other);
    }

    public virtual void onBulletCrash(Collider other)
    {

    }


    public void setBulletInfo(BulletInfo info)
    {
        this.bulletInfo = info;
        if (this.bulletInfo != null)
            initBullet();
        isInit = true;
    }

    protected virtual void initBullet()
    {

    }

}

