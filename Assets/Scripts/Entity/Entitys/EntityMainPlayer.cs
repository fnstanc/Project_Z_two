using System;
using System.Collections.Generic;
using UnityEngine;
//using VRTK;

public class EntityMainPlayer : EntityDynamicActor
{
    [SerializeField]
    private float moveSpeed = 0.1f;

    private WeaponType wType = WeaponType.none;
    public WeaponType WType
    {
        get
        {
            return this.wType;
        }
        set
        {
            if (this.wType != value)
            {
                if (rightWeapon != null)
                    rightWeapon.onDispose();
                if (leftWeapon != null)
                    leftWeapon.onDispose();

                this.wType = value;
                onChangeWeapon();
            }
        }
    }
    private BaseWeapon rightWeapon = null;
    private BaseWeapon leftWeapon = null;

    private Transform leftHand;
    public Transform LeftHand
    {
        get
        {
            if (leftHand == null)
            {
                leftHand = this.CacheTrans.Find("Controller (left)");
            }
            return leftHand;
        }
    }
    private Transform rightHand;
    public Transform RightHand
    {
        get
        {
            if (rightHand == null)
            {
                rightHand = this.CacheTrans.Find("Controller (right)");
            }
            return rightHand;
        }
    }
    private Transform eye;
    public Transform Eye
    {
        get
        {
            if (eye == null)
            {
                eye = this.CacheTrans.Find("Camera (eye)");
            }
            return eye;
        }
    }



    public override void onStart()
    {
        base.onStart();
        this.CacheObj.layer = 11;
        this.WType = WeaponType.gun;
        resetCamera();
    }

    private void resetCamera()
    {
        Camera main = Camera.main;
        if (main == null)
        {
            main = new GameObject("MainCamera").AddComponent<Camera>();
            main.tag = "MainCamera";
            MainCameraWidget mcw = main.GetComponent<MainCameraWidget>();
            if (mcw == null)
            {
                mcw = main.gameObject.AddComponent<MainCameraWidget>();
                mcw.setAgent(this);
            }
        }
    }


    public Vector3 getCanvasPos()
    {
        Vector3 pos = this.Eye.position + this.Eye.forward * 12;
        return new Vector3(pos.x, 4f, pos.z);
    }
    public Quaternion getCanvasRot()
    {
        Quaternion rot = this.Eye.rotation;
        rot.x = 0;
        rot.z = 0;
        return rot;
    }

    private void OnEnable()
    {
        MessageCenter.Instance.addListener(MsgCmd.On_Change_Weapon, onChangeWeaponMsg);
        MessageCenter.Instance.addListener(MsgCmd.On_Change_Value, onChangeValue);
    }
    private void OnDisable()
    {
        MessageCenter.Instance.removeListener(MsgCmd.On_Change_Weapon, onChangeWeaponMsg);
        MessageCenter.Instance.removeListener(MsgCmd.On_Change_Value, onChangeValue);
    }

    private void onChangeWeaponMsg(Message msg)
    {
        WeaponType type = (WeaponType)msg["type"];
        this.WType = type;
    }

    //切换武器
    private void onChangeWeapon()
    {
        WeaponFactory.Instance.createWeapon(this.WType, this);
    }
    //普通武器射击
    //private void onFire(object sender, ControllerInteractionEventArgs args)
    //{
    //    if (rightWeapon.isCanUse())
    //    {
    //        if (rightWeapon != null)
    //            rightWeapon.onFire();
    //        if (leftWeapon != null)
    //        {
    //            leftWeapon.onFire();
    //        }
    //    }
    //}
    //弓箭射击
    //private void bowOnFire(object sender, ControllerInteractionEventArgs args)
    //{
    //    if (rightWeapon != null)
    //        rightWeapon.bowOnFire();
    //    if (leftWeapon != null)
    //    {
    //        RightEvents.TriggerReleased -= bowOnFire;
    //        RightEvents.TriggerOnPress -= onPress;
    //        leftWeapon.bowOnFire();
    //    }

    //}
    //弓箭拉弓
    //private void onPress(object sender, ControllerInteractionEventArgs args)
    //{
    //    if (this.leftWeapon != null)
    //    {
    //        Vector3 rightVec = RightHand.InverseTransformPoint(LeftHand.position);
    //        leftWeapon.bowOnPull(rightVec.z);
    //    }
    //}
    //弓箭放弃射击
    public void bowGiveUP()
    {
        if (leftWeapon != null)
        {
            WeaponBow bow = leftWeapon as WeaponBow;
            if (bow != null)
            {
                bow.onGiveUP();
            }
        }
        if (rightWeapon != null)
        {
            WeaponArrow arrow = rightWeapon as WeaponArrow;
            if (arrow != null)
            {
                arrow.onGiveUP();
            }
        }

    }

    public void setRightWeapon(BaseWeapon bw)
    {
        rightWeapon = bw;
    }
    public void setLeftWeapon(BaseWeapon bw)
    {
        leftWeapon = bw;
    }

    public override void onDispose()
    {
        base.onDispose();
    }


    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        fsm = new PlayerFSM(this);
        onChangeState(StateType.spawn);
        sendHPMsg();
        this.BillBoard.setColorByType(PartType.namePart, Color.green);
    }

    public override void onDamage(DamageData dt)
    {
        this.HP -= dt.damage;
        sendHPMsg();
        UIMgr.Instance.onDamageColor();
    }

    private void sendHPMsg()
    {
        Message msg = new Message(MsgCmd.On_HP_Change_Value, this);
        msg["HP"] = this.HP;
        msg["OrgHP"] = this.OrgHP;
        msg.Send();
        if (this.HP <= 0)
        {
            Message msgdie = new Message(MsgCmd.Die_Main_Player, this);
            msgdie.Send();
        }
    }

    //玩家移动
    //触摸TouchPad 主角移动
    Quaternion rot = Quaternion.identity;
    Vector3 dir = Vector3.zero;
    public void onPlayerMove(MoveData data)
    {
        dir = data.moveDir;
        dir.z = dir.y;
        dir.y = 0;
        float angle = Vector3.Angle(Vector3.forward, dir);
        angle = dir.x < 0 ? 360 - angle : angle;
        rot = Quaternion.Euler(0, angle, 0);
        this.CacheTrans.rotation = Quaternion.Lerp(this.CacheTrans.rotation, rot, 0.5f);
        CC.Move(dir * moveSpeed);
    }
    public void onPlayerMoveStart(MoveData data)
    {
        this.onChangeState(StateType.run);
    }
    public void onPlayerMoveEnd(MoveData data)
    {
        this.onChangeState(StateType.idle);
    }

}

