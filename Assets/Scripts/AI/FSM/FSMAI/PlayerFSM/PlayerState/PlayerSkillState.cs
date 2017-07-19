using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : FSMState
{
    protected EntityDynamicActor dyAgent;
    protected string animName;
    protected float endTime = 0;
    //音效起始时间//音效ID
    protected double audioTime = 0;
    protected int audioId = 0;
    protected bool isAudioPlayed = false;
    //特效起始时间    //特效ID
    protected double effTime = 0;
    protected int effId = 0;
    //下一次伤害检测时间
    protected float nextAttackTime = 0;

    public PlayerSkillState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        checkSkill(this.args.skillData);
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
            doSkill(this.args.skillData);
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

    protected void checkSkill(SkillItemUIData skillData)
    {
        SkillType type = skillData.skillType;
        switch (type)
        {
            case SkillType.normal:
                this.nextAttackTime = skillData.skillAtkTime + Time.timeSinceLevelLoad;
                break;
            case SkillType.bullet:
                this.nextAttackTime = skillData.skillAtkTime + Time.timeSinceLevelLoad;
                break;
        }
    }

    protected void doSkill(SkillItemUIData skillData)
    {
        SkillType type = skillData.skillType;
        switch (type)
        {
            case SkillType.normal://普通技能 距离 水平角度  垂直角度 //AOI管理实体视野周围的所有实体 方便获取一定范围内的实体 这里直接拿所有实体
                List<BaseEntity> targetLst = EntityMgr.Instance.getEntityByType(EntityType.monster);
                AttackInfo info = new AttackInfo(skillData.atkRange, skillData.horAngle, skillData.verAngle);
                if (targetLst != null && targetLst.Count > 0)
                {
                    for (int i = 0; i < targetLst.Count; i++)
                    {
                        bool isAttacked = AttackHelper.isAttacked(this.dyAgent, targetLst[i], info);
                        if (isAttacked)
                        {
                            DamageData dt = new DamageData();
                            dt.casterId = this.dyAgent.UID;
                            dt.targetId = targetLst[i].UID;
                            dt.atkType = skillData.atkType;
                            dt.hitDis = skillData.hitDis;
                            dt.damage = skillData.skillDamage;
                            targetLst[i].onDamage(dt);
                        }
                    }
                }
                this.nextAttackTime = -1;
                break;
            case SkillType.bullet://子弹技能 由子弹碰撞做伤害检测
                BulletFactroy.createBullet(this.dyAgent, skillData.skillBulletId);
                this.nextAttackTime = -1;
                break;
        }
    }

}

