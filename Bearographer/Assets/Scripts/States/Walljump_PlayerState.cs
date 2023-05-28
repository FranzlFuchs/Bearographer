using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walljump_PlayerState : PlayerState
{
      public Walljump_PlayerState(Player player)
        : base(player) { }

    public override void DoStateUpdate()
    {
        _player.UpdateAnimJump();

      
        if (_player.GetVelocity().y <= 0)
        {
            _player.ChangeState(new Falling_PlayerState(_player));
        }

       

        _player.UpdateAnimJump();
    }

    public override void DoStateFixedUpdate()
    {
        _player.UpdateIsGrounded();
        _player.UpdateOnWall();
        _player.FixedUpdateWallJumpPhysics();
        _player.UpdateFlip();
    }

    public override void EnterState()
    {
        Debug.Log("WALLJUMPING");
        _player.WallJumpBody();
        _player.SetAnimJump();
    }

    public override void ExitState()
    {
        _player.StopAnimJump();
        _player.SetAnimIdle();
    }
}
