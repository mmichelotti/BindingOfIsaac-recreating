using UnityEngine;
using System;

public static class Extentions 
{
    public static Int2 Sign(this Int2 v) => new(Math.Sign(v.x), Math.Sign(v.y));
}
