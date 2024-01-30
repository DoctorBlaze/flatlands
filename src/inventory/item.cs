using Godot;
using System;


namespace Inventory{



/// <summary>
/// Item is just a bunch of variables like name and description keys, texture/model paths, mass and rarity. 
/// It's a parent class to weapons/tools/resources.
/// 
/// Item object (for specifig ingame item) will be created only ONCE! It contains important, ingame-unchangable values (like item name or texture)
/// Instances of item that are being storaged in player's inventory is ItemInstance object! Look at ItemInstance.cs for more info.
/// </summary>
public class Item
{
    public ItemInstanceType itemType = ItemInstanceType.general;

    //item name 
    public string name;
    public string[] description;

    //item visuals
    //public string icon;
    public Texture2D icon;
    /// <summary>
    /// scene with item
    /// </summary>
    public string scene;

    //other attributes
    public float mass;
    //public short rarity;

    //if true, player cannot hold offhand item

    //public long UID;
    public EItem itemID = EItem._noID;
    //optional ID for intems not in list
    public string itemStringID=null;
}

public enum itemFilter{
	noFilter,
	byClass,
	byRarity,
	byName,
	byMass,
	searchBullets
}

public enum ItemInstanceType{
    general,
    tool,
    storage,
    armor,
    accesory,
    resource
}

public enum ItemQuality{
    normal,
    fine,
    cool,
    awesome,
    mythic
}

public enum ItemReforge{
    none
}

}
