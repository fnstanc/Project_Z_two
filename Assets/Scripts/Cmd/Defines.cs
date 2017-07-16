using System;
using System.Collections.Generic;
using UnityEngine;

#region 加载相关
//加载方式
public enum LoadType
{
    coroutine = 1,
    async,
    bywww,
    bywwwAsync,
}
#endregion

#region UI相关
//UI节点
public enum UINode
{
    none = -1,
    root,
    main,
    pop,
}
//UI枚举(单独的UI都会有一个枚举)
public enum UIEnum
{
    none = 0,
    knapsack,
    knapsackTips,
    weaponSys,
    mainMeun,
    weaponSysTips,
    skillUI,
    mainPlayerUI,
    joyStickUI,
    funcMenuUI,
    skillDetailUI,
    damageTipsUI,
}
//UI加载路径 对应枚举
public class UIPath
{
    public static string getUIPath(UIEnum e)
    {
        string path = null;

        switch (e)
        {
            case UIEnum.weaponSys:
                path = "UI/supplysUI";
                break;
            case UIEnum.weaponSysTips:
                path = "UI/weaponSysTips";
                break;
            case UIEnum.mainMeun:
                path = "UI/MainMeunUI";
                break;
            case UIEnum.skillUI:
                path = "UI/SkillUI";
                break;
            case UIEnum.mainPlayerUI:
                path = "UI/MainPlayerUI";
                break;
            case UIEnum.joyStickUI:
                path = "UI/JoyStickUI";
                break;
            case UIEnum.funcMenuUI:
                path = "UI/FuncMenuUI";
                break;
            case UIEnum.knapsack:
                path = "UI/KanpsackUI";
                break;
            case UIEnum.skillDetailUI:
                path = "UI/SkillDetailUI";
                break;
            case UIEnum.damageTipsUI:
                path = "UI/DamageTipsUI";
                break;
            default:
                Debug.Log("<color=red>没有这个UI枚举</color>");
                break;
        }
        return path;
    }
    //UI绑定脚本 对应枚举
    public static Type getType(UIEnum e)
    {
        Type t = null;
        switch (e)
        {
            case UIEnum.weaponSys:
                t = typeof(WeaponSysUI);
                break;
            case UIEnum.weaponSysTips:
                t = typeof(WeaponSysTips);
                break;
            case UIEnum.mainMeun:
                t = typeof(MainMeunUI);
                break;
            case UIEnum.skillUI:
                t = typeof(SkillUI);
                break;
            case UIEnum.mainPlayerUI:
                t = typeof(MainPlayerUI);
                break;
            case UIEnum.joyStickUI:
                t = typeof(JoyStickUI);
                break;
            case UIEnum.funcMenuUI:
                t = typeof(FuncMenuUI);
                break;
            case UIEnum.knapsack:
                t = typeof(KnapsackUI);
                break;
            case UIEnum.skillDetailUI:
                t = typeof(SkillDetailUI);
                break;
            case UIEnum.damageTipsUI:
                t = typeof(DamageTipsUI);
                break;
            default:
                Debug.Log("<color=red>没有这个UI枚举绑定脚本</color>");
                break;
        }
        return t;
    }
}
#endregion

#region 实体相关
public enum EntityType
{
    none = 0,
    staticActor,
    player,
    monster,
}

public enum EntitySonType
{
    none = 0,
    first,
    second,
    third,
}

#endregion

#region 武器相关
public enum WeaponType
{
    none = 0,
    gun,
    AK47,
    shotGun,
    bow,
}
#endregion

public enum SkillType
{
    normal = 1,//普通技能
    bullet = 2,//子弹技能
    dodge = 3,//闪避技能
}

//UI显示做区分
public enum SkillModeType
{
    baseSkill = 1,//基础技能
    normalSkill,//普通技能
    triggerSkill,//触发技能
    dodgeSkill,//闪避技能
}

public enum AttackType
{
    normal = 1,//普通攻击
    hitBack,//击退攻击
    hitAir,//浮空攻击
}


public class Defines
{
    //进行检测
    public const int isTest = 1;
}