using System.Collections.Generic;

[System.Serializable]
public class LevelDesignConfig
{
    /// <summary>
    /// 关卡场景名称
    /// </summary>
    public string levelName;

    /// <summary>
    /// 关卡name
    /// </summary>
    public string name;

    /// <summary>
    /// 关卡难度
    /// </summary>
    public int diff;

    /// <summary>
    /// AI生成波数
    /// </summary>
    public int AIWave;

    /// <summary>
    /// AI生成间隔时间
    /// </summary>
    public int AISpawnTime;

    /// <summary>
    /// 生成怪物列表
    /// </summary>
    public string monsterLst;

    /// <summary>
    /// 水晶生成时间
    /// </summary>
    public int crystalSpawnTime;

    /// <summary>
    /// 生成水晶列表
    /// </summary>
    public string crystalLst;


    private static Dictionary<string, LevelDesignConfig> dictionary = new Dictionary<string, LevelDesignConfig>();

    /// <summary>
    /// 通过levelName获取LevelDesignConfig的实例
    /// </summary>
    /// <param name="levelName">索引</param>
    /// <returns>LevelDesignConfig的实例</returns>
    public static LevelDesignConfig Get(string levelName)
    {
        return dictionary[levelName];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<string, LevelDesignConfig> GetDictionary()
    {
        return dictionary;
    }

    /// <summary>
    /// 获取所有键
    /// </summary>
    /// <returns>所有键</returns>
    public static string[] GetKeys()
    {
        int count = dictionary.Keys.Count;
        string[] keys = new string[count];
        dictionary.Keys.CopyTo(keys,0);
        return keys;
    }

    /// <summary>
    /// 获取所有实例
    /// </summary>
    /// <returns>所有实例</returns>
    public static LevelDesignConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        LevelDesignConfig[] values = new LevelDesignConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
