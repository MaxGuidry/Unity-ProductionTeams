using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_WEBGL
public class MouseCursor : MonoBehaviour
{

    public Texture2D curserTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(curserTexture, hotspot, cursorMode);
    }    
}
#endif