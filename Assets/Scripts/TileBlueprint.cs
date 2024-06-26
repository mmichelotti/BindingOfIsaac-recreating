using UnityEngine;
using System;

[Serializable]
public struct TileBlueprint : IEquatable<TileBlueprint>, IFormattable
{
    public Vector2 Scale;
    [Range(0, 1)]
    public float EdgeThickness;

    public readonly Vector2 CenterOffset;
    public readonly float EdgeOffset;
    public readonly Vector2 Spacing;

    public TileBlueprint(Vector2 scale, float edgeThickness)
    {
        (Scale, CenterOffset) = (scale, scale / 2);
        edgeThickness *= Mathf.Min(scale.x, scale.y) / 10; ;
        (EdgeThickness, EdgeOffset) = (edgeThickness, edgeThickness / 2);
        Spacing = CenterOffset - new Vector2(EdgeOffset, EdgeOffset);
    }

    // Implementazione del confronto di uguaglianza
    public readonly bool Equals(TileBlueprint other) =>
        Scale == other.Scale && EdgeThickness == other.EdgeThickness;
    

    public override readonly bool Equals(object obj) => 
        obj is TileBlueprint other && Equals(other);

    public override readonly int GetHashCode() => 
        HashCode.Combine(Scale, EdgeThickness);

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return $"X {Scale.x}, Y {Scale.y} \nThickness {EdgeThickness}";
    }
}
