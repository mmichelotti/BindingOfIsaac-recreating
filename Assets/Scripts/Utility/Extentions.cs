using UnityEngine;
using System;

public static class Extentions 
{
    public static Float2 Sign(this Float2 v) => new(Math.Sign(v.x), Math.Sign(v.y));
}
