using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssetInfo
{
    public string respath;
    public string stpath;
    public string name;

    public AssetInfo()
    {

    }
    public AssetInfo(string name, string resPath, string stPath)
    {
        this.name = name;
        this.respath = resPath;
        this.stpath = stPath;
    }

    public UnityEngine.Object Obj;
    public bool isDestroy = false;

    #region res 加载
    //即时加载
    public UnityEngine.Object loadImm()
    {
        this.Obj = this.Obj != null ? this.Obj : ResMgr.Instance.resLoad(this.respath);
        return ResMgr.Instance.insObj(this.Obj);
    }
    //协程即时加载
    public IEnumerator loadCoroutine(Action<UnityEngine.Object> loaded)
    {
        this.Obj = this.Obj != null ? this.Obj : ResMgr.Instance.resLoad(this.respath);
        yield return null;
        if (loaded != null && this.Obj != null)
        {
            loaded(ResMgr.Instance.insObj(this.Obj));
        }
    }
    //协程异步加载
    public IEnumerator loadAsync(Action<UnityEngine.Object> loaded)
    {
        return loadAsync(loaded, null);
    }
    public IEnumerator loadAsync(Action<UnityEngine.Object> loaded, Action<float> progress)
    {
        if (this.Obj != null)
        {
            if (loaded != null)
            {
                loaded(ResMgr.Instance.insObj(this.Obj));
            }
            yield break;
        }
        ResourceRequest req = Resources.LoadAsync(this.respath);
        while (!req.isDone)
        {
            if (progress != null)
            {
                progress(req.progress);
            }
            yield return req;
        }
        this.Obj = req.asset;
        if (loaded != null)
        {
            loaded(ResMgr.Instance.insObj(this.Obj));
        }
    }
    #endregion

    #region bundle 加载
    public IEnumerator loadwww(Action<UnityEngine.Object> loaded, Action<float> progress = null)
    {
        if (this.Obj != null)
        {
            if (loaded != null)
            {
                loaded(ResMgr.Instance.insObj(this.Obj));
            }
            yield break;
        }
        WWW www = new WWW(this.stpath);
        yield return www;
        AssetBundle bundle = www.assetBundle;
        yield return bundle;
        this.Obj = ResMgr.Instance.insObj(bundle.LoadAsset(this.name));
        loaded(this.Obj);
        bundle.Unload(false);
    }
    public IEnumerator loadwwwAsync(Action<UnityEngine.Object> loaded, Action<float> progress = null)
    {
        if (this.Obj != null)
        {
            if (loaded != null)
            {
                loaded(ResMgr.Instance.insObj(this.Obj));
            }
            yield break;
        }
        WWW www = new WWW(this.stpath);
        yield return www;
        AssetBundle bundle = www.assetBundle;
        yield return bundle;
        AssetBundleRequest req = bundle.LoadAssetAsync(this.name);
        yield return req;
        while (!req.isDone)
        {
            if (progress != null)
            {
                progress(req.progress);
            }
        }
        this.Obj = req.asset;
        if (loaded != null)
        {
            loaded(ResMgr.Instance.insObj(this.Obj));
        }
        bundle.Unload(false);
    }

    #endregion
}

public class ResMgr : Singleton<ResMgr>
{
    public Dictionary<string, AssetInfo> dictAsset = null;

    public override void init()
    {
        dictAsset = new Dictionary<string, AssetInfo>();
    }

    public UnityEngine.Object load(string path)
    {
        AssetInfo info = null;
        if (!dictAsset.TryGetValue(path, out info))
        {
            info = new AssetInfo();
            info.name = path;
            info.respath = path;
            info.stpath = "file://" + Application.dataPath + "/StreamingAssets/" + path + ".assetbundle";
            dictAsset.Add(path, info);
        }
        return info.loadImm();
    }
    public void load(string path, Action<UnityEngine.Object> loaded)
    {
        load(path, loaded, null);
    }
    public void load(string path, Action<UnityEngine.Object> loaded, Action<float> progress, LoadType type = LoadType.coroutine)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        AssetInfo info = null;
        if (!dictAsset.TryGetValue(path, out info))
        {
            info = new AssetInfo();
            info.name = path;
            info.respath = path;
            info.stpath = "file://" + Application.dataPath + "/StreamingAssets/" + path + ".assetbundle";
            dictAsset.Add(path, info);
        }
        load(info, loaded, progress, type);
    }

    public void load(AssetInfo info, Action<UnityEngine.Object> loaded, Action<float> progress, LoadType type = LoadType.coroutine)
    {
        switch (type)
        {
            case LoadType.coroutine:
                DDOLObj.Instance.StartCoroutine(info.loadCoroutine(loaded));
                break;
            case LoadType.async:
                DDOLObj.Instance.StartCoroutine(info.loadAsync(loaded, progress));
                break;
            case LoadType.bywww:
                DDOLObj.Instance.StartCoroutine(info.loadwww(loaded, progress));
                break;
            case LoadType.bywwwAsync:
                break;
            default:
                DDOLObj.Instance.StartCoroutine(info.loadCoroutine(loaded));
                break;
        }
    }

    public UnityEngine.Object resLoad(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.Log("<color=red>ERROR：路径为空</color>" + path);
            return null;
        }
        UnityEngine.Object obj = null;
        obj = Resources.Load(path);
        if (obj == null)
        {
            Debug.Log("<color=red>ERROR：资源是否存在或资源路径错误</color>" + path);
        }
        return obj;
    }
    public UnityEngine.Object insObj(UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.Log("<color=red>ERROR：实例化空资源 </color>");
            return null;
        }
        return MonoBehaviour.Instantiate(obj);
    }

    //泛型加载
    public T loadResByType<T>(string path) where T : class
    {
        Type t = typeof(T);
        T res = Resources.Load(path, t) as T;
        if (res == null)
        {
            Debug.Log("加载资源失败 请检查路径或资源是否存在 path :" + path);
        }
        return res;
    }
    public void loadResByType<T>(string path, Action<T> loaded) where T : class
    {
        Type t = typeof(T);
        T res = Resources.Load(path, t) as T;
        loaded(res);
    }
}
