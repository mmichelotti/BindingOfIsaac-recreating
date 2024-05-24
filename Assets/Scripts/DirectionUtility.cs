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
//ho capito cosa sono le record struct ma non si possono usare
//quindi credo di avere capito cosa sono i record
//chiedere ad ale come effettivamente potrebbero essere implementati dei record al posto di classi
public static class DirectionUtility
{
    public static IReadOnlyDictionary<Directions, Orientation> OrientationOf { get; } = new Dictionary<Directions, Orientation>()
    {
        { Directions.Up,    Orientation.Up },
        { Directions.Right, Orientation.Right },
        { Directions.Down,  Orientation.Down },
        { Directions.Left,  Orientation.Left }
    };

    //with new Orientation structor there is no need to access a method but just to cast the struct as a quaternion
    public static Vector2 DirectionToMatrix(this Directions dir) => ((Vector2)dir.GetCompositeOffset() / 2f) + new Vector2(.5f,.5f);

    //supponendo che enum directions non venga esteso (cosa che non dovrebbe succedere dato che ora che � un flag copre tutte le possibili direzioni)
    public static Directions GetOpposite(this Directions dir) => (Directions)((int)dir >> 2 | ((int)dir & 0b0011) << 2);

    //implict cast of Orientation
    public static Quaternion GetRotation(this Directions dir) => OrientationOf[dir];
    public static Vector2Int GetOffset(this Directions dir) => OrientationOf[dir];


    public static Vector2Int GetCompositeOffset(this Directions compositeDir)
    {
        Vector2Int result = Vector2Int.zero;
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
    public static Vector2Int MultipleDirectionsToVectorStandard(Directions dir) =>
                       ((int)(dir & Directions.Up) * Vector2Int.up)
                     + (((int)(dir & Directions.Right) >> 1) * Vector2Int.right)
                     + (((int)(dir & Directions.Down) >> 2) * Vector2Int.down)
                     + (((int)(dir & Directions.Left) >> 3) * Vector2Int.left);
    */

    public static Vector2Int GetOffset(Vector2 origin, Vector2 target) => (origin - target).Sign();

    public static Directions GetDirection(Vector2 origin, Vector2 target) => (Directions)
        ( (Convert.ToInt32(origin.y - target.y > 0) * (int)Directions.Up)
        | (Convert.ToInt32(origin.y - target.y < 0) * (int)Directions.Down)
        | (Convert.ToInt32(origin.x - target.x > 0) * (int)Directions.Right)
        | (Convert.ToInt32(origin.x - target.x < 0) * (int)Directions.Left));


    private static Directions Neutralize(this Directions d)
    {
        int bits = (int)d;
        int halfmask = (bits >> 2) ^ (bits & 0b0011);
        return (Directions)(bits & (halfmask | halfmask << 2));
    }

}
