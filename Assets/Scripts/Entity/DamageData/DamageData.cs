using System;
using System.Collections.Generic;

//伤害数据结构
public class DamageData : BaseData
{
    public int casterId;
    public int targetId;

    public float damage;
    public AttackType atkType;
    public float hitDis;//浮空或者击退
}

