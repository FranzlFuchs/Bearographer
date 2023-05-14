using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_PlayerState : PlayerState
{
     public Moving_PlayerState(Player player) : base(player)
    {
    }
 
    public override void DoStateUpdate()
    {
    }
    public override void DoStateFixedUpdate()
    {
        _player.UpdateIsGrounded();
        _player.UpdateFlip();
    }

    public override void EnterState()
    {
        _player.MoveBody();
        _player.SetAnimMoving();
    }

    public override void ExitState()
    {
       return;
    }

  
  
}
