using System;
using System.Collections.Generic;
using UnityEngine;

public class BloodPart : BasePart
{
    protected SpriteRenderer bloodSp;
    protected SpriteRenderer bgBloodSp;
    protected GameObject bgPart;
    protected new float partHeight = -0.2f;

    public BloodPart(PartType type) : base(type)
    {
    }

    public override void create(Transform parent, EntityInfo info)
    {
        bgPart = new GameObject(this.PType.ToString() + "bg");
        bgPart.transform.SetParent(parent);
        bgPart.transform.localPosition = new Vector3(0, partHeight, 0.15f);
        bgPart.transform.localScale = new Vector3(1.02f, 0.15f, 1);
        bgBloodSp = bgPart.AddComponent<SpriteRenderer>();
        bgBloodSp.sprite = SpriteMgr.Instance.getSprite("whiteblood2");
        bgBloodSp.color = new Color(0, 0, 0, 0.7f);
        bgBloodSp.sortingOrder = -1;

        part = new GameObject(this.PType.ToString());
        part.transform.SetParent(parent);
        part.transform.localPosition = new Vector3(0, partHeight, 0.15f);
        part.transform.localScale = new Vector3(1f, 0.1f, 1);
        bloodSp = part.AddComponent<SpriteRenderer>();
        bloodSp.sprite = SpriteMgr.Instance.getSprite("whiteblood2");
        bloodSp.color = Color.red;
        bloodSp.sortingOrder = 0;
    }

    public override void setFloat(float rate)
    {
        if (part != null)
        {
            float end = (1 - rate) / 0.1f * -0.05f;
            part.transform.localScale = new Vector3(rate, 0.1f, 1f);
            part.transform.localPosition = new Vector3(end, partHeight, 0.15f);
        }
    }

}

