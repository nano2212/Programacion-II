using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;
    Vector2 cursorhotspot;
    // Start is called before the first frame update
    void Start()
    {
        cursorhotspot = new Vector2(cursorImage.width / 2, cursorImage.height / 2);
        Cursor.SetCursor(cursorImage, cursorhotspot, CursorMode.Auto);
    }

   
}
