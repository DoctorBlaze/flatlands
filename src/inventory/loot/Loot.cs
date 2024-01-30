using Godot;

namespace Inventory{

/// <summary>
/// one loot slot; 
/// contains only one item;
/// weight is being used only if placed in weightTable.
/// </summary>
public struct Loot{
    public uint weight;
    public float chance;

    
    public uint numMin;
    public uint numMax;

    public Item item;
    
}



}