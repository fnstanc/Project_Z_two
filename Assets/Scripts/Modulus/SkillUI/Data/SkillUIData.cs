using System;
using System.Collections.Generic;

public class SkillUIData : BaseData
{
    public List<SkillItemUIData> lstSkill = new List<SkillItemUIData>();

    public SkillItemUIData getBaseSkillData()
    {
        SkillItemUIData dt = null;
        for (int i = 0; i < lstSkill.Count; i++)
        {
            if (lstSkill[i].skillModeType == SkillModeType.baseSkill) {
                dt = lstSkill[i];
                break;
            }            
        }
        return dt;
    }

    public SkillItemUIData getDodgeSkillData()
    {
        SkillItemUIData dt = null;
        for (int i = 0; i < lstSkill.Count; i++)
        {
            if (lstSkill[i].skillModeType == SkillModeType.dodgeSkill)
                dt = lstSkill[i];
        }
        return dt;
    }

    public List<SkillItemUIData> getNormalSkillData()
    {
        List<SkillItemUIData> lst = new List<SkillItemUIData>();
        for (int i = 0; i < lstSkill.Count; i++)
        {
            if (lstSkill[i].skillModeType == SkillModeType.normalSkill)
                lst.Add(lstSkill[i]);
        }
        return lst;
    }

}

