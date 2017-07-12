using System;
using System.Collections.Generic;
using UnityEngine;

public class NamePart : BasePart
{
    private TextMesh bgMesh = null;

    public NamePart(PartType type) : base(type)
    {
    }

    public override void create(Transform parent, EntityInfo info)
    {
        GameObject namePartBg = new GameObject(this.PType.ToString() + "bg");
        namePartBg.transform.SetParent(parent);
        namePartBg.transform.localPosition = new Vector3(0 + 0.02f, partHeight - 0.02f, 0.15f);
        this.bgMesh = namePartBg.AddComponent<TextMesh>();
        this.bgMesh.anchor = TextAnchor.MiddleCenter;
        this.bgMesh.characterSize = 0.1f;
        this.bgMesh.fontSize = 20;
        this.bgMesh.color = Color.black;
        setStr(info.Name);

        part = new GameObject(this.PType.ToString());
        part.transform.SetParent(parent);
        part.transform.localPosition = new Vector3(0, partHeight, 0.15f);
        this.mesh = part.AddComponent<TextMesh>();
        this.mesh.anchor = TextAnchor.MiddleCenter;
        this.mesh.characterSize = 0.1f;
        this.mesh.fontSize = 20;
        setStr(info.Name);
    }


    public override void setStr(string str)
    {
        this.str = str;
        if (this.mesh != null)
        {
            this.mesh.text = str;
        }
        if (this.bgMesh != null)
        {
            this.bgMesh.text = str;
        }
    }

}

