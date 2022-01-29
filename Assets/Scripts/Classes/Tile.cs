using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileData
{
    Empty,
    Color,
    Obstacle,
};

public class Tile
{
    public static TileData[][] fill;
}