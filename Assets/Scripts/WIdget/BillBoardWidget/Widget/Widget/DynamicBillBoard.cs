using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBillBoard : BillBoardWidget
{
    public override void onAwake()
    {
        base.onAwake();
        this.dictParts.Add(PartType.namePart, new NamePart(PartType.namePart));
        this.dictParts.Add(PartType.bloodPart, new BloodPart(PartType.bloodPart));
    }

    protected override void setHeight(EntityInfo info)
    {
        this.addHeight = 0.7f;
        base.setHeight(info);
    }

}

