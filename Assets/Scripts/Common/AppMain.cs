using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AppMain : MonoBehaviour
{

    private List<BaseControl> controls = null;
    private ClientSocket client = null;

    private void Awake()
    {
        init();
        addControl();
        MonoBehaviour.DontDestroyOnLoad(this.gameObject);
        createSocket();
    }

    private void createSocket()
    {

        //有服务器
        //client = new ClientSocket();
        //client.clientSendMsg(Encoding.UTF8.GetBytes("10000"));

        //如果没有服务器 请使用这里
        Message netMsg = new Message(NetCmd.onReqsRoleData.ToString(), this);
        // 1 协议号  2 uid  3 tempid
        netMsg["msg"] = "10000,398273289,1008611";
        netMsg.Send();
    }


    private void addControl()
    {
        //初始化客户端所有control初始化(监听)
        controls = new List<BaseControl>();
        controls.Add(new WeaponSystemControl());
        //controls.Add(new MainMeunControl());
        controls.Add(new SkillUIControl());
        controls.Add(new MainPlayerControl());
        controls.Add(new JoyStickControl());
        controls.Add(new FuncMenuControl());
        controls.Add(new KnapsackControl());
        controls.Add(new SkillDetailControl());
        controls.Add(new DamageTipsControl());
        initControl();
    }

    //初始化control监听
    private void initControl()
    {
        for (int i = 0; i < controls.Count; i++)
        {
            controls[i].initListener();
            controls[i].initEnum();
        }
    }

    private void init()
    {
        SerializableSet set = ResMgr.Instance.loadResByType<SerializableSet>("SerializableSet");
        Deserializer.Deserialize(set);
        GameObject go = GameObject.Find("DDOLObj");
        if (go == null)
        {
            go = new GameObject("DDOLObj");
            go.AddComponent<DDOLObj>();
        }
        GameObject eventSys = GameObject.Find("EventSystem");
        if (eventSys != null)
        {
            MonoBehaviour.DontDestroyOnLoad(eventSys);
        }
        GameObject load = GameObject.Find("LoadCanvas");
        if (load == null)
        {
            UnityEngine.Object canvas = Resources.Load("UI/LoadCanvas");
            load = MonoBehaviour.Instantiate(canvas) as GameObject;
            if (load.GetComponent<DDOLCanvas>() == null)
            {
                load.AddComponent<DDOLCanvas>();
            }
        }
    }


    private void OnApplicationQuit()
    {
        this.client.closeSocket();
    }

}
