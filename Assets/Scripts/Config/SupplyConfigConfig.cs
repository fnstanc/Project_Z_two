using System.Collections.Generic;

[System.Serializable]
public class SupplyConfigConfig
{
    /// <summary>
    /// 模版对应枚举
    /// </summary>
    public int tempId;

    /// <summary>
    /// 武器名称
    /// </summary>
    public string name;

    /// <summary>
    /// 消耗金币
    /// </summary>
    public int costMoney;

    /// <summary>
    /// 基础伤害
    /// </summary>
    public int baseDamage;

    /// <summary>
    /// 成长伤害
    /// </summary>
    public int addDamage;

    /// <summary>
    /// 加载路径
    /// </summary>
    public string path;

    /// <summary>
    /// 武器描述
    /// </summary>
    public string desc;


    private static Dictionary<int, SupplyConfigConfig> dictionary = new Dictionary<int, SupplyConfigConfig>();

    /// <summary>
    /// 通过tempId获取SupplyConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>SupplyConfigConfig的实例</returns>
    public static SupplyConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, SupplyConfigConfig> GetDictionary()
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
    public static SupplyConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        SupplyConfigConfig[] values = new SupplyConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
