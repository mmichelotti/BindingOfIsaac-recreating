using UnityEngine;

public abstract class Point : MonoBehaviour
{
    public virtual Float2 Position { get; set; }
    public Directions DirectionFromCenter { get; set; }
    public void SetDirectionFrom(Float2 origin) => DirectionFromCenter = origin.GetDirectionTo(Position);
}
