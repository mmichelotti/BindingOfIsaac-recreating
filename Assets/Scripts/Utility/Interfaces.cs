using UnityEngine;

public interface IDirectionable<T>
{
    public abstract T Position { get; }
    public abstract Directions DirectionFromCenter { get; set; }
    public abstract void SetDirectionFrom(T origin);
}