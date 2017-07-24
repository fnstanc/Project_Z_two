using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SyncHelper
{
    //1协议号 2uid 3x 4y 5z
    public static void syncPos(int uid, float x, float y, float z)
    {
        string str = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", (int)NetCmd.onReqSyncPos, ',', uid.ToString(), ',', (int)x * 100, ',', (int)y * 100, ',', (int)z * 100);
        //StringBuilder build = new StringBuilder();
        //build.Append((int)NetCmd.onReqSyncPos);

        //Debug.Log(str.ToString());
        byte[] buff = Encoding.UTF8.GetBytes(str);
        AppMain.client.clientSendMsg(buff);
    }
    public static void onSyncPos(string msg)
    {
        //解析
        string[] msgLst = msg.Split(',');
        int uid = int.Parse(msgLst[1]);
        int x = int.Parse(msgLst[2]);
        x = x / 100;
        int y = int.Parse(msgLst[3]);
        y = y / 100;
        int z = int.Parse(msgLst[4]);
        z = z / 100;

        EntityDynamicActor role = EntityMgr.Instance.getEntityById<EntityDynamicActor>(uid);
        if (role != null)
            role.navgateTo(new Vector3(x, y, z));
    }

    //1协议号 2uid 3skillid
    public static void syncSkill(int uid, int skillId)
    {
        string str = string.Format("{0}{1}{2}{3}{4}", (int)NetCmd.roleCastSkill, ',', uid.ToString(), ',', skillId);
        byte[] buff = Encoding.UTF8.GetBytes(str);
        AppMain.client.clientSendMsg(buff);
    }
    public static void onSyncSkill(string msg)
    {
        //解析
        string[] msgLst = msg.Split(',');
        int uid = int.Parse(msgLst[1]);
        int skillId = int.Parse(msgLst[2]);

        EntityDynamicActor role = EntityMgr.Instance.getEntityById<EntityDynamicActor>(uid);
        if (role != null)
            role.onReleaseSkill(skillId, new FSMArgs(SkillUIControl.getSkillData(skillId)));
    }

}

