using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingTreeTrunk_Moving_Playerstate : PlayerState
{
    public CrawlingTreeTrunk_Moving_Playerstate(Player player) :
        base(player)
    {
    }

    public override void DoStateUpdate()
    {
        if (!_player.IsMovingTree())
        {
            _player
                .ChangeState(new CrawlingTreeTrunk_Idle_PlayerState(_player));
        }
     
        else if (_player.moveInputVert < 0)
        {
            _player.ChangeState(new Crawling_DashDown_PlayerState(_player));
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
        //Set Anim Crawling Moving
        Debug.Log("CRAWLING MOVE");        
        _player.TurnOffGravity();
        _player.FreezeBody();
        _player.MoveBodyTree();

        return;
    }

    public override void ExitState()
    {
        _player.TurnOnGravity();
        return;
    }
}
