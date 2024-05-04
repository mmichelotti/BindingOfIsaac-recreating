using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    [SerializeField]
    [HideInInspector]
    private string name;

    public List<Tile> Tiles = new();

    // room kind
    // size (?) from tiles
    public readonly Color Color;

    public int Id;

    private static int _lastId;

    public Room()
    {
        Color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        Id = ++_lastId;
        name = Id.ToString();
    }
}
