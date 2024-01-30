
using Godot;


namespace World{
namespace TM{

/// <summary>
/// exists as a single object
/// generated tiles just have an ID
/// </summary>
public class Tile{
    
    public string name = "tile";

    /// <summary>
    /// set in constructor
    /// </summary>
    public string textID = "";

    /// <summary>
    /// changes dynamically, depending on tilemap tile ids
    /// </summary>
    public int id = 0;
    public Layer layer = Layer.BasicTile;

    public float hardness = 0;
    public byte lvl = 0;
}

/// <summary>
/// Layer where this tile generates
/// </summary>
public enum Layer{

    /// <summary>
    /// like rock or water
    /// </summary>
    BasicTile,

    /// <summary>
    /// Grass, Path, etc.
    /// </summary>
    Vegetation,

    /// <summary>
    /// is covered in snow or water
    /// </summary>
    CoverLayer,

    /// <summary>
    /// grass, small rocks, trees AND buildings (can be changed by player)
    /// </summary>
    Toplayer
}



}
}