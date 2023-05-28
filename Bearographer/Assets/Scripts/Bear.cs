using System.Collections;
using System.Collections.Generic;
using PlayerControllerInputSysten;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bear : Player
{
    [SerializeField]
    private GameObject _bearMarker;

    private IState _prevState;

    public override float GetHorizontalAxis()
    {
        return InputSystem.HorizontalRawAD();
    }

    public override void SetInitialState()
    {
        _bearMarker.SetActive(false);
        _currentState = new StandBy_PlayerState(this);
        _prevState = new Idle_PlayerState(this);
    }

    public override bool GetJump()
    {
        return InputSystem.JumpW();
    }

    public void SetActive()
    {
        _bearMarker.SetActive(true);
        ChangeState (_prevState);
    }

    public void SetInActive()
    {
        _bearMarker.SetActive(false);
        _prevState = _currentState;
        ChangeState(new StandBy_PlayerState(this));
    }

    public override void TriggerEnter(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("TreeTrunk"))
        {
            OnTreeTrunk = true;
            //Tilemap map = coll.GetComponentInParent<Tilemap>();
            //Vector3Int pos = map.WorldToCell( coll.composite.contacts().point  transform.position);
            //_treePos = new Vector2(pos.x, pos.y);
        }
    }

    public override void TriggerExit(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("TreeTrunk"))
        {
            OnTreeTrunk = false;
        }
    }
}
