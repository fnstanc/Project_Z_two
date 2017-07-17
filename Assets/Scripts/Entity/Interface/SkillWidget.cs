using System;
using System.Collections.Generic;

public class SkillInfo
{
    public int SkillId;
    public int IntervalTime;
    public double LastReleaseTime = 0;
    public int SkillDamage;
    public int EffectId;
}

public class SkillWidget
{
    private EntityDynamicActor agent;
    private Dictionary<int, SkillInfo> dictSkills = null;

    public SkillWidget(EntityDynamicActor agent, List<int> skills)
    {
        this.agent = agent;
        dictSkills = new Dictionary<int, SkillInfo>();
        init(skills);
    }

    public bool releaseSkill(int skillId, bool isCanChangeState)
    {
        bool isCan = false;
        //1：是否有这个技能
        if (!dictSkills.ContainsKey(skillId))
        {
            return isCan;
        }
        SkillInfo info = dictSkills[skillId];
        //2：CD检测
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        double nowTime = ts.TotalSeconds;
        if (nowTime - info.LastReleaseTime > info.IntervalTime && isCanChangeState)
        {
            //可以释放技能
            isCan = true;
            info.LastReleaseTime = nowTime;
        }
        return isCan;
    }

    private void init(List<int> skills)
    {
        for (int i = 0; i < skills.Count; i++)
        {           
            SkillConfigConfig data = SkillConfigConfig.Get(skills[i]);
            if (data != null)
            {
                SkillInfo info = new SkillInfo();
                info.SkillId = data.tempId;
                info.IntervalTime = data.skillCD;
                info.SkillDamage = data.skillDamage;
                info.EffectId = data.skillEffect;
                dictSkills.Add(info.SkillId, info);
            }
        }
    }

    public List<int> getSkillInfo()
    {
        List<int> ids = new List<int>();
        foreach (var item in dictSkills)
        {
            ids.Add(item.Value.SkillId);
        }
        return ids;
    }
}

