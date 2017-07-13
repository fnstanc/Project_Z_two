using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailItemUI : BaseUI
{
    private Image icon;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        icon = this.GetComponent<Image>();
        UIEventTrigger listener = this.CacheObj.AddComponent<UIEventTrigger>();
        listener.isShowClickAnim(true);
        listener.setClickHandler(() => { SkillDetailUI ui = this.parentUI as SkillDetailUI; if (ui != null) ui.onSkillDetailItemClick(this.data as SkillDetailItemData); });
    }

    public override void refreshUI()
    {
        SkillDetailItemData dt = this.data as SkillDetailItemData;
        if (dt != null)
        {
            this.icon.sprite = SpriteMgr.Instance.getSprite(dt.skillIcon);
        }

    }

}

