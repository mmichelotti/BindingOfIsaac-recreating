using UnityEngine;
// i would love so much an interface IDirecitonable but if i do it i must implement the properties in the classes which is stupid to me

public interface IDirectionable
{
    public abstract Vector2 Position { get; set; }
    public abstract Directions DirectionFromCenter { get; set; }
    public void SetDirectionFrom(Vector2 origin) => DirectionFromCenter = origin.GetDirectionTo(Position);
}
