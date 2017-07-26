using System;
using System.Collections.Generic;

public class DropCallBillBoard : BillBoardWidget
{
    public override void onAwake()
    {
        base.onAwake();
        this.dictParts.Add(PartType.namePart, new CrystalNamePart(PartType.namePart));
    }
}

