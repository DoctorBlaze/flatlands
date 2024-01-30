using System.Collections.Generic;
using Statistycs;


namespace Inventory{


public partial class ItemWeapon : Item, IItemModifier
{

    public List<Modifier> modifiers {get; set;}
    public ModifierType modifierType{get; set;}  
}


}