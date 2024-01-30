using Godot;
using System;
using System.Collections.Generic;
using GUI;
using Inventory;
using Entities;
using System.Net.Http;

namespace GUI
{

public partial class PlayerGUI : CanvasLayer
{
	private const string overlayPath = "res://UI/scenes/overlay.tscn";
	private const string inventoryPath = "res://UI/scenes/containerDisplay.tscn";
	private const string dragPath = "res://UI/scenes/dragPanel.tscn";


	public Player guiOwner;

	private Control overlay;

	private ProgressBar hpBar;
	private Label hungerLabel;


	public List<ContainerDisplay> displayedConts;
	public bool contsOpened = false;
	//temportary slot with item on cursor
	public Inventory.ItemInstance dragItem;
	public TextItemDisplay textItemDisplay;
	public Control tmpDragElem;


	public PlayerGUI(Player pl){
		guiOwner = pl;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		/// CHANGE THAT SHIT!
		displayedConts = new();
		
		PackedScene overlayRes = ResourceLoader.Load<PackedScene>(overlayPath);
		
		overlay = overlayRes.Instantiate<Control>();
		
		AddChild(overlay);
		
		
		hpBar = overlay.GetNode<ProgressBar>("hp");
		hungerLabel = overlay.GetNode<Label>("hunger");
	}

	public override void _Process(double delta)
	{
		hpBar.MaxValue = guiOwner.genericStats.maxHealth;
		hpBar.Value = guiOwner.health;

		hungerLabel.Text = "none"; 
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseMotion eventMouseMotion){
			if(Input.MouseMode == Input.MouseModeEnum.Visible){
				if(tmpDragElem != null){
					//tmpDragElem.Visible = true;
					tmpDragElem.Position = eventMouseMotion.Position;
				}
			}
		}
		else if (Input.IsActionJustPressed("openInventory")){
			openCloseGUI(guiOwner.inventory,true);
		}
	}


	//____________________________________________________________________________
	//inventory management
	//____________________________________________________________________________
	public void OpenContainer(ItemContainer containerToCheck){
		//check this GUI was already opened
		foreach(ContainerDisplay cd in displayedConts) if(cd.contRef == containerToCheck) return;

		Panel contGUI = ResourceLoader.Load<PackedScene>(inventoryPath).Instantiate<Panel>();
		GUI.ContainerDisplay containerDisplay = new(contGUI)
			{
				contRef = containerToCheck,
				guiOwner = this
			};
		containerDisplay.GenGrid(); 
		displayedConts.Add(containerDisplay);
		AddChild(containerDisplay);
	}

	public void UpdDragItem(){
		GetNodeOrNull("dragItemPanel")?.QueueFree();
		tmpDragElem?.QueueFree();
		tmpDragElem = null;
		
		if(dragItem == null) return;

		Panel dragItemPanel = ResourceLoader.Load<PackedScene>(dragPath).Instantiate<Panel>();
		dragItemPanel.Name = "dragItemPanel";

		dragItemPanel.GetNode<TextureRect>("itemIcon").Texture = dragItem.item.icon;
		if(dragItem is Inventory.IInstance.IItemCount count){
			dragItemPanel.GetNode<Label>("resNum").Text = "+"+count.num;
		}
		dragItemPanel.GetNode<TextureRect>("itemIcon").Texture = dragItem.item.icon;
		tmpDragElem = dragItemPanel;
		AddChild(tmpDragElem);
		
	}

	/// <summary>
	/// open player GUI using a keybind
	/// </summary>
	/// <param name="cont">what to open</param>
	/// <param name="applyForAll">if gui being closed by this key, all other ContainerDisplays will be also closed</param>
	private void openCloseGUI(ItemContainer cont, bool applyForAll){
		textItemDisplay?.QueueFree();
		textItemDisplay = null;
		if(contsOpened){
			int thisIndex = -1;
			if(tmpDragElem != null) tmpDragElem.Visible = false;
			for(int i = 0; i < displayedConts.Count; ++i){
					if(displayedConts[i].contRef == cont || applyForAll)
					{thisIndex = i; displayedConts[i]?.QueueFree();
					displayedConts[i] = null;}
			}
			if(applyForAll) displayedConts.Clear();
			else if (thisIndex >= 0) displayedConts.RemoveAt(thisIndex);

			if(displayedConts.Count == 0){
				GD.Print("mosue captured");
				contsOpened = false;
				Input.MouseMode = Input.MouseModeEnum.Captured;
				return;
			}
			
		}
		if(tmpDragElem != null) tmpDragElem.Visible = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
		contsOpened = true;
		OpenContainer(cont);
	}

	
	public void SelectItem(int index){
		guiOwner.SelectItem(index);
	}



}

}
