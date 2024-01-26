using Godot;
using System;
using Damaging;
using Statistycs;
using System.Collections.Generic;
using System.Security.Cryptography;
using GUI;
using invSys;
using System.Linq;


namespace Entities{
public partial class Player : Entity
{	


	public List<DroppedItem> itemsNearby;

	//____________________________________________________________________________
	// dash
	//____________________________________________________________________________
	float dashBoost=0;
	float dashCooldown=0;
	float dashMaxCooldown=4;
	float dashMaxBoost=4;

	//____________________________________________________________________________
	// entity elements
	//____________________________________________________________________________
	protected AnimatedSprite2D dashSprite;
	public Camera2D cam;



	//____________________________________________________________________________
	/// <summary> main inventory </summary>
	public invSys.ItemContainer inventory;

	/// <summary> index oof item selected </summary>
	public int selectedItem;

	//public PlayerGUI gui;
	private string giuPath = "";


	public Player(){
		bodyPath = "res://scenes/player/MainCharacter.tscn";
	}

	//____________________________________________________________________________
	//player initialization
	//____________________________________________________________________________
	public override void _Ready()
	{	
		base._Ready();

		LoadBody();

		//gui = new PlayerGUI(this);
		//AddChild(gui);


		Input.MouseMode = Input.MouseModeEnum.Captured;


		inventory = new invSys.ItemContainer(40,"Inventory");
		itemsNearby = new List<DroppedItem>();

	}
	//____________________________________________________________________________



	//____________________________________________________________________________
	//player input
	//____________________________________________________________________________

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if (@event is InputEventMouseMotion eventMouseMotion){
				if(Input.MouseMode == Input.MouseModeEnum.Visible){
				}
			}
		}
	
	//____________________________________________________________________________
	//player input map
	//____________________________________________________________________________
		public override void _UnhandledInput(InputEvent @event)
		{
			base._Input(@event);
			if (@event is InputEventMouseMotion eventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured){


				//GD.Print("you are rotated to: ", (4+rotationState-camRotationState)%4);
				
			}
			else if(@event is InputEventKey inputEventKey){
				if (Input.IsActionJustPressed("mouseCapture")){
				if(Input.MouseMode == Input.MouseModeEnum.Captured)
					Input.MouseMode = Input.MouseModeEnum.Visible;
				else
					Input.MouseMode = Input.MouseModeEnum.Captured;
				}


				if (Input.IsActionJustPressed("pickupItem")){
					PickupItem();
				}

				if (Input.IsActionJustPressed("dash") && (dashCooldown <= 0)){
					dashBoost = dashMaxBoost;
					dashCooldown = dashMaxCooldown;
					dashSprite.Play("dash");
				}
			}

			

			
		}

	
	
	//____________________________________________________________________________
	//TICKING
	//____________________________________________________________________________

	public override void _PhysicsProcess(double delta)
	{
		//if(Input.MouseMode == Input.MouseModeEnum.Visible) return;
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		walkDir = inputDir.Normalized()*basicStats.walkSpeed;
		if(dashBoost > 0){
			walkDir *= (1+(dashBoost));
			dashBoost -= (float)delta*8f; 
		}  
		base._PhysicsProcess(delta);
		PlayAnimation();

		if(dashCooldown > 0) dashCooldown -= (float)delta;

	}

	//____________________________________________________________________________
	//player stats
	//____________________________________________________________________________
	public override void BasicStatsInit(){
		base.BasicStatsInit();
		basicStats.regeneration = 1.0f;
		basicStats.critChance = 0.1f;
		basicStats.critDamage = 1.25f;
	}


	//____________________________________________________________________________
	//entity events
	//____________________________________________________________________________

	public override void OnDamageRecieve(DamageResult damageResult){
		base.OnDamageRecieve(damageResult);
		GD.Print("player got damage: ",damageResult.resultDamage);

	}

	public override void OnDamageDeal(DamageResult damageResult){

	}

	//____________________________________________________________________________
	//player animating
	//____________________________________________________________________________

	public void DefineCameraRotationState(float yr){
		

	}



	

	//____________________________________________________________________________
	//Item picking up
	//____________________________________________________________________________
	private void PickupItem(){
		if(itemsNearby.Count<=0) {GD.Print("Nothing to pick up"); return;}
		if(itemsNearby[0] == null) {GD.Print("Item dissapeared"); return;}
		inventory.addItem(itemsNearby[0].containedItem);
		itemsNearby[0].QueueFree();
		itemsNearby.RemoveAt(0);
		UpdatePickupLabel();
	}

	private void OnItemEntered(Node3D body)
	{
		if(body is DroppedItem di){
			//PickupItem(di);
			itemsNearby.Add(di);
			UpdatePickupLabel();
			GD.Print("Item found");
		}
	}


	private void OnItemExited(Node3D body)
	{
		if(body is DroppedItem di){
			itemsNearby.Remove(di);
			UpdatePickupLabel();
			GD.Print("Item dissapeared");
		}
	}

	public void UpdatePickupLabel(){
		//gui.pickupLabel.Visible = itemsNearby.Count()>0;
	}


	public void SelectItem(int index){
		selectedItem = index;
		GD.Print(inventory.items[selectedItem]?.item.ToString());
	}

    //____________________________________________________________________________
    //Load all the components
    //____________________________________________________________________________
    protected override void OnBodyLoaded()
    {
		base.OnBodyLoaded();
		cam = GetNode<Camera2D>("camera");
		dashSprite = GetNode<AnimatedSprite2D>("dashSprite");
    }

	//____________________________________________________________________________
	//animation
	//____________________________________________________________________________
	public override void PlayAnimation(){
		string animName;
		if(walkDir == Vector2.Zero) animName = "Idle ";
		else animName = "Go ";

		switch(rState){
			case RotationState.Front:
				animName += "Down";
				sprite.FlipH = false;
				break;
			case RotationState.Back:
				animName += "Up";
				sprite.FlipH = false;
				break;
			case RotationState.Right:
				animName += "Side";
				sprite.FlipH = false;
				break;
			case RotationState.Left:
				animName += "Side";
				sprite.FlipH = true;
				break;
			default: break;
		}

		sprite.Play(animName);

	}

}



}












