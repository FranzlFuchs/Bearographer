using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_PlayerState : PlayerState
{
    public Jumping_PlayerState(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {
        _player.UpdateAnimJump();
    }

    public override void DoStateFixedUpdate()
    {
        _player.UpdateIsGrounded();

        if (_player.isGrounded)
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
        }

        //_player.UpdateFlip();
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
