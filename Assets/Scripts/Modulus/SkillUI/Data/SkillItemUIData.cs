using System;
using System.Collections.Generic;

public class SkillItemUIData : BaseData
{
    public int skillID;
    public int skillCD;
    public int skillDamage;
    public string skillIcon;
    public string animName;

    public float skillEffectTime;
    public int skillEffectID;

    public float skillAudioTime;
    public int skillAudioId;
    //技能类型
    public SkillType skillType;
    public int skillBulletId;
    public float skillAtkTime;
    //fsm状态
    public StateType fsmStateType;
    //技能作用类型 目前用于UI展示做区分
    public SkillModeType skillModeType;
    //技能攻击类型 浮空 击退 ...
    public AttackType atkType;
    //浮空 击退的距离
    public float hitDis;
    //技能攻击范围
    public float atkRange;
    //技能水平检测角度
    public int horAngle;
    //技能垂直检测角度
    public int verAngle;
}
