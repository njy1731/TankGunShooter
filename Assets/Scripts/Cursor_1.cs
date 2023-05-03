using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_1 : MonoBehaviour
{
    [SerializeField] private Texture2D cursorImage;

    void Start()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }
}
