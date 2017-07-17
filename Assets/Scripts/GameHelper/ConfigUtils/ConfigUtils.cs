using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigUtils
{

    public static Vector3 getVector3(string str)
    {
        string[] vec = str.Split(',');
        Vector3 pos = new Vector3(int.Parse(vec[0]), int.Parse(vec[1]), int.Parse(vec[2]));
        return pos;
    }

    public static List<float> getFloatLst(string str)
    {
        List<float> lst = new List<float>();
        string[] lstFloat = str.Split(',');
        for (int i = 0; i < lstFloat.Length; i++)
        {
            lst.Add(float.Parse(lstFloat[i]));
        }
        return lst;
    }

    public static List<int> getIntLst(string str)
    {
        List<int> lst = new List<int>();
        string[] lstFloat = str.Split(',');
        for (int i = 0; i < lstFloat.Length; i++)
        {
            lst.Add(int.Parse(lstFloat[i]));
        }
        return lst;
    }

}

