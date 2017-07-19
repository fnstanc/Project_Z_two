using System;
using System.Collections.Generic;

public class FSMArgs
{
    public SkillItemUIData skillData;
    public List<SkillItemUIData> comboSkills;
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

