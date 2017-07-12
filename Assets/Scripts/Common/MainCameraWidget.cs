using System;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraWidget : MonoBehaviour
{
    private BaseEntity agent;
    [SerializeField]
    private float height = 3.5f;
    [SerializeField]
    private float distance = 6;

    private Transform cacheTrans;
    public Transform CacheTrans
    {
        get
        {
            if (this.cacheTrans == null)
            {
                this.cacheTrans = this.transform;
            }
            return this.cacheTrans;
        }
        set
        {
            this.cacheTrans = value;
        }
    }

    private void Awake()
    {
        this.gameObject.AddComponent<AudioListener>();
    }


    Vector3 offset = Vector3.zero;
    void Update()
    {
        if (this.agent != null)
        {
            this.CacheTrans.position = (agent.CacheTrans.position - Vector3.forward * distance) + Vector3.up * height;
            //this.CacheTrans.position = Vector3.Lerp(this.CacheTrans.position, (agent.CacheTrans.forward * -distance) + (agent.CacheTrans.up * height) + agent.CacheTrans.position, 0.5f);
            //this.CacheTrans.position = Vector3.Lerp(this.CacheTrans.position, new Vector3(0, (agent.CacheTrans.position.y + height), agent.CacheTrans.position.z - distance), 0.5f);
            this.CacheTrans.LookAt(agent.CacheTrans.position, Vector3.up);
        }
    }
    protected void ClampAngle(ref Vector3 angle)
    {
        if (angle.x < -180) angle.x += 360;
        else if (angle.x > 180) angle.x -= 360;

        if (angle.y < -180) angle.y += 360;
        else if (angle.y > 180) angle.y -= 360;
    }

    protected float GetAngle(Vector2 fromVector2, Vector2 toVector2)
    {
        Vector2 v2 = fromVector2 - toVector2;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg; ;
        angle += 180f;
        return angle;
    }

    public void setAgent(BaseEntity agent)
    {
        this.agent = agent;
        lookPlayer();
    }

    public void lookPlayer()
    {
        if (this.agent != null)
        {
            this.CacheTrans.position = (agent.CacheTrans.forward * -distance) + (agent.CacheTrans.up * height) + agent.CacheTrans.position;
            this.CacheTrans.rotation = agent.CacheTrans.rotation;
        }
    }


}

