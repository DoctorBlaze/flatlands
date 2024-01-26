using Godot;
using System;
using System.Collections.Generic;
using GUI;
using invSys;
using Entities;
using System.Net.Http;

namespace GUI
{

public partial class PlayerGUI : Control
{
	public Player guiOwner;

	private Panel overlay;
	private SkillTree skilltree=null;
	private ProgressBar hpBar;
	private Label hpLabel;

	private Button openBookBtn;

	public List<ContainerDisplay> displayedConts;
	public bool contsOpened = false;
	//temportary slot with item on cursor
	public invSys.ItemInstance dragItem;
	public TextItemDisplay textItemDisplay;
	public Control tmpDragElem;

	public Label pickupLabel;

	public PlayerGUI(Player pl){
		guiOwner = pl;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		/// CHANGE THAT SHIT!
		displayedConts = new();
		
		PackedScene overlayRes = ResourceLoader.Load<PackedScene>("res://ui/playerOverlay.tscn");
		
		overlay = overlayRes.Instantiate<Panel>();
		
		guiOwner.AddChild(overlay);
		
		
		hpBar = overlay.GetNode<Panel>("statsPanel").GetNode<ProgressBar>("hpBar");
		openBookBtn = overlay.GetNode<Button>("openBook");
		hpLabel = hpBar.GetNode<Label>("hpLabel");

		pickupLabel = overlay.GetNode<Label>("pickUpLabel");
	}

	public override void _Process(double delta)
	{
		hpBar.Value = guiOwner.health / guiOwner.genericStats.maxHealth; 
		hpLabel.Text =  guiOwner.health + "/" + guiOwner.genericStats.maxHealth; 
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
		else if (@event is InputEventMouseButton){
			if(openBookBtn.ButtonPressed){
				OpenSkillTree();
			}
		}

	}

	public override void _UnhandledInput(InputEvent @event)
	{
			
			/*if (Input.IsActionJustPressed("openArtifactBag")){
				if(!contsOpened) openCloseGUI(guiOwner.inventory,false);
				openCloseGUI(guiOwner.artifactsBag,false);
			}
			if (Input.IsActionJustPressed("openAmmoBag")){
				if(!contsOpened) openCloseGUI(guiOwner.inventory,false);
				openCloseGUI(guiOwner.ammoBag,false);
			}*/
	}


	//____________________________________________________________________________
	//inventory management
	//____________________________________________________________________________
	public void OpenContainer(ItemContainer containerToCheck){
		//check this GUI was already opened
		foreach(ContainerDisplay cd in displayedConts) if(cd.contRef == containerToCheck) return;

		Panel contGUI = ResourceLoader.Load<PackedScene>("res://ui/containerDisplay.tscn").Instantiate<Panel>();
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

		Panel dragItemPanel = ResourceLoader.Load<PackedScene>("res://ui/dragPanel.tscn").Instantiate<Panel>();
		dragItemPanel.Name = "dragItemPanel";

		dragItemPanel.GetNode<TextureRect>("itemIcon").Texture = ResourceLoader.Load<Texture2D>(dragItem.item.icon);
		if(dragItem is invSys.IInstance.IItemCount count){
			dragItemPanel.GetNode<Label>("resNum").Text = "+"+count.num;
		}
		dragItemPanel.GetNode<TextureRect>("itemIcon").Texture = ResourceLoader.Load<Texture2D>(dragItem.item.icon);
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

	public void OpenSkillTree(){
		if(skilltree == null){
			PackedScene STRes = ResourceLoader.Load<PackedScene>("res://ui/interactiveBook/skillTree.tscn");
			if(STRes == null) return;
			skilltree = STRes.Instantiate<SkillTree>();
			guiOwner.AddChild(skilltree);
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else{
			skilltree.QueueFree();
			skilltree = null;
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}

}

}
