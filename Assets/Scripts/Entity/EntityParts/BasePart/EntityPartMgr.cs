using System;
using System.Collections.Generic;

public class EntityPartMgr
{
    public static T create<T>(BaseEntity agent) where T : EntityPartWidget
    {
        T widget = default(T);
        EntityType type = agent.getAttr<EntityType>(Attr.entityType.ToString());
        switch (type)
        {
            case EntityType.player:
                widget = new EntityPartWidget(agent) as T;
                break;
            case EntityType.monster:
                break;
            case EntityType.staticActor:
                break;
        }
        return widget;
    }
}

