using UnityEngine;

[System.Serializable]
public struct TileBlueprint : System.IEquatable<TileBlueprint>
{
    public Vector2 Scale;

    [HideInInspector]
    public Vector2 CenterOffset;

    [Range(0, 1)]
    public float EdgeThickness;

    [HideInInspector]
    public float EdgeOffset;

    [HideInInspector]
    public Vector2 Spacing;

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
    public readonly bool Equals(TileBlueprint other)
    {
        return Scale == other.Scale && EdgeThickness == other.EdgeThickness;
    }

    public override readonly bool Equals(object obj)
    {
        return obj is TileBlueprint other && Equals(other);
    }

    public override readonly int GetHashCode()
    {
        return System.HashCode.Combine(Scale, EdgeThickness);
    }
}
