using System.Collections.Generic;

[System.Serializable]
public class BulletConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 子弹类型1普通
    /// </summary>
    public int bulletType;

    /// <summary>
    /// 子弹Size
    /// </summary>
    public string bulletSize;

    /// <summary>
    /// 子弹击中效果 1：普通 2：击退 3：浮空
    /// </summary>
    public int atkType;

    /// <summary>
    /// 击退距离 浮空高度
    /// </summary>
    public float atkDistance;

    /// <summary>
    /// 子弹飞行时间
    /// </summary>
    public float bulletLife;

    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    public float bulletSpeed;


    private static Dictionary<int, BulletConfigConfig> dictionary = new Dictionary<int, BulletConfigConfig>();

    /// <summary>
    /// 通过tempId获取BulletConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>BulletConfigConfig的实例</returns>
    public static BulletConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, BulletConfigConfig> GetDictionary()
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
    public static BulletConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        BulletConfigConfig[] values = new BulletConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
