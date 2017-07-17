using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSysUI : BaseUI
{
    private GameObject slot;
    private Text money;
    private Text score;
    private Text energy;
    private Dictionary<int, WeaponSysItem> dictItem = null;

    public override void resetUIInfo()
    {
        uiEnum = UIEnum.weaponSys;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {
        base.onStart();
        slot = this.CacheTrans.Find("itemContent/weaponSlot").gameObject;
        money = this.CacheTrans.Find("propertyContent/money/moneyText").GetComponent<Text>();
        score = this.CacheTrans.Find("propertyContent/score/scoreText").GetComponent<Text>();
        energy = this.CacheTrans.Find("propertyContent/energy/energyText").GetComponent<Text>();
        slot.SetActive(false);
        dictItem = new Dictionary<int, WeaponSysItem>();
        MessageCenter.Instance.addListener(MsgCmd.On_BB_Change_Value, onPropertyChanage);
    }

    private void onPropertyChanage(Message msg)
    {

    }

    public override void refreshUI()
    {
        insSlot();
    }

    private void insSlot()
    {
        WeaponSystemData dt = this.data as WeaponSystemData;
        if (dt == null)
        {
            return;
        }
        money.text = "金币: " + dt.Money.ToString();
        score.text = "分数: " + dt.Score.ToString();
        energy.text = "能量: " + dt.Energy.ToString();

        List<WeaponSysItemData> lst = dt.WeaponInfoLst;
        if (lst != null && lst.Count > 0)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (!dictItem.ContainsKey(i))
                {
                    GameObject go = MonoBehaviour.Instantiate(slot, slot.transform.parent) as GameObject;
                    go.SetActive(true);
                    WeaponSysItem item = go.AddComponent<WeaponSysItem>();
                    dictItem[i] = item;
                }
                dictItem[i].setData(lst[i]);
            }
        }
    }

    public override void onDispose()
    {
        MessageCenter.Instance.removeListener(MsgCmd.On_BB_Change_Value, onPropertyChanage);
    }
}

