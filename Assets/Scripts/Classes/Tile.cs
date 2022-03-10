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
    Default,
    First,
    Second,
    Third,
    Forth,
    Fifth,
    Sixth,

};

public class Tile
{
    public static TileData[][] fill;
}