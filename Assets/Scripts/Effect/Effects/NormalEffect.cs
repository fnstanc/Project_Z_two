using System;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : BaseEffect
{
    private double orgTime;
    public double lifeTime;
    private bool isSetInfo = false;

    private void Update()
    {
        if (!isSetInfo || lifeTime < 0)
            return;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            PoolMgr.Instance.saveObj(this.gameObject, this.info.config.tempId + this.info.config.path);
            lifeTime = orgTime;
            isSetInfo = false;
        }
    }

    public override void refreshEffect()
    {
        if (this.info.config.isUseParent)
            this.CacheTrans.SetParent(info.parent);
        this.CacheTrans.localPosition = info.initPos;
        this.CacheTrans.localEulerAngles = info.initAngle;
        this.CacheTrans.localScale = info.initScale;
        lifeTime = this.info.config.life;
        orgTime = this.info.config.life;
        ParticleSystem sys = this.CacheObj.GetComponent<ParticleSystem>();
        if (sys != null)
        {
            sys.Play(true);
            ParticleSystem.MainModule main = sys.main;
            main.startSize = (float)info.config.startSize;
            main.loop = info.config.isLoop;
        }
        isSetInfo = true;
    }


}

