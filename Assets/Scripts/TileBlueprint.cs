using UnityEngine;
using System;

[Serializable]
public struct TileBlueprint : IEquatable<TileBlueprint>, IFormattable
{
    #region variables
    public Vector2 Scale;
    [Range(0, 1)] public float EdgeThickness;

    public readonly Vector2 CenterOffset;
    public readonly Vector2 Spacing;
    public readonly float EdgeOffset;
    #endregion
    public TileBlueprint(Vector2 scale, float edgeThickness)
    {
        edgeThickness *= Mathf.Min(scale.x, scale.y) / 10; ;

        (Scale, CenterOffset) = (scale, scale / 2);
        (EdgeThickness, EdgeOffset) = (edgeThickness, edgeThickness / 2);
        Spacing = CenterOffset - new Vector2(EdgeOffset, EdgeOffset);
    }

    #region interfaces
    public readonly bool Equals(TileBlueprint other) => (Scale, EdgeThickness) == (other.Scale, other.EdgeThickness);
    public override readonly bool Equals(object obj) => obj is TileBlueprint other && Equals(other);
    public override readonly int GetHashCode() => HashCode.Combine(Scale, EdgeThickness);
    public string ToString(string format, IFormatProvider formatProvider) => $"X {Scale.x}, Y {Scale.y} \nThickness {EdgeThickness}";
    #endregion
}
