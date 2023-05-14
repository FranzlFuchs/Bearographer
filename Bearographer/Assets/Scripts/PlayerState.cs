using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected Player _player;
    
    public PlayerState(Player player)
    {
        _player = player;
    }  

    abstract public void DoStateUpdate();
    abstract public void DoStateFixedUpdate();
    abstract public void EnterState();  
    abstract public void ExitState();   

}
