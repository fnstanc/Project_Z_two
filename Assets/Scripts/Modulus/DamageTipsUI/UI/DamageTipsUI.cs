using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTipsUI : BaseUI
{
    private Queue<DamageTipsItemUI> cacheQueue = new Queue<DamageTipsItemUI>();
    private int index = 0;

    private GameObject DamageItem1;
    private GameObject DamageItem2;
    private GameObject DamageItem3;
    private GameObject DamageItem4;
    private GameObject DamageItem5;
    private GameObject DamageItem6;
    private GameObject DamageItem7;
    private GameObject DamageItem8;
    private GameObject DamageItem9;
    private GameObject DamageItem10;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.damageTipsUI;
        this.uiNode = UINode.root;
    }

    public override void onStart()
    {
        DamageItem1 = this.CacheTrans.Find("DamageItem1").gameObject;
        DamageTipsItemUI item1 = DamageItem1.AddComponent<DamageTipsItemUI>();
        cacheItem(item1);
        DamageItem2 = this.CacheTrans.Find("DamageItem2").gameObject;
        DamageTipsItemUI item2 = DamageItem2.AddComponent<DamageTipsItemUI>();
        cacheItem(item2);
        DamageItem3 = this.CacheTrans.Find("DamageItem3").gameObject;
        DamageTipsItemUI item3 = DamageItem3.AddComponent<DamageTipsItemUI>();
        cacheItem(item3);
        DamageItem4 = this.CacheTrans.Find("DamageItem4").gameObject;
        DamageTipsItemUI item4 = DamageItem4.AddComponent<DamageTipsItemUI>();
        cacheItem(item4);
        DamageItem5 = this.CacheTrans.Find("DamageItem5").gameObject;
        DamageTipsItemUI item5 = DamageItem5.AddComponent<DamageTipsItemUI>();
        cacheItem(item5);
        DamageItem6 = this.CacheTrans.Find("DamageItem6").gameObject;
        DamageTipsItemUI item6 = DamageItem6.AddComponent<DamageTipsItemUI>();
        cacheItem(item6);
        DamageItem7 = this.CacheTrans.Find("DamageItem7").gameObject;
        DamageTipsItemUI item7 = DamageItem7.AddComponent<DamageTipsItemUI>();
        cacheItem(item7);
        DamageItem8 = this.CacheTrans.Find("DamageItem8").gameObject;
        DamageTipsItemUI item8 = DamageItem8.AddComponent<DamageTipsItemUI>();
        cacheItem(item8);
        DamageItem9 = this.CacheTrans.Find("DamageItem9").gameObject;
        DamageTipsItemUI item9 = DamageItem9.AddComponent<DamageTipsItemUI>();
        cacheItem(item9);
        DamageItem10 = this.CacheTrans.Find("DamageItem10").gameObject;
        DamageTipsItemUI item10 = DamageItem10.AddComponent<DamageTipsItemUI>();
        cacheItem(item10);


    }

    public override void refreshUI()
    {
        DamageTipsData dt = this.data as DamageTipsData;
        if (dt != null)
            doDamageAnim(dt);
    }


    private void doDamageAnim(DamageTipsData dt)
    {
        //control那边做过滤
        if (dt.dataQueue.Count > 0)
        {
            DamageData data = dt.dataQueue.Dequeue();
            //如果没有缓存的item 实例化
            if (cacheQueue.Count == 0)
            {
                GameObject go = UIUtils.cloneObj(DamageItem1, DamageItem1.transform.parent);
                DamageTipsItemUI item = go.GetComponent<DamageTipsItemUI>();
                if (item == null)
                {
                    item = go.AddComponent<DamageTipsItemUI>();
                }
                cacheItem(item);
            }

            //如果还有缓存的item
            if (cacheQueue.Count > 0)
            {
                DamageTipsItemUI item = cacheQueue.Dequeue();
                item.setActive(true);
                item.setParentUI(this);
                item.setData(data);
                this.index++;
                item.setIndex(this.index);
            }
        }

    }
    //显示下一个
    public void showNext()
    {
        DamageTipsData dt = this.data as DamageTipsData;
        if (dt != null && dt.dataQueue.Count > 0)
            doDamageAnim(dt);
    }

    //缓存item
    public void cacheItem(DamageTipsItemUI item)
    {
        item.setActive(false);
        cacheQueue.Enqueue(item);
    }

    //检查队列
    public void checkQueue(int itemIndex)
    {
        if (itemIndex == this.index)
        {
            DamageTipsData dt = this.data as DamageTipsData;
            if (dt == null || dt.dataQueue.Count == 0)
            {
                closeSelfUI();
            }
            else if(dt != null || dt.dataQueue.Count > 0)  {
                doDamageAnim(dt);
            }              
        }
    }
}

