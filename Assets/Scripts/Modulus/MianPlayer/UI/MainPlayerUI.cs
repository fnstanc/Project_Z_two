using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerUI : BaseUI
{

    private Image headIcon;

    //target
    private int targetId = -1;
    private GameObject targetPanel;
    private Image targetIcon;
    private Image targetBlood;
    private float targetOrgHp = -1;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.mainPlayerUI;
        this.uiNode = UINode.root;
    }

    public override void onStart()
    {
        headIcon = this.CacheTrans.Find("HeadPanel/headIcon").GetComponent<Image>();
        targetPanel = this.CacheTrans.Find("TargetPanel").gameObject;
        targetIcon = this.CacheTrans.Find("TargetPanel/targetIcon").GetComponent<Image>();
        targetBlood = this.CacheTrans.Find("TargetPanel/bloodBG/blood").GetComponent<Image>();
    }

    public override void onActive()
    {
        MessageCenter.Instance.addListener(MsgCmd.On_MainPlayer_TargetChange, onTargetChange);
    }
    public override void onDeActive()
    {
        MessageCenter.Instance.removeListener(MsgCmd.On_MainPlayer_TargetChange, onTargetChange);
    }

    public override void refreshUI()
    {
        MainPlayerUIData dt = this.data as MainPlayerUIData;
        if (dt != null)
        {
            this.headIcon.sprite = SpriteMgr.Instance.getSprite(dt.headIcon);
        }
        checkTarget(this.targetId);
    }

    private void onTargetChange(Message msg)
    {
        int id = (int)msg["targetId"];
        checkTarget(id);
    }

    private void checkTarget(int id)
    {
        if (id == -1)
        {
            targetPanel.SetActive(false);
            return;
        }
        if (this.targetId != -1)
        {
            BaseEntity oldTarget = EntityMgr.Instance.getEntityById(this.targetId);
            if (oldTarget != null)
            {
                oldTarget.removeAttrHandler(Attr.hp.ToString(), onTargetBloodChange);
            }
        }
        this.targetId = id;
        BaseEntity newTarget = EntityMgr.Instance.getEntityById(this.targetId);
        if (newTarget != null)
        {
            newTarget.addAttrHandler(Attr.hp.ToString(), onTargetBloodChange);
            targetOrgHp = float.Parse(newTarget.getAttr(Attr.orgHP.ToString()).ToString());
            float hp = float.Parse(newTarget.getAttr(Attr.hp.ToString()).ToString());
            targetBlood.fillAmount = hp / targetOrgHp;
        }
        targetPanel.SetActive(true);
    }

    //当目标血条改变
    private void onTargetBloodChange(object val)
    {
        targetBlood.fillAmount = float.Parse(val.ToString()) / targetOrgHp;
    }


}

