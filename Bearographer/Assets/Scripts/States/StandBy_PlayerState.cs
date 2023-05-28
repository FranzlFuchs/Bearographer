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
        return;
    }

    public override void DoStateFixedUpdate()
    {
        return;
    }

    public override void EnterState()
    {
        return;
    }

    public override void ExitState()
    {
        return;
    }
}
