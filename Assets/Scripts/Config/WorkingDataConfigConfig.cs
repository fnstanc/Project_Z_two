using System.Collections.Generic;

[System.Serializable]
public class WorkingDataConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 查找间隔时间
    /// </summary>
    public float seekTime;

    /// <summary>
    /// 查找范围
    /// </summary>
    public float seekRange;

    /// <summary>
    /// 巡逻范围
    /// </summary>
    public float partolRange;


    private static Dictionary<int, WorkingDataConfigConfig> dictionary = new Dictionary<int, WorkingDataConfigConfig>();

    /// <summary>
    /// 通过tempId获取WorkingDataConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>WorkingDataConfigConfig的实例</returns>
    public static WorkingDataConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, WorkingDataConfigConfig> GetDictionary()
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
    public static WorkingDataConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        WorkingDataConfigConfig[] values = new WorkingDataConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
