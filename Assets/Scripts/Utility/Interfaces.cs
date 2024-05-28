using UnityEngine;
// i would love so much an interface IDirecitonable but if i do it i must implement the properties in the classes which is stupid to me

public abstract class Point : MonoBehaviour
{
    public virtual Float2 Position { get; set; }
    public Directions DirectionFromCenter { get; set; }
    public void SetDirectionFrom(Float2 origin) => DirectionFromCenter = origin.GetDirectionTo(Position);
}
