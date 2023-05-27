using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_PlayerState : PlayerState
{
    public Jumping_PlayerState(Player player)
        : base(player) { }

    public override void DoStateUpdate()
    {
        _player.UpdateAnimJump();

        if (_player.GetJump() && _player.HasExtraJumps())
        {
            _player.ChangeState(new Multiple_Jumping_Playerstate(_player));
        }

        if (_player.isGrounded && _player.GetVelocity().y <= 0)
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
        }

        if (_player.GetIsOnWall())
        {
            _player.ChangeState(new Wallgrab_PlayerState(_player));
        }

        _player.MoveBody();
    }

    public override void DoStateFixedUpdate()
    {
        _player.UpdateIsGrounded();
        _player.UpdateOnWall();
        _player.FixedUpdateJumpPhysics();
        _player.UpdateFlip();
    }

    public override void EnterState()
    {
        Debug.Log("JUMPING");
        _player.JumpBody();
        _player.SetAnimJump();
    }

    public override void ExitState()
    {
        _player.StopAnimJump();
        _player.SetAnimIdle();
    }
}
