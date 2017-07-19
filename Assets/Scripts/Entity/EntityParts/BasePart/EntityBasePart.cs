using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityBasePart
{
    protected string partNode = "";
    protected BaseEntity agent;
    protected GameObject partObj;

    public EntityBasePart(BaseEntity agent)
    {
        this.agent = agent;
    }

    //初始化
    public virtual void initPart()
    {
        partObj = this.agent.CacheTrans.Find(partNode).gameObject;
    }

    public GameObject getPartObj()
    {
        return this.partObj;
    }

}

