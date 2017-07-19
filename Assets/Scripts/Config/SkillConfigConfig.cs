using System.Collections.Generic;

[System.Serializable]
public class SkillConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 技能名称
    /// </summary>
    public string skillName;

    /// <summary>
    /// 技能作用类型1基础(做多段)2普通技能3触发技能4特殊(dodge)
    /// </summary>
    public int skillModeType;

    /// <summary>
    /// 技能伤害类型1普通技能2子弹技3闪避技能
    /// </summary>
    public int skillType;

    /// <summary>
    /// 技能FSM状态(对应StateTye)0.idle1.run2.onHit3.die4.spawn5.skill6.dodge7baseCombo 100.yasuoRSkill
    /// </summary>
    public int fsmState;

    /// <summary>
    /// 技能效果1普通2击退3浮空
    /// </summary>
    public int atkType;

    /// <summary>
    /// 击退距离浮空高度
    /// </summary>
    public float atkDistance;

    /// <summary>
    /// 攻击距离
    /// </summary>
    public float atkRange;

    /// <summary>
    /// 伤害检测水平角度不检测-1
    /// </summary>
    public int horAngle;

    /// <summary>
    /// 伤害检测垂直角度不检测-1
    /// </summary>
    public int verAngle;

    /// <summary>
    /// 技能检测伤害时间
    /// </summary>
    public float skillAtkTime;

    /// <summary>
    /// 子弹类型id
    /// </summary>
    public int skillBulletId;

    /// <summary>
    /// 技能CD
    /// </summary>
    public int skillCD;

    /// <summary>
    /// 技能伤害
    /// </summary>
    public int skillDamage;

    /// <summary>
    /// 技能icon
    /// </summary>
    public string skillIcon;

    /// <summary>
    /// 动画名称
    /// </summary>
    public string animName;

    /// <summary>
    /// 技能特效
    /// </summary>
    public int skillEffect;

    /// <summary>
    /// 特效起始时间
    /// </summary>
    public float effectTime;

    /// <summary>
    /// 技能音效ID
    /// </summary>
    public int audioId;

    /// <summary>
    /// 音效起始时间
    /// </summary>
    public float audioTime;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string skillDesc;


    private static Dictionary<int, SkillConfigConfig> dictionary = new Dictionary<int, SkillConfigConfig>();

    /// <summary>
    /// 通过tempId获取SkillConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>SkillConfigConfig的实例</returns>
    public static SkillConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, SkillConfigConfig> GetDictionary()
    {
        return dictionary;
    }

    /// <summary>
    /// 获取所有键
    /// </summary>
    /// <returns>所有键</returns>
    public static int[] GetKeys()
    {
        int count = dictionary.Keys.Count;
        int[] keys = new int[count];
        dictionary.Keys.CopyTo(keys,0);
        return keys;
    }

    /// <summary>
    /// 获取所有实例
    /// </summary>
    /// <returns>所有实例</returns>
    public static SkillConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        SkillConfigConfig[] values = new SkillConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
