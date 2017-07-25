using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailUI : BaseUI
{
    private GameObject temp;
    private GameObject normalSkill;
    private GameObject choose;

    //
    private Text skillId;
    private Text skillName;
    private Text skillDesc;
    private Text skillModeType;
    private Text atkType;
    private Text atkRange;
    private Text horAngle;
    private Text verAngle;
    private Text skillDamage;

    private Dictionary<int, SkillDetailItemUI> cacheItem = new Dictionary<int, SkillDetailItemUI>();
    private int selectId = -1;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.skillDetailUI;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {
        temp = this.CacheTrans.Find("SkillContent/skillItem").gameObject;
        choose = this.CacheTrans.Find("choose").gameObject;
        temp.SetActive(false);
        normalSkill = this.CacheTrans.Find("SkillDetailContent/normalSkill").gameObject;

        skillId = this.CacheTrans.Find("SkillDetailContent/common/skillIdPre/skillId").GetComponent<Text>();
        skillName = this.CacheTrans.Find("SkillDetailContent/common/skillNamePre/skillName").GetComponent<Text>();
        skillDesc = this.CacheTrans.Find("SkillDetailContent/common/skillDescPre/skillDesc").GetComponent<Text>();
        skillModeType = this.CacheTrans.Find("SkillDetailContent/common/skillModeTypePre/skillModeType").GetComponent<Text>();
        atkType = this.CacheTrans.Find("SkillDetailContent/common/atkTypePre/atkType").GetComponent<Text>();
        atkRange = this.CacheTrans.Find("SkillDetailContent/normalSkill/atkRangePre/atkRange").GetComponent<Text>();
        horAngle = this.CacheTrans.Find("SkillDetailContent/normalSkill/horAnglePre/horAngle").GetComponent<Text>();
        verAngle = this.CacheTrans.Find("SkillDetailContent/normalSkill/verAnglePre/verAngle").GetComponent<Text>();
        skillDamage = this.CacheTrans.Find("SkillDetailContent/normalSkill/skillDamagePre/skillDamage").GetComponent<Text>();
        UIUtils.addCommonBg(this, CommonBgType.AnimBG_Fog);
    }

    public override void refreshUI()
    {
        SkillDetailData dt = this.data as SkillDetailData;
        if (dt != null)
        {
            for (int i = 0; i < dt.lst.Count; i++)
            {
                SkillDetailItemData itemData = dt.lst[i];
                if (!cacheItem.ContainsKey(itemData.id))
                {
                    GameObject go = UIUtils.cloneObj(temp, temp.transform.parent);
                    SkillDetailItemUI itemUI = go.AddComponent<SkillDetailItemUI>();
                    itemUI.setParentUI(this);
                    cacheItem.Add(itemData.id, itemUI);
                }
                cacheItem[itemData.id].setData(itemData);
            }
            if (selectId == -1)
            {
                onSkillDetailItemClick(dt.lst[0]);
            }
        }
    }

    public void onSkillDetailItemClick(SkillDetailItemData itemData)
    {
        skillId.text = itemData.id.ToString();
        skillName.text = itemData.skillName;
        skillDesc.text = itemData.skillDesc;
        skillModeType.text = itemData.skillModeType;
        atkType.text = itemData.atkType;
        atkRange.text = itemData.atkRange.ToString();
        horAngle.text = itemData.horAngle.ToString();
        verAngle.text = itemData.verAngle.ToString();
        skillDamage.text = itemData.skillDamage.ToString();

        if (cacheItem.ContainsKey(itemData.id))
        {
            choose.transform.SetParent(cacheItem[itemData.id].CacheTrans);
            choose.transform.localPosition = Vector3.zero;
        }

    }



}
