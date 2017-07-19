using System.Collections.Generic;

[System.Serializable]
public class ModelConfigConfig
{
    /// <summary>
    /// 模版id
    /// </summary>
    public int tempId;

    /// <summary>
    /// 实体名称
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
    /// 实体出生点
    /// </summary>
    public string spawnPos;

    /// <summary>
    /// 实体加载路径
    /// </summary>
    public string loadPath;

    /// <summary>
    /// 实体combo技能
    /// </summary>
    public string comboSkills;

    /// <summary>
    /// 实体技能
    /// </summary>
    public string skills;

    /// <summary>
    /// 生命值
    /// </summary>
    public int hp;

    /// <summary>
    /// 姓名版高度
    /// </summary>
    public float nameHeight;

    /// <summary>
    /// 姓名版颜色
    /// </summary>
    public int nameColor;

    /// <summary>
    /// 实体icon
    /// </summary>
    public string headIcon;

    /// <summary>
    /// AI数据
    /// </summary>
    public int wdID;


    private static Dictionary<int, ModelConfigConfig> dictionary = new Dictionary<int, ModelConfigConfig>();

    /// <summary>
    /// 通过tempId获取ModelConfigConfig的实例
    /// </summary>
    /// <param name="tempId">索引</param>
    /// <returns>ModelConfigConfig的实例</returns>
    public static ModelConfigConfig Get(int tempId)
    {
        return dictionary[tempId];
    }
    
    /// <summary>
    /// 获取字典
    /// </summary>
    /// <returns>字典</returns>
    public static Dictionary<int, ModelConfigConfig> GetDictionary()
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
    public static ModelConfigConfig[] GetValues()
    {
        int count = dictionary.Values.Count;
        ModelConfigConfig[] values = new ModelConfigConfig[count];
        dictionary.Values.CopyTo(values, 0);
        return values;
    }
}
