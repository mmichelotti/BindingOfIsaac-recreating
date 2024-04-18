using UnityEngine;

[System.Serializable]
public struct RoomBlueprint
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

    public RoomBlueprint(Vector2 scale, float edgeThickness)
    {
        Scale = scale;
        CenterOffset = scale / 2;
        float min = Mathf.Min(scale.x, scale.y) / 10;
        edgeThickness *= min;
        EdgeThickness = edgeThickness;
        EdgeOffset = edgeThickness / 2;
        Spacing = new(CenterOffset.x - EdgeOffset, CenterOffset.y - EdgeOffset);
    }
}
