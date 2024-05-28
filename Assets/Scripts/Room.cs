using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Room : Point
{
    public override Float2 Position
    {
        get
        {
            Float2 sum = Float2.Zero;
            foreach (Tile tile in Tiles)
            {
                sum += (Float2)tile.Position;
            }
            return sum / Tiles.Count;
        }
    }

    public List<Tile> Tiles = new();

    public Color Color { get; set; }
    public int Id { get; private set; }

    private static int _lastId;

    public Room()
    {
        Color = Random.ColorHSV(0f, 1f, .3f, .5f, 1f, 1f);
        Id = ++_lastId;
    }

}
