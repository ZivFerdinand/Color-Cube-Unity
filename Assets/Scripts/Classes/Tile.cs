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
    Blue,
    Green,
    Orange,
    Red,
    White,
    Yellow,

};

public class Tile
{
    public static TileData[][] fill;
}