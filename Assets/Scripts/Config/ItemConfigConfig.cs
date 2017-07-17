using System.Collections.Generic;

[System.Serializable]
public class ItemConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 物品名称
    /// </summary>
    public string name;

    /// <summary>
    /// 实体类型
    /// </summary>
    public int type;

    /// <summary>
    /// 实体子类型
    /// </summary>
    public int sonType;

    /// <summary>
    /// 描述
    /// </summary>
    public string desc;


    private static Dictionary<int, ItemConfigConfig> dictionary = new Dictionary<int, ItemConfigConfig>();

    /// <summary>
    /// 通过tempId获取ItemConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>ItemConfigConfig的实例</returns>
    public static ItemConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, ItemConfigConfig> GetDictionary()
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
    public static ItemConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        ItemConfigConfig[] values = new ItemConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
