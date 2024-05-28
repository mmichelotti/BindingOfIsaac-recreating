using UnityEngine;
using System;
public static class ExtentionUtility 
{
    public static Vector2 Sign(this Vector2 v) => new(Math.Sign(v.x), Math.Sign(v.y));
}
