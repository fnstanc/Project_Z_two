using System;
using System.Collections.Generic;

public class EntityUtils
{
    //获得一个可攻击的目标
    public static BaseEntity getCanAttackEntity(BaseEntity caster, EntityType targetType, float atkRange)
    {
        BaseEntity target = null;
        List<BaseEntity> lst = EntityMgr.Instance.getEntityByType(targetType);
        if (lst != null && lst.Count > 0)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if ((lst[i].CacheTrans.position - caster.CacheTrans.position).magnitude < atkRange)
                {
                    target = lst[i];
                }
            }
        }
        return target;
    }


}

