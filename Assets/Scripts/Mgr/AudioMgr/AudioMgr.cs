using System;
using System.Collections.Generic;
using UnityEngine;
using ChuMeng;

public class AudioInfo
{
    public int tempId;
    public string audioName;
    public bool isLoop;
}

public class AudioMgr : Singleton<AudioMgr>
{
    private Dictionary<int, AudioInfo> dictAudios = null;

    public override void init()
    {
        base.init();
        dictAudios = new Dictionary<int, AudioInfo>();
        initAudio();
    }

    //接口播放音效 on point 
    public void playAudioAtPoint(int tempId)
    {
        playAudioAtPoint(tempId, Vector3.zero);
    }
    public void playAudioAtPoint(int tempId, Vector3 pos)
    {
        if (dictAudios.ContainsKey(tempId))
        {
            AudioInfo info = dictAudios[tempId];
            loadClip(info.audioName, (clip) =>
            {
                AudioSource.PlayClipAtPoint(clip, pos);
            });
        }
    }




    //加载音效
    private string audioPathPre = "Audio/";
    private void loadClip(string name, Action<AudioClip> loaded)
    {
        string path = audioPathPre + name;
        ResMgr.Instance.loadResByType<AudioClip>(path, loaded);
    }


    //初始化数据
    private void initAudio()
    {
        List<AudioConfigData> lst = GameData.AudioConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            AudioInfo info = new AudioInfo();
            info.tempId = lst[i].tempId;
            info.audioName = lst[i].audioName;
            info.isLoop = lst[i].isLoop;
            dictAudios.Add(info.tempId, info);
        }
    }
}

