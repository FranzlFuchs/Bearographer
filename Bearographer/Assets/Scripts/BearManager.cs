using System.Collections;
using System.Collections.Generic;
using SupanthaPaul;
using UnityEngine;

public class BearManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _listBears = new List<GameObject>();

    private List<Bear> _listBearControllers;

    private List<Bear>.Enumerator _activeBearEnumerator;

    void Start()
    {
        _listBearControllers = new List<Bear>();

        foreach (GameObject bear in _listBears)
        {
            Bear bc = bear.GetComponent<Bear>();
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
