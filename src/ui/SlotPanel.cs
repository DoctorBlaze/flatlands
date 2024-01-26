using Godot;
using System;
using Entities;
using invSys;
using GUI;
using System.Data;
using invSys.IInstance;
using System.Security.Cryptography;

namespace GUI{
public partial class SlotPanel : Control
{
	public invSys.ItemInstance slotItem; //item placed in the slot
	public GUI.PlayerGUI pGUI; //not the slot owner
	public int slotIndex = -1;
	public invSys.ItemContainer contRef = null; //2nd method

	public Button mainButton;

	public bool SlotUpdate(){
		mainButton.GetChild<TextureRect>(0).Texture = null;
		mainButton.GetChild<Label>(1).Text = "";

		if(contRef != null && slotIndex >= 0){
			slotItem = contRef.items[slotIndex];
		}


		if(slotItem == null){
			return false;
		}

		mainButton.GetChild<TextureRect>(0).Texture = ResourceLoader.Load(slotItem.item.icon) as Texture2D;

		//if item is resource, display num
		if (slotItem is invSys.IInstance.IItemCount icount) mainButton.GetChild<Label>(1).Text = icount.num.ToString();

		return true;
	}
	

	public SlotPanel(invSys.ItemContainer cont, int ind){

		
		mainButton = ResourceLoader.Load<PackedScene>("res://ui/slotPanel.tscn").Instantiate<Button>();
		contRef = cont;
		slotIndex = ind;
		AddChild(mainButton);
		SlotUpdate();

	}


    public override void _Input(InputEvent @event)
    {
		//click events
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
           if (mainButton.IsHovered() && mouseButtonEvent.Pressed)
            {
				if(Input.IsActionPressed("attackTakeAll")){
					if(pGUI.dragItem == null){ //take item to slot
						pGUI.dragItem = contRef.items[slotIndex];
						contRef.items[slotIndex] = null;
					}
					else{ // put item to slot (may be stacking)
                        if (contRef.items[slotIndex] is invSys.IInstance.IItemCount contCount 
						&& pGUI.dragItem is invSys.IInstance.IItemCount dragCount 
						&& (contRef.items[slotIndex].item == pGUI.dragItem.item))
                        {
							contCount.num += dragCount.num;
							pGUI.dragItem = null;
                        }
						else{
                        	invSys.ItemInstance tmp = pGUI.dragItem;
							pGUI.dragItem = contRef.items[slotIndex];	
							contRef.items[slotIndex] = tmp;	
						}
					}
					pGUI.UpdDragItem();
					if(pGUI.tmpDragElem != null) pGUI.tmpDragElem.Position = GetGlobalMousePosition();
					SlotUpdate();
				}
				else if(Input.IsActionPressed("useTakeOne")){
					if(contRef.items[slotIndex] != null){//if selected slot NOT empty (try to stack or swap)
						if(contRef.items[slotIndex] is IItemCount contCount){ //from items in contairer will be taken one to drag slot
							
							if(pGUI.dragItem == null){ //item will duplicate in two different stacks
								contCount.num -= 1;
								pGUI.dragItem = new ItemInstanceStackable(contRef.items[slotIndex].item,1);
							}
							else if(contRef.items[slotIndex].item == pGUI.dragItem.item && pGUI.dragItem is IItemCount dragCount){
								contCount.num -= 1;
								dragCount.num += 1;
							}
							if(contCount.num <= 0){contRef.items[slotIndex] = null;}

						}
						//else if(contRef.items[slotIndex] is ItemInstanceGun) {
							//GD.Print("try to select weapon");
							//pGUI.guiOwner.SelectMainhand(contRef.items[slotIndex]);
						//}
						
					}
					//else pGUI.guiOwner.SelectMainhand(null);
					pGUI.UpdDragItem();
					if(pGUI.tmpDragElem != null) pGUI.tmpDragElem.Position = GetGlobalMousePosition();
					SlotUpdate();
				}
				else if(Input.IsActionPressed("selectItem")){
					pGUI.SelectItem(slotIndex);
				}
            }    
        }
		if (@event is InputEventMouseMotion mouseMotion)
        {
			if (mainButton.IsHovered()){
				if(contRef.items[slotIndex] != null){
					if(pGUI.textItemDisplay != null){pGUI.textItemDisplay.QueueFree();pGUI.textItemDisplay = null;}
					TextItemDisplay tid = new(contRef.items[slotIndex],mouseMotion.Position + new Vector2(20,20),this);
					pGUI.AddChild(tid);
					pGUI.textItemDisplay = tid;
				}
				else{
					if(pGUI.textItemDisplay != null) pGUI.textItemDisplay.QueueFree();
					pGUI.textItemDisplay = null;
				}
			}
		}

		if (Input.IsActionPressed("cancelDrop") && mainButton.IsHovered()){
			//pGUI.guiOwner.dropItem(slotIndex);
			SlotUpdate();
		}
		else if (Input.IsActionPressed("rechargeFromSource") && mainButton.IsHovered()){
			//pGUI.guiOwner.SelectAmmoSource(contRef,slotIndex);
		}
    }


}

}
