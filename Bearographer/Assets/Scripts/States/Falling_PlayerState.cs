using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_PlayerState : PlayerState
{
    public Falling_PlayerState(Player player)
        : base(player) { }

    public override void DoStateUpdate()
    {
        _player.UpdateAnimJump();

        if (_player.GetJump() && _player.HasExtraJumps() && !_player.OnTreeTrunk)
        {
            _player.ChangeState(new Multiple_Jumping_Playerstate(_player));
        }    

        if (_player.GetJump() && _player.OnTreeTrunk)
        {
            _player.ChangeState(new CrawlingTreeTrunk_Idle_PlayerState(_player));
        }    

        if (_player.GetIsOnWall())
        {
            _player.ChangeState(new Wallgrab_PlayerState(_player));
        }

        if (_player.isGrounded)
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
        }

        _player.MoveBody();
        _player.UpdateAnimJump();
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
        Debug.Log("FALLING");        
        _player.SetAnimJump();
    }

    public override void ExitState()
    {
        _player.StopAnimJump();
        _player.SetAnimIdle();
    }
}
