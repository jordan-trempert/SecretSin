using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(0,0), CursorMode.Auto);
    }


}
