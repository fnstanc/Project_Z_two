using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageTipsItemUI : BaseUI
{
    private Text DamageText;
    private Color textColor = Color.red;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        DamageText = this.CacheTrans.Find("DamageText").GetComponent<Text>();
        this.DamageText.gameObject.SetActive(false);
    }

    public override void refreshUI()
    {
        DamageData dt = this.data as DamageData;
        if (dt != null)
        {
            this.DamageText.gameObject.SetActive(false);
            BaseEntity agent = EntityMgr.Instance.getEntityById(dt.targetId);
            if (agent != null)
            {
                textColor = EntityMgr.Instance.isMainPlayer(agent) ? Color.red : Color.yellow;
                this.DamageText.color = textColor;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(agent.CacheTrans.position);
                this.DamageText.text = "- " + dt.damage;
                doAnim(getEndPoint(screenPos));
            }
        }
    }

    private void doAnim(Vector3 endPoint)
    {
        resetItem();
        this.DamageText.gameObject.SetActive(true);
        this.CacheTrans.DOScale(Vector3.one, 0.15f).OnComplete(() =>
        {
            this.CacheTrans.DOMove(endPoint, 0.35f).OnComplete(() =>
            {
                if (this.parentUI != null)
                {
                    DamageTipsUI ui = this.parentUI as DamageTipsUI;
                    ui.showNext();
                }
                this.DamageText.DOFade(0, 0.15f).SetDelay(0.5f).OnComplete(() =>
                {
                    resetItem();
                    if (this.parentUI != null)
                    {
                        DamageTipsUI ui = this.parentUI as DamageTipsUI;
                        ui.cacheItem(this);
                        ui.checkQueue(this.index);
                    }
                });
            });
        });

    }

    private void resetItem()
    {
        //缩放
        this.CacheTrans.localScale = Vector3.zero;
    }
    private int index = 0;
    public void setIndex(int index)
    {
        this.index = index;
    }

    private Vector3 getEndPoint(Vector3 screenPos)
    {
        Vector3 point = Vector3.zero;
        float height = UIUtils.getScreenHeight();
        screenPos.y = 150 + screenPos.y > height ? height - 60 : 150 + screenPos.y;
        this.CacheTrans.position = screenPos;
        float end = UnityEngine.Random.Range(-60f, 60f);
        point = new Vector3(screenPos.x + end, screenPos.y + 90 > height ? height : screenPos.y + UnityEngine.Random.Range(60, 90), screenPos.z);
        return point;
    }

}

