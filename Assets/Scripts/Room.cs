using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room : Point
{
    private static int _lastId;

    #region properties
    public Color Color { get; set; }
    public int Id { get; private set; }
    public List<Tile> Tiles { get; set; } = new();
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
    #endregion

    public Room()
    {
        Color = Random.ColorHSV(0f, 1f, .3f, .5f, 1f, 1f);
        Id = ++_lastId;
    }
}
