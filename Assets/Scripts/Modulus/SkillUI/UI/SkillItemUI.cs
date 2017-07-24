using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillItemUI : BaseUI
{
    private Image skillIcon;
    private Image skillMask;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        this.skillIcon = this.CacheObj.GetComponent<Image>();
        this.skillMask = this.CacheTrans.Find("skillMask").GetComponent<Image>();
        this.skillMask.gameObject.SetActive(false);
        UIEventTrigger listener = this.CacheObj.AddComponent<UIEventTrigger>();
        listener.isShowClickAnim(true);
        listener.setClickHandler(castSkill);

    }

    public override void refreshUI()
    {
        SkillItemUIData dt = this.data as SkillItemUIData;
        if (dt != null)
        {
            this.skillIcon.sprite = SpriteMgr.Instance.getSprite(dt.skillIcon);
        }
    }

    private void castSkill()
    {        
        Message msg = new Message(MsgCmd.On_MainPlayer_CastSkill, this);
        SkillItemUIData dt = this.data as SkillItemUIData;
        msg["skillId"] = dt.skillID;
        msg.Send();
    }

    //当技能释放成功
    private void onCastSkillSuccess(Message msg)
    {
        int skillId = (int)msg["skillId"];
        Debug.Log("onCastSkillSuccess(Message msg)");
        SkillItemUIData dt = this.data as SkillItemUIData;
        if (dt != null && skillId == dt.skillID)
        {
            UIEventTrigger listener = this.CacheObj.AddComponent<UIEventTrigger>();
            if (listener != null) listener.isCanClickBtn(false);
            showSkillCDAnim();
        }
    }
    //UI技能CD动画
    private void showSkillCDAnim()
    {
        float skillCD = 1;
        SkillItemUIData dt = this.data as SkillItemUIData;
        if (dt != null)
        {
            skillCD = dt.skillCD;
        }
        this.skillMask.gameObject.SetActive(true);
        this.skillMask.fillAmount = 1;        
        this.skillMask.DOFillAmount(0, skillCD).SetEase(Ease.Linear).OnComplete(()=> { this.skillMask.gameObject.SetActive(false); });
    }

    public override void onActive()
    {
        MessageCenter.Instance.addListener(MsgCmd.On_Skill_Release_Success, onCastSkillSuccess);
    }
    public override void onDeActive()
    {
        MessageCenter.Instance.removeListener(MsgCmd.On_Skill_Release_Success, onCastSkillSuccess);
    }

}

