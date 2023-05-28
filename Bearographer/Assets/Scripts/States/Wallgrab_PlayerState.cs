using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallgrab_PlayerState : PlayerState
{
    public Wallgrab_PlayerState(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {

        if (_player.GetJump())
        {   
            _player.CalculateSides();
            _player.ChangeState(new Walljump_PlayerState(_player));
            return;
        }
        
        //EVentuell noch WallClim einbaun, hat noch Fehler

        /*
        if (_player.GetJump() && _player.moveInput != _player.GetWallSide() )
        {   
            _player.CalculateSides();
            _player.ChangeState(new Walljump_PlayerState(_player));
            return;
        }


        if (_player.GetJump() && _player.moveInput == _player.GetWallSide())
        {
            _player.CalculateSides();
            _player.ChangeState(new Wallclimb_PlayerState(_player));
            return;
        }
        */
    }

    public override void DoStateFixedUpdate()
    {
        _player.SlideBody();

        _player.UpdateWallStickTime();
        _player.UpdateOnWall();
        _player.UpdateIsGrounded();

        if (_player.GetWallStickTime() <= 0f)
        {
            _player.ChangeState(new Falling_PlayerState(_player));
        }

        if (!_player.isGrounded && !_player.GetIsOnWall())
        {
            _player.ChangeState(new Falling_PlayerState(_player));
        }
    }

    public override void EnterState()
    {
        Debug.Log("WALL GRAB");
        _player.ResetWallStickTime();
        _player.SetAnmimWallGrab(); // for animation
    }

    public override void ExitState()
    {
        _player.StopAnmimWallGrab(); // for animation
        _player.actuallyWallGrabbing = false;
    }
}
