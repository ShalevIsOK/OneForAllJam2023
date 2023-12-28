using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    SpriteRenderer Sprite;
    Bounds bounds;
    public static float LeftEdge { get; private set; }
    public static float RightEdge { get; private set; }
    public static float TopEdge { get; private set; }
    public static float BottomEdge { get; private set; }
    private void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        bounds = Sprite.bounds;
        LeftEdge = bounds.min.x;
        RightEdge = bounds.max.x;
        TopEdge = bounds.max.y;
        BottomEdge = bounds.min.y;
    }
    
    
}
