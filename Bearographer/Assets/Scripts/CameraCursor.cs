using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture;

    [SerializeField]
    private GameObject _bearPrefab;

    private float _cursorTexWidth;

    private float _cursorTexHeight;

    private BoxCollider2D _collider;

    private List<GameObject> _bearsInRange = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _cursorTexWidth = _cursorTexture.width;
        _cursorTexHeight = _cursorTexture.height;

        //Textur setzen
        Vector2 cursorOffset = new Vector2(_cursorTexWidth / 2, _cursorTexHeight / 2);
        Cursor.SetCursor(_cursorTexture, cursorOffset, CursorMode.ForceSoftware);

        //Collider an Texturgröße anpassen
        _collider = GetComponent<BoxCollider2D>();

        Vector2 screenPositionOR = new Vector2(
            Input.mousePosition.x + _cursorTexWidth / 2,
            Input.mousePosition.y + _cursorTexHeight / 2
        );
        Vector2 screenPositionUL = new Vector2(
            Input.mousePosition.x - _cursorTexWidth / 2,
            Input.mousePosition.y - _cursorTexHeight / 2
        );

        Vector2 worldPositionOR = Camera.main.ScreenToWorldPoint(screenPositionOR);
        Vector2 worldPositionUL = Camera.main.ScreenToWorldPoint(screenPositionUL);
        Vector2 S = new Vector2(
            worldPositionOR.x - worldPositionUL.x,
            worldPositionOR.y - worldPositionUL.y
        );
        _collider.size = S;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject Position an Maus ausrichten
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        this.gameObject.transform.position = worldPosition;

        //Auslöser
        if (Input.GetMouseButtonDown(0))
        {
            TakeShot();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bear"))
        {
            Debug.Log("OUT");
            _bearsInRange.Remove(coll.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bear"))
        {
            Debug.Log("IN");
            _bearsInRange.Add(coll.gameObject);
        }
    }

    void TakeShot()
    {
        int index = _bearsInRange.Count;
        for (int i = 0; i < index; i++)
        {
            //_bearsInRange[0].SetActive(false);
            Destroy(_bearsInRange[0]);
        }
    }
}
