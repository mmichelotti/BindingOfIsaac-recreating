using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Room
{
    //room kind
    //size (?) from tiles
    [SerializeField]
    [HideInInspector]
    private string name;

    public List<Tile> Tiles = new();

    public readonly Color Color;
    public int Id { get; private set; }

    private static int _lastId;

    public Vector2 Pivot
    {
        get
        {
            Vector2 sum = Vector2.zero;
            foreach (Tile tile in Tiles)
            {
                sum += (Vector2)tile.Coordinates;
            }
            return sum / Tiles.Count;
        }
    }

    public Room()
    {
        Color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        Id = ++_lastId;
        name = Id.ToString();
    }
}
