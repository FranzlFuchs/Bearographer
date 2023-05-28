using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingTreeTrunk_Idle_PlayerState : PlayerState
{
    public CrawlingTreeTrunk_Idle_PlayerState(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {
        if (_player.IsMovingTree())
        {
            if (_player.moveInputVert > 0.0f )
            {
                _player
                    .ChangeState(new CrawlingTreeTrunk_Moving_Playerstate(_player));
            }

            if (_player.moveInputVert < 0.0f )
            {
                //Dash down
                _player.ChangeState(new Crawling_DashDown_PlayerState(_player));
            }
         
        }

        //if (_player.GetJump())
        {
            //  _player.ChangeState(new Jumping_PlayerState(_player));
        }
    }

    public override void DoStateFixedUpdate()
    {
        _player.UpdateIsGrounded();
        _player.UpdateOnWall();
        return;
    }

    public override void EnterState()
    {
        //Set Crawling Idle
        Debug.Log("CRAWLING IDLE");
        _player.FreezeBody();
        //_player.SetTreePosition();
        _player.TurnOffGravity();
        return;
    }

    public override void ExitState()
    {
        _player.TurnOnGravity();
        return;
    }
}
