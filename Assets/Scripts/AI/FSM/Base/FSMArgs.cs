using System;
using System.Collections.Generic;

public class FSMArgs
{
    public SkillItemUIData skillData;
    public DamageData damageData;

    public FSMArgs()
    {

    }
    public FSMArgs(SkillItemUIData skillData)
    {
        this.skillData = skillData;
    }
    public FSMArgs(DamageData damageData)
    {
        this.damageData = damageData;
    }

}

