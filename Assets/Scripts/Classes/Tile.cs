using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileData
{
    None,
    Color,
    Obstacle,
};

public enum TileColor
{
    None,
    Red,
    Blue,
    Green,
};

public class Tile
{
    public static TileData[][] fill;
}