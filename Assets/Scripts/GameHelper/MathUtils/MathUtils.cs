using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MathUtils
{

    public static int get32UID()
    {
        return BitConverter.ToInt32(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()), 0);
    }

    public static long get64UID()
    {
        return BitConverter.ToInt64(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()), 0);
    }

    public static Vector3 getRandomPos(BaseEntity agent = null)
    {
        if (agent == null)
            agent = EntityMgr.Instance.getMainPlayer();
        Vector3 v3 = Vector3.zero;
        float x = UnityEngine.Random.Range(-5f, 5f);
        float z = UnityEngine.Random.Range(-5f, 5f);
        v3.x = agent.CacheTrans.position.x + x;
        v3.y = agent.CacheTrans.position.y;
        v3.z = agent.CacheTrans.position.z + z;
        return v3;
    }


}

