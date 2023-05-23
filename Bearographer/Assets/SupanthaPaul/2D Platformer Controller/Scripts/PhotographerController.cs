using System.Collections;
using System.Collections.Generic;
using SupanthaPaul;
using UnityEngine;

public class PhotographerController : PlayerController
{
    public override float GetHorizontalAxis()
    {
        return InputSystem.HorizontalRawArrows();
    }

    
   public override bool GetJump()
    {
        return InputSystem.JumpUp();
    }
}
