using System;
using System.Collections.Generic;
using UnityEngine;

public class NavgateWidget : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
    public EntityDynamicActor agent;
    public bool isRun = true;

    Vector3 dir = Vector3.zero;

    public void Update()
    {
        dir = target - agent.CacheTrans.position;
        if (isRun)
        {
            if (this.agent != null && (this.agent.SType == StateType.idle || this.agent.SType == StateType.run))
            {
                if (agent != null && dir.magnitude > 0.8f)
                {
                    agent.CacheTrans.LookAt(target, Vector3.up);
                    agent.moveTo(dir.normalized, true);
                    agent.onChangeState(StateType.run);
                }
                else
                {
                    isRun = false;
                    agent.onChangeState(StateType.idle);
                }
            }

        }

    }

    public static NavgateWidget create(EntityDynamicActor agent)
    {
        NavgateWidget widget = agent.CacheObj.GetComponent<NavgateWidget>();
        if (widget == null)
        {
            widget = agent.CacheObj.AddComponent<NavgateWidget>();
        }
        widget.agent = agent;
        widget.target = agent.CacheTrans.position;
        return widget;
    }


}

