using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FuncMenuUI : BaseUI
{
    private Button flexBtn;
    private bool isOpen = true;

    private GameObject content;

    private GameObject tempItem;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.funcMenuUI;
        this.uiNode = UINode.root;
    }

    public override void onStart()
    {
        content = this.CacheTrans.Find("Content").gameObject;
        tempItem = this.CacheTrans.Find("Content/menuItem").gameObject;
        tempItem.SetActive(false);
        flexBtn = this.CacheTrans.Find("flexBtn").GetComponent<Button>();
        flexBtn.onClick.AddListener(() =>
        {
            content.transform.DOScale(isOpen ? 0 : 1, 0.25f).SetEase(Ease.InOutBack);
            isOpen = !isOpen;
            flexBtn.gameObject.transform.localScale = new Vector3(isOpen ? 1 : -1, 1, 1);
        });

    }

    public override void refreshUI()
    {
        FuncMenuData dt = this.data as FuncMenuData;
        if (dt != null)
            refreshItem(dt);
    }

    private void refreshItem(FuncMenuData dt)
    {
        for (int i = 0; i < dt.lst.Count; i++)
        {
            GameObject go = UIUtils.cloneObj(tempItem, content.transform);
            if (go != null)
            {
                go.SetActive(true);
                FuncMenuItemUI itemUI = go.AddComponent<FuncMenuItemUI>();
                itemUI.setData(dt.lst[i]);
            }
        }
    }




}

