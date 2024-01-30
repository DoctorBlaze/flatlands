using Godot;
using System;
using Inventory;
using System.Collections.Generic;

namespace Inventory{

/// <summary>
/// item container is a list of items with auto-add function. Containers are used for backpacks and chests.
/// </summary>
public partial class ItemContainer : Node
{
	public ItemContainer(int slotsNum, string name){
		items = new ItemInstance[slotsNum];
		contName = name;
	}

	public ItemInstance[] items;
	public string contName = "";

	//add item to inventory [ if item added, return slot index, else -1 ]
	public int addItem(ItemInstance someItem){
		//resource can be stacked (try to stack it!)
		if(someItem is IInstance.IItemCount someItemC){
			for(int i = 0; i < items.Length; ++i){
				if(items[i] is IInstance.IItemCount slotItemC) //stack same type
				{
					if(someItem.item == items[i].item){
						slotItemC.num += someItemC.num;
						someItem = null;
					}
				}
			}
		}

		//if resource cannot be stacked
		for(int i = 0; i < items.Length; ++i){
				if(items[i] == null){
					items[i] = someItem;
					return i;
				}
		}
		
		return -1;
	}

	public bool PlayerCollectItem(DroppedItem droppedItem){
		if (droppedItem.pickupCooldown > 0.1f) return false;
		int tmp = addItem(droppedItem.containedItem);
		droppedItem.pickupCooldown = 1.0f;
		if(tmp >= 0){
			droppedItem.QueueFree();
			return true;
		}
		
		return false;
	}

	//find first item that matches cryteria (return -1 if not found)
	public int findItem(int start, itemFilter filter){

		for(int i = 0; i < items.Length; ++i){
			if(filter == itemFilter.noFilter && items[i] != null){
				return i;
			}
		}
		return -1;
	}


	//ublic int findItem()

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void ConsumeItemAt(int ind){
		if (ind < 0 || ind >= items.Length) {GD.PrintErr("ConsumeItemAt(ind): ind out of range items[]"); return;}
		if(items[ind] is ItemInstanceStackable stackable){
			stackable.num--;
			if(stackable.num <= 0) items[ind] = null;
		}
		else{
			items[ind] = null;
		}
	}

	//____________________________________________________________________________
	// item search
	//____________________________________________________________________________
	public ItemInstance FindByName(string name){
		return null;
	}
}



}
