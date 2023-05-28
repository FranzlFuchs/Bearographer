using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandBy_PlayerState : PlayerState
{
    public StandBy_PlayerState(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {
        _player.UpdateAnimJump();
        return;
    }

    public override void DoStateFixedUpdate()
    {
        return;
    }

    public override void EnterState()
    {
        _player.FreezeBody();
        if(_player.OnTreeTrunk)
        {
            _player.TurnOffGravity();
        }
        _player.StopAnimJump();
        _player.SetAnimIdle();
    }

    public override void ExitState()
    {
        return;
    }
}
