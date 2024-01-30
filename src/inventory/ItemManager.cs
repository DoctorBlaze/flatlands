using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Godot;

namespace Inventory{

public sealed class ItemManager{
	private static readonly object lockObject = new object();
	private static ItemManager instance;

	/// <summary>
	/// Get the singletone Items object
	/// </summary>
	public static ItemManager init{
		get
		{
			if (instance == null)
			{
				lock (lockObject)
				{
					if (instance == null)
					{
						instance = new ItemManager();
					}
				}
			}
			return instance;
		}
	}

	public Dictionary<EItem,Item> ditems {get;}

	private ItemManager(){
		ditems = new();
		RegisterAll();
	}

	public void RegisterItem(Item i){
		if(i.itemID == EItem._noID){
			GD.PrintErr(i.ToString(), " register error: Items with [itemID = EItem._noID] are not added in Items.items");    
			return;
		}
		if(ditems.Keys.Contains(i.itemID)){
			GD.PrintErr(i.ToString(), " register error: This ID has been already used");    
			return;
		}
		
		ditems.Add(i.itemID,i);
		GD.Print(i.ToString(), " registered succesfully.");   
	}

	public Item Get(EItem ind){
		return ditems[ind];
	}


	public ItemInstance GetInstance(EItem item, int num=1){
		ItemInstance ret;

		if(ditems[item] is ItemResource){
			return new ItemInstanceStackable(ditems[item],num);
		}
		return new ItemInstance(ditems[item]);
	}

	private void RegisterAll(){
		RegisterItem(new Items.EmberwoodFlower());
		RegisterItem(new Items.BambooTanto());
	}

}

/// <summary>
/// Enum of all registered items in the game
/// </summary>
public enum EItem{

	/// <summary>
	/// items with this ID will not be added in Items singletone list
	/// </summary>
	_noID,
	EmberwoodFlower,
	BambooTanto,

}

}
