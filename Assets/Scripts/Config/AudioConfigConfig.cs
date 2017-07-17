using System.Collections.Generic;

[System.Serializable]
public class AudioConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 音效名称
    /// </summary>
    public string audioName;

    /// <summary>
    /// 是否循环
    /// </summary>
    public bool isLoop;


    private static Dictionary<int, AudioConfigConfig> dictionary = new Dictionary<int, AudioConfigConfig>();

    /// <summary>
    /// 通过tempId获取AudioConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>AudioConfigConfig的实例</returns>
    public static AudioConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, AudioConfigConfig> GetDictionary()
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
    public static AudioConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        AudioConfigConfig[] values = new AudioConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
