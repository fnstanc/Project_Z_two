using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIControl : BaseControl
{
    private Dictionary<int, SkillItemUIData> dictSkillData = null;

    public override void initEnum()
    {
        this.uiEnum = UIEnum.skillUI;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_Skill_UI, onOpenUI);
        //释放技能监听
        MessageCenter.Instance.addListener(MsgCmd.On_MainPlayer_CastSkill, onCastSkill);
    }

    private void onOpenUI(Message msg)
    {
        if (dictSkillData == null) initData();
        if (dictSkillData.Count > 0)
        {
            SkillUIData dt = new SkillUIData();
            dt.lstSkill.AddRange(dictSkillData.Values);
            this.updateUI(dt);
        }
    }

    //当接受到释放技能请求
    private void onCastSkill(Message msg)
    {
        int skillId = (int)msg["skillId"];
        if (dictSkillData == null) initData();
        EntityMainPlayer role = EntityMgr.Instance.getMainPlayer() as EntityMainPlayer;
        if (role != null)
        {
            FSMArgs args = new FSMArgs();
            if (dictSkillData.ContainsKey(skillId))
            {
                SkillItemUIData skillData = dictSkillData[skillId];
                args.skillData = skillData;
            }
            role.onReleaseSkill(skillId, args);
        }
    }

    //组装数据
    private void initData()
    {
        EntityDynamicActor dy = EntityMgr.Instance.getMainPlayer() as EntityDynamicActor;

        if (dy == null || dy.getSkillWidget() == null) return;
        if (dictSkillData == null) dictSkillData = new Dictionary<int, SkillItemUIData>();

        List<int> skills = dy.getSkillWidget().getSkillInfo();
        SkillUIData dt = new SkillUIData();
        for (int i = 0; i < skills.Count; i++)
        {
            SkillConfigConfig data = SkillConfigConfig.Get(skills[i]);
            if (data != null)
            {
                SkillItemUIData info = new SkillItemUIData();
                info.skillID = data.tempId;
                info.skillCD = data.skillCD;
                info.skillDamage = data.skillDamage;
                info.skillEffectID = data.skillEffect;
                info.skillIcon = data.skillIcon;
                info.animName = data.animName;
                info.skillEffectTime = data.effectTime;
                info.skillAudioId = data.audioId;
                info.skillAudioTime = data.audioTime;
                info.skillType = (SkillType)data.skillType;
                info.fsmStateType = (StateType)data.fsmState;
                info.skillModeType = (SkillModeType)data.skillModeType;
                info.atkType = (AttackType)data.atkType;
                info.hitDis = (float)data.atkDistance;
                info.skillBulletId = data.skillBulletId;
                info.skillAtkTime = data.skillAtkTime;
                info.atkRange = (float)data.atkRange;
                info.horAngle = data.horAngle;
                info.verAngle = data.verAngle;

                dictSkillData.Add(info.skillID, info);
            }
        }
    }


}

