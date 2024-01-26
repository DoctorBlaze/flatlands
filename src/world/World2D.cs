

using System;
using Entities;
using Godot;

namespace World{

public partial class World2D : Node2D{
	//____________________________________________________________________________
	//World config
	//____________________________________________________________________________
	public byte chunklen;
	

	//____________________________________________________________________________
	//
	//____________________________________________________________________________

	public World2D(){
		Random rnd = new Random();
		Player player = new Player();
		player.Position = new Vector2(500,600);
		Mob mob;

		for(byte i = 0; i < 16; ++i){
			mob = new();
			//mob.SetTarget(player);
			mob.Position = new Vector2(rnd.Next()%1000-500,rnd.Next()%1000-500);
			AddChild(mob);
		}
		
		//mob.SetTarget(player);
		
		AddChild(player);
	}
}

public enum Difficulty{
	Easy,
	Normal,
	Hard,
	Dtwo
}


}
