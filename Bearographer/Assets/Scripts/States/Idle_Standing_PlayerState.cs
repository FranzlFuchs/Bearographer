using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Standing_PlayerState : PlayerState
{
    public Idle_Standing_PlayerState(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {
        if (_player.IsMoving())
        {
            _player.ChangeState(new Moving_PlayerState(_player));
        }

        if (_player.GetJump())
        {
            _player.ChangeState(new Jumping_PlayerState(_player));
        }
    }

    public override void DoStateFixedUpdate()
    {
    }

    public override void EnterState()
    {
        Debug.Log("IDLE_Standing");
        _player.StopBody();
        _player.SetAnimIdle();
    }

    public override void ExitState()
    {
        return;
    }
}
