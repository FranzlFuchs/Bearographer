using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_PlayerState : PlayerState
{



    public Moving_PlayerState(Player player) :
        base(player)
    {

    }

    public override void DoStateUpdate()
    {
        if (!_player.IsMoving())
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
        }


        if (_player.GetJump() && !_player.OnTreeTrunk) 
        {
            _player.ChangeState(new Jumping_PlayerState(_player));
        }

        if (_player.GetJump() && _player.OnTreeTrunk) 
        {
            _player.ChangeState(new CrawlingTreeTrunk_Idle_PlayerState(_player));
        }

        _player.UpdateAnimJump();
    }

    public override void DoStateFixedUpdate()
    {
        int playerSide = _player.GetPlayerSide();
        _player.UpdateIsGrounded();
        _player.CalculateSides();
        _player.UpdateOnWall();
        _player.UpdateFlip();

        if(playerSide != _player.GetPlayerSide())
        {

            _player.ChangeState(new Moving_PlayerState(_player));
        }
    }

    public override void EnterState()
    {
        Debug.Log("MOVING");        
        _player.MoveBody();
        _player.SetAnimMoving();
    }

    public override void ExitState()
    {
        return;
    }
}
