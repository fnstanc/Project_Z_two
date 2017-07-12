using System;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    none = -1,
    namePart,
    hpPart,
    bloodPart,
}

public class BasePart
{
    protected PartType PType;
    protected TextMesh mesh;
    protected string str;
    protected GameObject part;
    //当前part高度  姓名0f 血条-0.2f 其他
    protected float partHeight = 0f;

    public BasePart(PartType type)
    {
        PType = type;
    }

    public virtual void create(Transform parent, EntityInfo info)
    {

    }

    public virtual void setStr(string str)
    {
        this.str = str;
        if (this.mesh != null)
        {
            this.mesh.text = str;
        }
    }
    public virtual void setFloat(float rate)
    {

    }

    public virtual void setColor(Color color)
    {
        if (this.mesh != null)
            this.mesh.color = color;
    }

}

