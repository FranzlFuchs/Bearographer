using System.Collections;
using System.Collections.Generic;
using SupanthaPaul;
using UnityEngine;

public class BearController : PlayerController
{
    public BearController()
    {
        _isactive = false;
    }

    public override float GetHorizontalAxis()
    {
        return InputSystem.HorizontalRawAD();
    }

    public override bool GetJump()
    {
        return InputSystem.JumpW();
    }

    public void SetActive()
    {
        _isactive = true;
    }

    public void SetInActive()
    {
        _isactive = false;
    }
}
