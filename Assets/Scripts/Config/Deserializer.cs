public class Deserializer
{
    public static void Deserialize(SerializableSet set)
    {
       
        for (int i = 0, l = set.AudioConfigs.Length; i < l; i++)
        {
            AudioConfigConfig.GetDictionary().Add(set.AudioConfigs[i].tempId, set.AudioConfigs[i]);
        }
       
        for (int i = 0, l = set.BulletConfigs.Length; i < l; i++)
        {
            BulletConfigConfig.GetDictionary().Add(set.BulletConfigs[i].tempId, set.BulletConfigs[i]);
        }
       
        for (int i = 0, l = set.EffectConfigs.Length; i < l; i++)
        {
            EffectConfigConfig.GetDictionary().Add(set.EffectConfigs[i].tempId, set.EffectConfigs[i]);
        }
       
        for (int i = 0, l = set.FuncMenuConfigs.Length; i < l; i++)
        {
            FuncMenuConfigConfig.GetDictionary().Add(set.FuncMenuConfigs[i].tempId, set.FuncMenuConfigs[i]);
        }
       
        for (int i = 0, l = set.ItemConfigs.Length; i < l; i++)
        {
            ItemConfigConfig.GetDictionary().Add(set.ItemConfigs[i].tempId, set.ItemConfigs[i]);
        }
       
        for (int i = 0, l = set.LevelDesigns.Length; i < l; i++)
        {
            LevelDesignConfig.GetDictionary().Add(set.LevelDesigns[i].levelName, set.LevelDesigns[i]);
        }
       
        for (int i = 0, l = set.ModelConfigs.Length; i < l; i++)
        {
            ModelConfigConfig.GetDictionary().Add(set.ModelConfigs[i].tempId, set.ModelConfigs[i]);
        }
       
        for (int i = 0, l = set.SkillConfigs.Length; i < l; i++)
        {
            SkillConfigConfig.GetDictionary().Add(set.SkillConfigs[i].tempId, set.SkillConfigs[i]);
        }
       
        for (int i = 0, l = set.SupplyConfigs.Length; i < l; i++)
        {
            SupplyConfigConfig.GetDictionary().Add(set.SupplyConfigs[i].tempId, set.SupplyConfigs[i]);
        }
       
        for (int i = 0, l = set.WorkingDataConfigs.Length; i < l; i++)
        {
            WorkingDataConfigConfig.GetDictionary().Add(set.WorkingDataConfigs[i].tempId, set.WorkingDataConfigs[i]);
        }

    }
}
