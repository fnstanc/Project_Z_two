using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : FSMState
{
    private EntityDynamicActor dyAgent;
    private string animName;
    private float endTime = 0;
    //crossTime 可以配置
    //private float crossFadeTime;
    //音效起始时间//音效ID
    private double audioTime = 0;
    private int audioId = 0;
    private bool isAudioPlayed = false;
    //特效起始时间    //特效ID
    private double effTime = 0;
    private int effId = 0;
    //下一次伤害检测时间
    private float nextAttackTime = 0;

    public PlayerSkillState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        checkSkill();
        if (dyAgent != null)
        {
            if (this.args != null)
            {
                this.animName = this.args.skillData.animName;
                this.endTime = dyAgent.anim[this.animName].length + Time.timeSinceLevelLoad;
                // Debug.Log(this.animName + "  len " + dyAgent.anim[this.animName].length);
                this.audioId = this.args.skillData.skillAudioId;
                this.audioTime = this.args.skillData.skillAudioTime + Time.timeSinceLevelLoad;
                this.effId = this.args.skillData.skillEffectID;
                this.effTime = this.args.skillData.skillEffectTime + Time.timeSinceLevelLoad;
            }
            dyAgent.anim.CrossFade(this.animName, 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Once;
        }
    }


    public override void onUpdate()
    {
        //攻击检测
        if (nextAttackTime > -1 && Time.timeSinceLevelLoad >= nextAttackTime)
        {
            doSkill();
        }
        //音效
        if (Time.timeSinceLevelLoad >= audioTime && dyAgent != null && !isAudioPlayed)
        {
            isAudioPlayed = true;
            AudioMgr.Instance.playAudioAtPoint(this.audioId, this.dyAgent.CacheTrans.position);
        }
        //动画播放完毕
        if (Time.timeSinceLevelLoad >= endTime && dyAgent != null)
        {
            onRelease();
        }
    }

    public override void onExit()
    {
        base.onExit();
        isAudioPlayed = false;
    }

    public override bool isCanChangeTo(StateType type)
    {
        return type == StateType.skill ? false : true;
    }

    private void checkSkill()
    {
        SkillType type = this.args.skillData.skillType;
        switch (type)
        {
            case SkillType.normal:
                this.nextAttackTime = 100 + Time.timeSinceLevelLoad;//随便写的 配置表还没配
                break;
            case SkillType.bullet:
                this.nextAttackTime = 0.5f + Time.timeSinceLevelLoad;
                break;
        }
    }

    private void doSkill()
    {
        SkillType type = this.args.skillData.skillType;
        switch (type)
        {
            case SkillType.normal:

                break;
            case SkillType.bullet:
                BulletFactroy.createBullet(this.dyAgent, this.args.skillData.skillBulletId);
                this.nextAttackTime = -1;
                break;
        }
    }

}

