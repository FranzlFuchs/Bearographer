using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawling_DashDown_PlayerState : PlayerState
{
    public Crawling_DashDown_PlayerState(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {
        if (_player.isGrounded)
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
        }

        _player.MoveBody();

        return;
    }

    public override void DoStateFixedUpdate()
    {
        _player.UpdateIsGrounded();
        return;
    }

    public override void EnterState()
    {
         Debug.Log("CRAWLING DASH");
         _player.MoveBodyDashDown();
       
    }

    public override void ExitState()
    {
        _player.StopAnimJump();
        _player.SetAnimIdle();
    }
}
