using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseComboState : PlayerSkillState
{
    //最大位移距离 可以配置
    private float maxMoveDis = 1.8f;
    private float atkRange = -1;

    private int maxCount = -1;
    private int currCount = 0;
    private int nextCount = -1;

    private float changeNextTime = -1;

    public PlayerBaseComboState(BaseEntity agent, StateType type) : base(agent, type)
    {

    }

    public override void onEnter()
    {
        Debug.Log(this.agent.UID + "<color=yellow>   进入状态 ->  </color>" + this.SType.ToString());
        dyAgent.activeWeaponTrail(true);
        MessageCenter.Instance.addListener(MsgCmd.On_Combo_Release_Success, onComboClick);
        maxCount = this.args.comboSkills.Count - 1;
        playAnim();
    }

    Vector3 dir = Vector3.zero;
    Vector3 look = Vector3.zero;
    float dis = -1;
    public override void onUpdate()
    {
        //combo检测
        if (Time.timeSinceLevelLoad >= changeNextTime)
        {
            if (nextCount == -1 || nextCount == currCount)
            {
                onRelease();
            }
            else
            {
                currCount = nextCount;
                playAnim();
            }
        }
        //攻击检测
        if (nextAttackTime > -1 && Time.timeSinceLevelLoad >= nextAttackTime - 0.2f)
        {
            if (this.dyAgent.Target != null)
            {
                dir = this.dyAgent.Target.CacheTrans.position - this.dyAgent.CacheTrans.position;
                look = this.dyAgent.Target.CacheTrans.position;
                look.y = this.dyAgent.CacheTrans.position.y;
                dis = dir.magnitude;
                this.dyAgent.CacheTrans.LookAt(look, Vector3.up);
                if (dis > atkRange && dis - atkRange < atkRange)
                {
                    this.dyAgent.moveTo(dir.normalized * 10f);
                }
            }
        }
        if (nextAttackTime > -1 && Time.timeSinceLevelLoad >= nextAttackTime)
        {
            SkillItemUIData comboData = this.args.comboSkills[currCount];
            doSkill(comboData);
        }
        //音效
        if (Time.timeSinceLevelLoad >= audioTime && dyAgent != null && !isAudioPlayed)
        {
            isAudioPlayed = true;
            AudioMgr.Instance.playAudioAtPoint(this.audioId, this.dyAgent.CacheTrans.position);
        }
    }

    public override void onExit()
    {
        Debug.Log(this.agent.UID + "<color=red>   退出状态 ->  </color>" + this.SType.ToString());
        MessageCenter.Instance.removeListener(MsgCmd.On_Combo_Release_Success, onComboClick);
        currCount = 0;
        nextCount = -1;
        changeNextTime = -1;
        dyAgent.activeWeaponTrail(false);
    }

    private void checkTarget()
    {
        BaseEntity target = EntityUtils.getCanAttackEntity(this.dyAgent, EntityType.player, maxMoveDis + atkRange);
        if (target != null)
        {
            this.dyAgent.Target = target;
        }
    }

    private void playAnim()
    {
        checkTarget();
        SkillItemUIData comboData = this.args.comboSkills[currCount];
        atkRange = comboData.atkRange;
        string animName = comboData.animName;
        dyAgent.anim.CrossFade(animName, 0.1f);
        dyAgent.anim.wrapMode = WrapMode.Once;
        changeNextTime = Time.timeSinceLevelLoad + dyAgent.anim[animName].length;
        this.audioId = comboData.skillAudioId;
        this.audioTime = comboData.skillAudioTime + Time.timeSinceLevelLoad;
        this.effId = comboData.skillEffectID;
        this.effTime = comboData.skillEffectTime + Time.timeSinceLevelLoad;
        isAudioPlayed = false;
        checkSkill(comboData);
    }

    private void onComboClick(Message msg)
    {
        if (nextCount != -1 && nextCount != currCount) return;
        nextCount = nextCount > currCount ? nextCount : currCount + 1;
        nextCount = nextCount > maxCount ? 0 : nextCount;
        changeNextTime -= 0.5f;
    }

    public override bool isCanChangeTo(StateType type)
    {
        return type == StateType.baseCombo ? false : true;
    }

}

