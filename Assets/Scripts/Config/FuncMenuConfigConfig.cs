using System.Collections.Generic;

[System.Serializable]
public class FuncMenuConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool isShow;

    /// <summary>
    /// 图标
    /// </summary>
    public string icon;

    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    /// <summary>
    /// cmd
    /// </summary>
    public string cmdName;


    private static Dictionary<int, FuncMenuConfigConfig> dictionary = new Dictionary<int, FuncMenuConfigConfig>();

    /// <summary>
    /// 通过tempId获取FuncMenuConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>FuncMenuConfigConfig的实例</returns>
    public static FuncMenuConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, FuncMenuConfigConfig> GetDictionary()
    {
        return dictionary;
    }

    /// <summary>
    /// 获取所有键
    /// </summary>
    /// <returns>所有键</returns>
    public static int[] GetKeys()
    {
        int count = dictionary.Keys.Count;
        int[] keys = new int[count];
        dictionary.Keys.CopyTo(keys,0);
        return keys;
    }

    /// <summary>
    /// 获取所有实例
    /// </summary>
    /// <returns>所有实例</returns>
    public static FuncMenuConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        FuncMenuConfigConfig[] values = new FuncMenuConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
