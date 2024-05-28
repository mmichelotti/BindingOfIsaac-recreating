using UnityEngine;

public interface ITestable
{
    
}
public interface IDirectionable<T> where T : ITestable
{
    public abstract T Position { get; }
    public abstract Directions DirectionFromCenter { get; set; }
    public abstract void SetDirectionFrom(T origin);
}