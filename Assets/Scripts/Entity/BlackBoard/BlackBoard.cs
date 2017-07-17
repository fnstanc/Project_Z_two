using System;
using System.Collections.Generic;

public enum Attr
{
    entityType,
    entitySonType,
    hp,
    orgHP,
    mp,
    name,
    uid,
    money,
    skillLst,
    target,
}


public class BlackBoard
{
    private Dictionary<string, object> dictVals = null;
    private Dictionary<string, List<Action<object>>> handlerMap = null;

    public BlackBoard()
    {
        dictVals = new Dictionary<string, object>();
        handlerMap = new Dictionary<string, List<Action<object>>>();
    }
    //监听
    public void addValueHandler(string type, Action<object> handler)
    {
        if (!handlerMap.ContainsKey(type))
        {
            handlerMap.Add(type, new List<Action<object>>());
        }
        if (!handlerMap[type].Contains(handler))
            handlerMap[type].Add(handler);
    }

    public void removeValueHandler(string type, Action<object> handler)
    {
        if (handlerMap.ContainsKey(type))
        {
            if (handlerMap[type].Contains(handler))
                handlerMap[type].Remove(handler);
        }
    }
    public void removeAllValueHandlerByType(string type, bool isRemoveAll = false)
    {
        if (isRemoveAll)
        {
            foreach (var item in handlerMap)
            {
                item.Value.Clear();
            }
            return;
        }
        if (handlerMap.ContainsKey(type))
        {
            handlerMap[type].Clear();
        }
    }



    public void onValueChange(string type, object val)
    {
        if (dictVals.ContainsKey(type))
        {
            dictVals[type] = val;
        }
        else
        {
            dictVals.Add(type, val);
        }
        if (handlerMap.ContainsKey(type))
        {
            for (int i = 0; i < handlerMap[type].Count; i++)
            {
                handlerMap[type][i](dictVals[type]);
            }
        }
    }

    public void onAddValue(string type, float val)
    {
        if (dictVals.ContainsKey(type))
        {
            dictVals[type] = (float)dictVals[type] + val;
        }
        else
        {
            dictVals.Add(type, val);
        }
        if (handlerMap.ContainsKey(type))
        {
            for (int i = 0; i < handlerMap[type].Count; i++)
            {
                handlerMap[type][i](dictVals[type]);
            }
        }
    }

    public object getValue(string type)
    {
        object o = null;
        if (dictVals.ContainsKey(type))
        {
            o = dictVals[type];
        }
        return o;
    }

    public T getValue<T>(string type)
    {
        T val = default(T);
        if (dictVals.ContainsKey(type))
        {
            val = (T)dictVals[type];
        }
        return val;
    }

}

