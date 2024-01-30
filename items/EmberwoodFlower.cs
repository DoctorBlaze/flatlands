
using Godot;
using Inventory;

namespace Items{


public class EmberwoodFlower : ItemResource{

    public EmberwoodFlower(){
        name = "Emberwood flower";
        icon = ResourceLoader.Load<Texture2D>("res://asset/texture/item/emberwood_flower.png");
        mass = 0f;
        itemID = EItem.EmberwoodFlower;
        itemStringID = ("EmberwoodFlower");
    }


}


}