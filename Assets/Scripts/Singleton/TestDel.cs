using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestDel : MonoBehaviour
{
    private CharacterController cc;

    private void Start()
    {
        cc = this.GetComponent<CharacterController>();
        this.cc.SimpleMove(Vector3.up * -1);
    }

    private void Update()
    {
        //if (!cc.isGrounded)
        //{
        //    this.cc.SimpleMove(Vector3.up * -1);
        //}
    }

}

