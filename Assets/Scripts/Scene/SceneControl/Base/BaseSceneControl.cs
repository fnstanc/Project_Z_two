using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseSceneControl : MonoBehaviour
{
    protected int uid = 1;
    protected SceneInfo info = null;

    private void Awake()
    {
        createEntityPlayer();
        onAwake();
    }

    public virtual void onAwake()
    {

    }

    private void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {

    }

    //创建玩家
    protected void createEntityPlayer()
    {
        EntityMgr.Instance.createEntity(1008611, 1008611,()=> {
            Message msg = new Message(MsgCmd.Open_MainPlayer_UI, this);
            msg.Send();
            Message msg2 = new Message(MsgCmd.Open_Skill_UI, this);
            msg2.Send();
            Message msg3 = new Message(MsgCmd.Open_JoyStick_UI, this);
            msg3.Send();
        });
    }
    //创建水晶
    protected IEnumerator createEntityCrystal(float timer)
    {
        yield return new WaitForSeconds(timer);
        for (int i = 0; i < info.LstCrystal.Count; i++)
        {
            EntityMgr.Instance.createEntity(info.LstCrystal[i], uid);
            uid++;
        }
    }

    //创建怪
    protected IEnumerator createEntityMonster(float timer)
    {
        while (info.AIWave > 0)
        {
            yield return new WaitForSeconds(timer);
            for (int i = 0; i < info.LstAI.Count; i++)
            {
                EntityMgr.Instance.createEntity(info.LstAI[i], uid);
                uid++;
            }
            info.AIWave--;
        }
        yield break;
    }


    public void setData(SceneInfo info)
    {
        this.info = info;
        if (info != null)
        {
            onSetData();
        }
    }

    public virtual void onSetData()
    {

    }
}

