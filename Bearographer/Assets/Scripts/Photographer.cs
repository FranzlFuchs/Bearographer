using System.Collections;
using System.Collections.Generic;
using PlayerControllerInputSysten;
using UnityEngine;

public class Photographer : Player
{
    public override float GetHorizontalAxis()
    {
        return InputSystem.HorizontalRawArrows();
    }

    public override bool GetJump()
    {
        return InputSystem.JumpUp();
    }

    public override void TriggerEnter(Collider2D coll)
    {
        return;
    }

    public override void TriggerExit(Collider2D coll)
    {
        return;
    }
}
