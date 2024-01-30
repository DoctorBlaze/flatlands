
using Godot;
using Inventory;
using Statistycs;

using System.Collections.Generic;

namespace Items{


public class BambooTanto : Tanto{

	public BambooTanto(){
		name = "Bamboo Tanto";
		description = new string[1];
		description[0] = "light blade with bamboo handle";
		icon = ResourceLoader.Load<Texture2D>("res://asset/texture/item/tanto.png");
		mass = 0f;
		itemID = EItem.BambooTanto;
		itemStringID = ("Bamboo Tanto");

		modifierType = Inventory.ModifierType.Hand;
		modifiers = new List<Modifier>();
		modifiers.Add(new Modifier(10f,StatList.damage, Statistycs.ModifierType.Add));
		modifiers.Add(new Modifier(1.1f,StatList.walkSpeed, Statistycs.ModifierType.Mul));
		modifiers.Add(new Modifier(1.05f,StatList.critDamage, Statistycs.ModifierType.Mul));
	}


}


}
