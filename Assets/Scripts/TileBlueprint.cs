using UnityEngine;
using System;

[Serializable]
public struct TileBlueprint : IEquatable<TileBlueprint>
{
    public Vector2 Scale;
    [Range(0, 1)]
    public float EdgeThickness;

    public readonly Vector2 CenterOffset;
    public readonly float EdgeOffset;
    public readonly Vector2 Spacing;

    public TileBlueprint(Vector2 scale, float edgeThickness)
    {
        Scale = scale;
        CenterOffset = scale / 2;
        float min = Mathf.Min(scale.x, scale.y) / 10;
        edgeThickness *= min;
        EdgeThickness = edgeThickness;
        EdgeOffset = edgeThickness / 2;
        Spacing = new(CenterOffset.x - EdgeOffset, CenterOffset.y - EdgeOffset);
    }

    // Implementazione del confronto di uguaglianza
    public readonly bool Equals(TileBlueprint other) =>
        Scale == other.Scale && EdgeThickness == other.EdgeThickness;
    

    public override readonly bool Equals(object obj) => 
        obj is TileBlueprint other && Equals(other);

    public override readonly int GetHashCode() => 
        HashCode.Combine(Scale, EdgeThickness);
}
