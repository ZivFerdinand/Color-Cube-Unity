using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Color-Cube/Create Color Pallete", fileName = "Pallete_")]
public class ColorMap : ScriptableObject
{
    public string palleteName = "";
    public List<Color> colors = new List<Color>();
}
