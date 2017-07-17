using System;
using System.Collections.Generic;
using UnityEngine;

//动态实体
public class EntityDynamicActor : BaseEntity, ISkill
{
    [HideInInspector]
    private Animation _anim = null;
    public Animation anim
    {
        get
        {
            if (this._anim == null)
                _anim = this.GetComponent<Animation>();
            return this._anim;
        }

    }
    [HideInInspector]
    public Animator animator = null;
    [HideInInspector]
    public CharacterController CC = null;

    public FSM fsm = null;
    protected SkillWidget MySkill = null;
    protected WordBubbleWidget WBW = null;
    public BaseEntity Target
    {
        get
        {
            int id = this.BB.getValue<int>(Attr.target.ToString());
            return EntityMgr.Instance.getEntityById(id);
        }
        set
        {
            BaseEntity entity = value as BaseEntity;
            this.BB.onValueChange(Attr.target.ToString(), entity.UID);
        }
    }

    public StateType SType = StateType.none;

    public override void onStart()
    {
        animator = this.GetComponent<Animator>();
        CC = this.GetComponent<CharacterController>();
        if (CC == null)
        {
            CC = this.CacheObj.AddComponent<CharacterController>();
        }
        if (CC != null && this.info != null)
        {
            CC.height = this.info.NameHeight;
            CC.center = new Vector3(0, this.info.NameHeight / 2, 0);
            CC.radius = 0.3f;//配置表没配
        }
    }

    private bool isUseG = true;
    public override void onUpdate()
    {
        base.onUpdate();
        if (fsm != null)
            fsm.onTick();
        if (CC.enabled && isUseG)
            CC.SimpleMove(Vector3.zero);
    }

    public void setUseGrivaty(bool isUse)
    {
        isUseG = isUse;
    }
    public void moveTo(Vector3 dir)
    {
        if (CC.enabled)
            CC.SimpleMove(dir);
    }

    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        // this.BB.onValueChange(Attr.target.ToString(), null);
        //创建姓名版 血条等..
        BillBoard = this.CacheObj.AddComponent<DynamicBillBoard>();
        BillBoard.onCreate(this.info);
        //创建技能管理器
        MySkill = new SkillWidget(this, this.Skills);
    }

    public virtual bool onChangeState(StateType type, FSMArgs args = null)
    {
        if (this.fsm != null)
        {
            SType = type;
            return this.fsm.onChangeState(type, args);
        }
        return false;
    }
    public virtual bool isCanChangeState(StateType type)
    {
        return this.fsm.isCanChangeState(type);
    }

    public SkillWidget getSkillWidget()
    {
        return this.MySkill;
    }

    public virtual void onReleaseSkill(int skillId, FSMArgs args = null)
    {
        StateType stateType = StateType.die;
        if (args != null && args.skillData != null)
        {
            stateType = args.skillData.fsmStateType;
        }
        bool canChangeState = isCanChangeState(stateType);
        //Debug.Log("释放技能 id == " + skillId + "   canCastSkill : " + canCastSkill + "  canChangeState : " + canChangeState);
        if (MySkill.releaseSkill(skillId, canChangeState) && canChangeState)
        {
            onChangeState(stateType, args);
            Message msg = new Message(MsgCmd.On_Skill_Release_Success, this);
            msg["skillId"] = skillId;
            msg.Send();
        }

    }
    //说话接口
    public void sayWord(string str, bool isNotRunShow = false)
    {
        if (WBW == null)
        {
            //GameObject go = new GameObject("WordBubble");
            //go.transform.SetParent(this.CacheTrans);
            //WBW = go.AddComponent<WordBubbleWidget>();
            ResMgr.Instance.load("UI/WordBubble", (obj) =>
            {
                GameObject go = obj as GameObject;
                go.transform.SetParent(this.CacheTrans);
                go.transform.localPosition = new Vector3(0, 3, 0);
                WBW = go.GetComponent<WordBubbleWidget>();
                if (WBW == null)
                {
                    WBW = go.AddComponent<WordBubbleWidget>();
                }
                WBW.setWord(str, isNotRunShow);
            });
        }
        else
        {
            WBW.setWord(str, isNotRunShow);
        }
    }

}

