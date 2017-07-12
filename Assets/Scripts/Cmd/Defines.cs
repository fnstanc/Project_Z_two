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
    normal = 1,
    bullet = 2,
    dodge = 3,
}


public enum SkillModeType
{
    baseSkill = 1,
    normalSkill,
    triggerSkill,
    dodgeSkill,
}

public class Defines
{
    //进行检测
    public const int isTest = 1;
}