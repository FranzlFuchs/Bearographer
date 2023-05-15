using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorOffset = new Vector2(_cursorTexture.width / 2, _cursorTexture.height / 2);
        Cursor.SetCursor(_cursorTexture, cursorOffset, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        this.gameObject.transform.position = worldPosition;
    }
}
