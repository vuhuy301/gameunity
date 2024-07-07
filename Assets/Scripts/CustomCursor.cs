using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; 
    public Vector2 hotSpot = Vector2.zero; 
    public CursorMode cursorMode = CursorMode.Auto; 

    void Start()
    {
       
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnDisable()
    {
        
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
