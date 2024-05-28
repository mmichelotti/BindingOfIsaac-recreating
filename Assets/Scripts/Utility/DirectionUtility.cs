using System.Collections.Generic;
using UnityEngine;
using System;

[System.Flags]
public enum Directions
{
    Center = 0b0000,
    Up =     0b0001,
    Right =  0b0010,
    Down =   0b0100,
    Left =   0b1000,
}

public static class DirectionUtility
{
    public static IReadOnlyDictionary<Directions, Orientation> OrientationOf { get; } = new Dictionary<Directions, Orientation>()
    {
        { Directions.Up,    Orientation.Up },
        { Directions.Right, Orientation.Right },
        { Directions.Down,  Orientation.Down },
        { Directions.Left,  Orientation.Left }
    };

    public static Float2 DirectionToMatrix(this Directions dir) => (dir.GetCompositeOffset() / 2f) + .5f;
    public static Directions GetOpposite(this Directions dir) => (Directions)((int)dir >> 2 | ((int)dir & 0b0011) << 2);
    public static Quaternion GetRotation(this Directions dir) => OrientationOf[dir];
    public static Float2 GetOffset(this Directions dir) => OrientationOf[dir];
    public static Float2 GetCompositeOffset(this Directions compositeDir)
    {
        Float2 result = Float2.Zero;
        int bitshifter = 0;
        foreach (var (dir, offset) in OrientationOf)
        {
            //bitwise operation returns 1 if compositeDir has it, 0 if it doesnt
            result += ((int)(compositeDir & dir) >> bitshifter) * offset;
            bitshifter++;
        }
        return result;
    }
    /*
    public static Float2 MultipleDirectionsToVectorStandard(Directions dir) =>
                       ((int)(dir & Directions.Up) * Float2.up)
                     + (((int)(dir & Directions.Right) >> 1) * Float2.right)
                     + (((int)(dir & Directions.Down) >> 2) * Float2.down)
                     + (((int)(dir & Directions.Left) >> 3) * Float2.left);
    */

    public static Float2 GetOffset(this Float2 origin, Float2 target) => (target - origin).Sign();
    public static Directions GetDirectionTo(this Float2 origin, Float2 target) => (Directions)
        ( (Convert.ToInt32(target.y - origin.y > 0) * (int)Directions.Up)
        | (Convert.ToInt32(target.y - origin.y < 0) * (int)Directions.Down)
        | (Convert.ToInt32(target.x - origin.x > 0) * (int)Directions.Right)
        | (Convert.ToInt32(target.x - origin.x < 0) * (int)Directions.Left));

    private static Directions Neutralize(this Directions dir)
    {
        int bits = (int)dir;
        int halfmask = (bits >> 2) ^ (bits & 0b0011);
        return (Directions)(bits & (halfmask | halfmask << 2));
    }
}
