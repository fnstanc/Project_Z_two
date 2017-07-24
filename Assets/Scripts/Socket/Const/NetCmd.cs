using System;
using System.Collections.Generic;

public enum NetCmd : int
{
    //上行
    onReqSyncPos = 9000,//同步移动
    roleCastSkill,

    //下行
    onReqsSyncPos = 9001,//同步移动
    onReqsRoleData = 10000,//创建主角
    onReqsNetRoleData = 10001,//创建netPlayer
    onReqsRoleCastSkill = 10002,

}

