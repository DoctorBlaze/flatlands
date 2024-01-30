

using System;
using Entities;
using Godot;
using Inventory;

namespace World{

public partial class World2D : Node2D{
	//____________________________________________________________________________
	//World config
	//____________________________________________________________________________
	public byte chunklen;
	

	public EntityPool epool;


	public ItemManager imanager;

	public World2D(){
		imanager = ItemManager.init;
		epool = new EntityPool(this);
		Random rnd = new Random();


		Player player = new Player();
		player.Position = new Vector2(100,60);
		Slime mob;

		for(byte i = 0; i < 16; ++i){
			mob = new();
			mob.Position = new Vector2(rnd.Next()%2000-1000,rnd.Next()%2000-1000);
			epool.SpawnEntity(mob);
		}
		player.inventory.addItem(imanager.GetInstance(EItem.BambooTanto,1));
		//GD.Print(imanager.GetInstance(EItem.EmberwoodFlower));
		player.inventory.addItem(imanager.GetInstance(EItem.EmberwoodFlower,4));
		
		
		epool.SpawnPlayer(player);
	}
}

public enum Difficulty{
	Easy,
	Normal,
	Hard,
	Dtwo
}


}
