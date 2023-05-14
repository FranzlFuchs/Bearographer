using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Standing_PlayerState : PlayerState
{
    public Idle_Standing_PlayerState(Player player) : base(player)
    {
    }
 
    public override void DoStateUpdate()
    {
    }
    public override void DoStateFixedUpdate()
    {
        
    }

    public override void EnterState()
    {
        _player.StopBody();
        _player.SetAnimIdle();
    }

    public override void ExitState()
    {
       return;
    }

}
