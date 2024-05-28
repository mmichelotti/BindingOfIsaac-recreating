using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room : IDirectionable
{
    private static int _lastId;

    #region properties
    public Color Color { get; set; }
    public int Id { get; private set; }
    public List<Tile> Tiles { get; set; } = new();

    public Vector2 Position
    {
        get
        {
            Vector2 sum = Vector2.zero;
            foreach (Tile tile in Tiles)
            {
                sum += tile.Position;
            }
            return sum / Tiles.Count;
        }
        set
        {

        }
    }

    public Directions DirectionFromCenter { get; set; }
    #endregion

    public Room()
    {
        Color = Random.ColorHSV(.3f, .5f, 1f, 1f, .3f, .7f);
        Id = ++_lastId;
    }
}
