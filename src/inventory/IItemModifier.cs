using System.Collections.Generic;
using Godot;
using Statistycs;

namespace Inventory{

public interface IItemModifier{

    /// <summary>
    /// basic item stats
    /// </summary>
    public List<Modifier> modifiers {get; set;}


    /// <summary>
    /// where we have to wear this item to get stats boost
    /// </summary>
    public ModifierType modifierType{get; set;}        

}


public enum ModifierType{
    Any,
    Hand,
    Helmet,
    Chest,
    Boots,
    Artifact
}


}