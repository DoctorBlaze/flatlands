using Godot;
using System;
using Inventory.IInstance;
using Entities;

namespace Inventory{


/// <summary>
/// This is the instance of the item that is being storaged in ItemContainer.
/// </summary>
public class ItemInstance
{
    public Item item;
    public ItemInstance(){
    }

    public ItemInstance(Item i){
        item = i;
    }
}

public class ItemInstanceArmor : ItemInstance, IItemQuality, IItemReforge
{
    public ItemQuality quality{get;set;}
    public ItemReforge reforge{get;set;}

    public ItemInstanceArmor(Item i){
        item = i;
    }
}

public class ItemInstanceArtifact : ItemInstance, IItemQuality
{
    public ItemQuality quality{get;set;}
    public ItemReforge reforge{get;set;}

    public ItemInstanceArtifact(Item i){
        item = i;
    }
}

public class ItemInstanceStackable : ItemInstance, IItemCount
{
    public int num{get;set;}

    public ItemInstanceStackable(Item i, int count){
        item = i;
        num = count;

    }
}

}