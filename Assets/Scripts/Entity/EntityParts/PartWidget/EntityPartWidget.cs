using System;
using System.Collections.Generic;
using UnityEngine;

public enum EntityPartType
{
    weapon = 1,
    wing,
    head,
}

public class EntityPartWidget
{
    private Dictionary<EntityPartType, EntityBasePart> dictPart = null;
    private BaseEntity agent;

    public EntityPartWidget(BaseEntity agent)
    {
        this.agent = agent;
        dictPart = new Dictionary<EntityPartType, EntityBasePart>();
        addPart();
        createPart();
    }

    protected virtual void addPart()
    {
        dictPart.Add(EntityPartType.weapon, new EntityWeaponPart(agent));
    }
    private void createPart()
    {
        foreach (var item in dictPart)
        {
            item.Value.initPart();
        }
    }

    //外部接口
    public GameObject getPartByType(EntityPartType type)
    {
        GameObject part = null;
        if (dictPart.ContainsKey(type))
        {
            part = dictPart[type].getPartObj();
        }
        return part;
    }


}

