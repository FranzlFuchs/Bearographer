using System.Collections;
using System.Collections.Generic;
using SupanthaPaul;
using UnityEngine;

public class BearController : PlayerController
{
   public override float GetHorizontalAxis()
    {
        return InputSystem.HorizontalRawAD();
    }
    
   public override bool GetJump()
    {
        return InputSystem.JumpW();
    }

}
