using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallgrab_PlayerState : PlayerState
{
    public Wallgrab_PlayerState(Player player)
        : base(player) { }

    public override void DoStateUpdate()
    {
        return;
    }

    public override void DoStateFixedUpdate()
    {
        _player.SlideBody();

        _player.UpdateWallStickTime();
        _player.UpdateOnWall();

        if (_player.GetWallStickTime() <= 0f)
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
        }
        if (!_player.GetIsOnWall())
        {
            _player.ChangeState(new Idle_Standing_PlayerState(_player));
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
