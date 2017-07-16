using System;
using System.Collections.Generic;
using UnityEngine;

public class UIUtils
{
    public static GameObject cloneObj(GameObject go)
    {
        return MonoBehaviour.Instantiate(go) as GameObject;
    }

    public static GameObject cloneObj(GameObject go, Transform parent)
    {
        GameObject item = MonoBehaviour.Instantiate(go) as GameObject;
        item.transform.SetParent(parent);
        item.transform.localScale = new Vector3(1, 1, 1);
        item.SetActive(true);
        return item;
    }
    //获取屏幕高度
    public static float getScreenHeight()
    {
        return Screen.height;
    }

}

