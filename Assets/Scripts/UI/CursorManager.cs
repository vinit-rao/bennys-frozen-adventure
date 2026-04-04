using UnityEngine;
using UnityEngine.EventSystems; 

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor; 
    
    private Vector2 defaultHotSpot;
    private Vector2 hoverHotSpot;

    void Start()
    {
       
        defaultHotSpot = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        
      
        hoverHotSpot = new Vector2(0, 0); 

        SetDefaultCursor();
    }


    public void SetHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, hoverHotSpot, CursorMode.Auto);
    }


    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, defaultHotSpot, CursorMode.Auto);
    }
}