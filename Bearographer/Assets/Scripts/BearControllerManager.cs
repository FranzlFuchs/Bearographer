using System.Collections;
using System.Collections.Generic;
using SupanthaPaul;
using UnityEngine;

public class BearControllerManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _listBears = new List<GameObject>();

    private List<BearController> _listBearControllers;

    private List<BearController>.Enumerator _activeBearEnumerator;

    void Start()
    {
        _listBearControllers = new List<BearController>();

        foreach (GameObject bear in _listBears)
        {
            BearController bc = bear.GetComponent<BearController>();
            _listBearControllers.Add (bc);
        }
        _activeBearEnumerator = _listBearControllers.GetEnumerator();
        _activeBearEnumerator.MoveNext();
        _activeBearEnumerator.Current.SetActive();
    }

    private void NextBearactive()
    {
        _activeBearEnumerator.Current.SetInActive();

        if (!_activeBearEnumerator.MoveNext())
        {
            _activeBearEnumerator = _listBearControllers.GetEnumerator();
            _activeBearEnumerator.MoveNext();
        }
        _activeBearEnumerator.Current.SetActive();
    }

    private void Update()
    {
        if (InputSystem.ChangeBear())
        {
            NextBearactive();
        }
    }
}
