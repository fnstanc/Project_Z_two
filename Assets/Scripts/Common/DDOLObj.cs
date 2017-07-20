using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DDOLObj : MonoBehaviour
{
    public static DDOLObj Instance;

    private void Awake()
    {
        MonoBehaviour.DontDestroyOnLoad(this.gameObject);
        this.gameObject.AddComponent<TimeMgr>();
        GameObject go = GameObject.Find("EventSystem");
        if (go == null)
        {
            go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }
        Instance = this;
    }

    public Queue<string> msgs = new Queue<string>();
    private void Update()
    {
        if (msgs.Count > 0)
        {
            string msg = msgs.Dequeue();
            string[] lst = msg.Split(',');
            //0协议号        //1 playerid 没有是-1   2 3参数
            int proId = int.Parse(lst[0]);
            NetCmd cmd = (NetCmd)proId;
            Message netMsg = new Message(cmd.ToString(), this);
            netMsg["msg"] = msg;
            netMsg.Send();
        }
    }




}

