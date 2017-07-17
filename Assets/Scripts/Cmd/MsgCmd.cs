using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MsgCmd
{
    public const string Get_All_Goods = "Get_All_Goods";
    public const string On_Get_All_Goods = "On_Get_All_Goods";
    public const string On_Goods_Change = "On_Goods_Change";

    public const string Client_Use_Goods = "Client_Use_Goods";



    //属性发生改变
    public const string On_Change_Value = "On_Change_Value";
    public const string On_BB_Change_Value = "On_BB_Change_Value";

    //血量变换 玩家
    public const string On_HP_Change_Value = "On_HP_Change_Value";
    //血量变化 水晶
    public const string On_Crystal_HP_Change = "On_Crystal_HP_Change";
    //玩家死亡
    public const string Die_Main_Player = "Die_Main_Player";
    //水晶死亡
    public const string Die_Crystal_Entity = "Die_Crystal_Entity";
    //怪物死亡
    public const string Die_Monster_Entity = "Die_Monster_Entity";

    //场景加载完毕 抛出事件
    public const string On_Scene_Load_Finished = "On_Scene_Load_Finished";

    #region UI相关

    //背包UI
    public const string Open_Knapsack_UI = "Open_Knapsack_UI";
    public const string Close_Knapsack_UI = "Close_Knapsack_UI";

    public const string On_Weather_Msg = "On_Weather_Msg";

    //武器系统UI
    public const string Open_WeaponSystem_UI = "Open_WeaponSystem_UI";
    public const string Close_WeaponSystem_UI = "Close_WeaponSystem_UI";
    public const string On_Buy_Weapon = "On_Buy_Weapon";
    public const string On_Change_Weapon = "On_Change_Weapon";

    //MainUI 
    public const string Open_Main_Meun_UI = "Open_Main_Meun_UI";

    //技能UI
    public const string Open_Skill_UI = "Open_Skill_UI";
    public const string On_Skill_Release_Success = "On_Skill_Release_Success";

    //MainPlayerUI
    public const string Open_MainPlayer_UI = "Open_MainPlayer_UI";
    public const string On_MainPlayer_CastSkill = "On_MainPlayer_CastSkill";
    public const string On_MainPlayer_TargetChange = "On_MainPlayer_TargetChange";

    //MainPlayerMove Cmd
    public const string On_MainPlayer_Moving= "On_MainPlayer_Moving";
    public const string On_MainPlayer_Move_Start = "On_MainPlayer_Move_Start";
    public const string On_MainPlayer_Move_End = "On_MainPlayer_Move_End";
    //JoyStickUI
    public const string Open_JoyStick_UI = "Open_JoyStick_UI";

    //FuncMenuUI
    public const string Open_FuncMenu_UI = "Open_FuncMenu_UI";

    //SkillDetailUI
    public const string Open_SkillDetail_UI = "Open_SkillDetail_UI";

    //DamageTipsUI
    public const string On_Take_Damage = "On_Take_Damage";

    #endregion
}

