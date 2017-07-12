using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : BaseUI
{
    private GameObject baseSkill;
    private GameObject skill1;
    private GameObject skill2;
    private GameObject skill3;
    private GameObject skill4;
    private GameObject dodgeSkill;
    private List<GameObject> skillMap = new List<GameObject>();

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.skillUI;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {
        baseSkill = this.CacheTrans.Find("baseSkill").gameObject;
        skill1 = this.CacheTrans.Find("skill1").gameObject;
        skill2 = this.CacheTrans.Find("skill2").gameObject;
        skill3 = this.CacheTrans.Find("skill3").gameObject;
        skill4 = this.CacheTrans.Find("skill4").gameObject;
        dodgeSkill = this.CacheTrans.Find("dodgeSkill").gameObject;
        skillMap.Add(skill1);
        skillMap.Add(skill2);
        skillMap.Add(skill3);
        skillMap.Add(skill4);
    }

    public override void refreshUI()
    {
        SkillUIData dt = this.data as SkillUIData;
        if (dt != null)
        {
            refreshNormalSkill(dt);
            refreshBaseSkill(dt);
            refreshDodgeSkill(dt);
        }
    }

    //normalSkill
    private void refreshNormalSkill(SkillUIData dt)
    {
        List<SkillItemUIData> lst = dt.getNormalSkillData();
        for (int i = 0; i < lst.Count; i++)
        {
            GameObject go = skillMap[i];
            if (go == null) break;
            SkillItemUI item = go.GetComponent<SkillItemUI>();
            if (item == null)
                item = go.AddComponent<SkillItemUI>();
            if (item != null)
            {
                item.setData(lst[i]);
            }
        }
    }

    //baseSkill
    private void refreshBaseSkill(SkillUIData dt)
    {
        SkillItemUI skillUI = baseSkill.GetComponent<SkillItemUI>();
        if (skillUI == null)
            skillUI = baseSkill.AddComponent<SkillItemUI>();

        if (skillUI != null)
            skillUI.setData(dt.getBaseSkillData());
    }

    //dodgeSkill
    private void refreshDodgeSkill(SkillUIData dt)
    {
        SkillItemUI skillUI = dodgeSkill.GetComponent<SkillItemUI>();
        if (skillUI == null)
            skillUI = dodgeSkill.AddComponent<SkillItemUI>();

        if (skillUI != null)
            skillUI.setData(dt.getDodgeSkillData());
    }

}

