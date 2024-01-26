using Godot;
using System;


namespace invSys{
namespace IInstance{

public interface IItemCount{
    public int num{get;set;}
}

public interface IItemQuality{
    public ItemQuality quality{get;set;}
}

public interface IItemReforge{
    public ItemReforge reforge{get;set;}
}

}
}