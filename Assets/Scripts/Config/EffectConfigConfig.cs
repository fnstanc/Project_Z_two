using System.Collections.Generic;

[System.Serializable]
public class EffectConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 特效类型1:Normal2:PickUp3:残影
    /// </summary>
    public int effectType;

    /// <summary>
    /// 特效名称
    /// </summary>
    public string path;

    /// <summary>
    /// 生命周期
    /// </summary>
    public float life;

    /// <summary>
    /// 是否设置父物体
    /// </summary>
    public bool isUseParent;

    /// <summary>
    /// 特效大小size
    /// </summary>
    public float startSize;

    /// <summary>
    /// 是否循环
    /// </summary>
    public bool isLoop;


    private static Dictionary<int, EffectConfigConfig> dictionary = new Dictionary<int, EffectConfigConfig>();

    /// <summary>
    /// 通过tempId获取EffectConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>EffectConfigConfig的实例</returns>
    public static EffectConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, EffectConfigConfig> GetDictionary()
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
    public static EffectConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        EffectConfigConfig[] values = new EffectConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
