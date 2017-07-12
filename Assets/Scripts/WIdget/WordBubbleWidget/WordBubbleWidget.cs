using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WordBubbleWidget : MonoBehaviour
{
    private const string wordText = "WordText";
    private const float startHeight = 0.6f;
    private const float addHeight = 0.2f;
    private const float maxWidth = 3.5f;
    private const float addWidth = 0.011667f;
    private const float textStartY = 1.5f;
    private const float textAddY = 0.1f;

    private const int rowSize = 300;
    private int changeRowSize = 300;

    private SpriteRenderer sr;
    public SpriteRenderer SR
    {
        get
        {
            if (this.sr == null)
            {
                this.sr = this.gameObject.GetComponent<SpriteRenderer>();
            }
            return this.sr;
        }
    }
    private TextMesh tm;
    public TextMesh TM
    {
        get
        {
            if (this.tm == null)
            {
                this.tm = this.transform.Find(wordText).GetComponent<TextMesh>();
            }
            return this.tm;
        }
    }

    private Queue<string> wordQueue = new Queue<string>();
    private bool isRun = false;

    public void setWord(string text, bool isNotRunShow)
    {
        if (!isNotRunShow || (isNotRunShow && !isRun))
            wordQueue.Enqueue(text);
        runWord();
    }

    private void runWord()
    {
        if (wordQueue.Count > 0 && !isRun)
        {
            updateWord(wordQueue.Dequeue());
        }
    }

    //设置对话气泡文字接口
    private void updateWord(string text)
    {
        isRun = true;
        int row = 1;
        int fontsize = TM.fontSize;
        Font font = TM.font;
        string newText = text;
        font.RequestCharactersInTexture(text, fontsize, TM.fontStyle);
        CharacterInfo characterInfo;
        float width = 0f;
        for (int i = 0; i < text.Length; i++)
        {
            font.GetCharacterInfo(text[i], out characterInfo, fontsize);
            width += characterInfo.advance;
            if (width > changeRowSize)
            {
                changeRowSize += rowSize;
                newText = newText.Insert(i + (int)(width / rowSize), "\n");
                row += 1;
            }
        }
        changeRowSize = rowSize;

        float endWidth = row > 1 ? maxWidth : addWidth * width;
        float endHeight = startHeight + addHeight * row;

        float textEndY = row - 3 > 0 ? (textAddY * (row - 3)) + textStartY : textStartY;
        // this.gameObject.transform.localPosition = new Vector3(0, textEndY, 0);
        this.gameObject.SetActive(true);
        this.SR.color = new Color(1, 1, 1, 1);
        this.TM.color = new Color(1, 1, 1, 1);
        SR.size = new Vector2(endWidth, endHeight);
        TM.text = newText;
        this.transform.localScale = new Vector3(0, 0, 0);
        this.transform.DOScale(new Vector3(1, 1, 1), 0.3f).OnComplete(() =>
        {
            DOTween.To((t) => { this.SR.color = new Color(1, 1, 1, t); this.TM.color = new Color(1, 1, 1, t); }, 1, 0, 0.3f).SetDelay(5f).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
                isRun = false;
                runWord();
            });
        });
    }

    //对准摄像机
    void Update()
    {
        if (Camera.main != null)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Camera.main.transform.rotation, 0.9f);
        }
    }




}

