using UnityEngine;
using System;

public static class Extentions 
{
    public static Vector2Int Sign(this Vector2 v) => new(Math.Sign(v.x), Math.Sign(v.y));
}
