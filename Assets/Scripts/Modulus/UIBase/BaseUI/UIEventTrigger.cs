using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 orgScale = Vector3.zero;
    private Vector3 clickScale = Vector3.zero;

    private Action onClickHandler = null;
    private Action onClickUpHandler = null;
    private Action onEnterHandler = null;
    private Action onExitHandler = null;
    private float clickCD = 0.5f;
    [SerializeField]
    private bool isDoAnim = false;
    [SerializeField]
    private bool isCanClick = true;

    void Awake()
    {
        orgScale = this.transform.localScale;
        clickScale = new Vector3(orgScale.x + 0.2f, orgScale.y + 0.2f, 1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!checkCanClick()) return;
        if (onClickHandler != null)
        {
            onClickHandler();          
        }
        if (isDoAnim)
        {
            this.transform.DOScale(clickScale, 0.15f).OnComplete(() =>
            {
                this.transform.DOScale(orgScale, 0.15f);
            });
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!checkCanClick()) return;
        if (onClickUpHandler != null)
        {
            onClickUpHandler();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!checkCanClick()) return;
        if (onEnterHandler != null)
        {
            onEnterHandler();
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!checkCanClick()) return;
        if (onExitHandler != null)
        {
            onExitHandler();
        }
        this.transform.localScale = new Vector3(1f, 1f, 1);
    }

    public void setClickHandler(Action handler)
    {
        onClickHandler = handler;
    }
    public void setClickUpHandler(Action handler)
    {
        onClickUpHandler = handler;
    }
    public void setEnterHandler(Action handler)
    {
        onEnterHandler = handler;
    }
    public void setExitHandler(Action handler)
    {
        onExitHandler = handler;
    }

    //是否做点击缩放动画
    public void isShowClickAnim(bool b)
    {
        isDoAnim = b;
    }
    public void isCanClickBtn(bool b) {
        isCanClick = b;
    }
    //检查是否可以被点击
    private bool  checkCanClick() {
        return isCanClick;
    }

}

