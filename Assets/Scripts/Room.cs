using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Room : IDirectionable<Vector2>
{
    //room kind
    //size (?) from tiles
    [SerializeField]
    [HideInInspector]
    private string name;

    public List<Tile> Tiles = new();

    public Color Color { get; set; }
    public int Id { get; private set; }

    private static int _lastId;

    public Room()
    {
        Color = Random.ColorHSV(0f, 1f, .3f, .5f, 1f, 1f);
        Id = ++_lastId;
        name = Id.ToString();
    }

    #region interface implementation
    public Directions DirectionFromCenter { get; set; }
    public void SetDirectionFrom(Vector2 origin) => DirectionFromCenter = origin.GetDirectionTo(Position);

    public Vector2 Position
    {
        get
        {
            Vector2 sum = Vector2.zero;
            foreach (Tile tile in Tiles)
            {
                sum += (Vector2)tile.Position;
            }
            return sum / Tiles.Count;
        }
    }
    #endregion


}
