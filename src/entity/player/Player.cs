using Godot;
using System;
using Damaging;
using Statistycs;
using System.Collections.Generic;
using System.Security.Cryptography;
using GUI;
using Inventory;
using System.Linq;


namespace Entities{
public partial class Player : Entity
{	

	GUI.PlayerGUI gui;

	public List<DroppedItem> itemsNearby;

	//____________________________________________________________________________
	// dash
	//____________________________________________________________________________
	float dashBoost=0;
	float dashCooldown=0;
	float dashMaxCooldown=4;
	float dashMaxBoost=4;

	float attacking = 0f;

	//____________________________________________________________________________
	// entity elements
	//____________________________________________________________________________
	protected AnimatedSprite2D dashSprite;
	protected Sprite2D heldSprite;
	protected AnimatedSprite2D slashSprite;
	public Camera2D cam;


	public Area2D interactArea;

	

	//____________________________________________________________________________
	/// <summary> main inventory </summary>
	public Inventory.ItemContainer inventory;

	/// <summary> index oof item selected </summary>
	public int selectedItem;

	//public PlayerGUI gui;
	private string guiPath = "";


	public Player(){
		bodyPath = "res://scenes/player/MainCharacter.tscn";
		inventory = new Inventory.ItemContainer(50,"Inventory");
	}

	//____________________________________________________________________________
	//player initialization
	//____________________________________________________________________________
	public override void _Ready()
	{	
		base._Ready();

		LoadBody();

		
		itemsNearby = new List<DroppedItem>();

		gui = new PlayerGUI(this);
		AddChild(gui);


		Input.MouseMode = Input.MouseModeEnum.Captured;


		

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
				if (Input.IsActionJustPressed("attack") && (attacking < 0f)){
					//GD.Print("a");
					if(inventory.items[selectedItem] != null) {
						SlashAnimation();
						attacking = 0.25f;
					}
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
		walkDir = inputDir.Normalized()/(1f+attacking*attacking*10f);
		if(dashBoost > 0){
			walkDir *= (1+(dashBoost));
			dashBoost -= (float)delta*8f; 
		}  
		base._PhysicsProcess(delta);
		if(attacking<0f) PlayAnimation();
		else{
			attacking -= (float)delta;
			SwingWeapon();
		}
		LocateInteractArea();

		if(dashCooldown > 0) dashCooldown -= (float)delta;

	}

	//____________________________________________________________________________
	//player stats
	//____________________________________________________________________________
	public override void CalcStats(){
		//base.BasicStatsInit();
		genericStats = basicStats;
		GD.Print("CalcStats()");
		if(inventory == null)  {GD.Print("no inventory."); return;}

		if(inventory.items[selectedItem]?.item == null) {GD.Print("no item selected."); return;}

		if(inventory.items[selectedItem].item is IItemModifier im){
			GD.Print("modifier applied");
			foreach(Modifier m in im.modifiers){
				genericStats.AddModifier(m);
			}
		}
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
		if(inventory.items[selectedItem] == null){
			heldSprite.Visible = false;
			return;
		}
		CalcStats();
		heldSprite.Texture = inventory.items[selectedItem].item.icon;

		//GD.Print(inventory.items[selectedItem]?.item.ToString());
	}

	//____________________________________________________________________________
	//Load all the components
	//____________________________________________________________________________
	protected override void OnBodyLoaded()
	{
		base.OnBodyLoaded();
		cam = body.GetNode<Camera2D>("camera");
		heldSprite = sprite.GetNode<Sprite2D>("held");
		slashSprite = sprite.GetNode<AnimatedSprite2D>("slash");
		dashSprite = body.GetNode<AnimatedSprite2D>("dashSprite");
		interactArea = body.GetNode<Area2D>("InteractArea");
	}

	private void LocateInteractArea(){
		switch(rState){
			case RotationState.Front:
				interactArea.Position = new Vector2(0,20);
				break;
			case RotationState.Back:
				interactArea.Position = new Vector2(0,-60);
				break;
			case RotationState.Right:
				interactArea.Position = new Vector2(50,0);
				break;
			case RotationState.Left:
				interactArea.Position = new Vector2(-50,0);
				break;
			default: break;
		}
	}

	//____________________________________________________________________________
	//animation
	//____________________________________________________________________________
	public override void PlayAnimation(){
		string animName;
		if(walkDir == Vector2.Zero) animName = "Idle ";
		else animName = "Go ";

		heldSprite.Visible = false;

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

	void SlashAnimation(){
		List<Node2D> arr = interactArea.GetOverlappingBodies().ToList();
		for(int i = 0; i < arr.Count(); ++i){
			if(arr[i] is Entity ent && arr[i] != this){
				ent.RecieveDamage(genericStats.damage,DamageType.melee,this,this.Position);
			}
		}

		switch(rState){
			case RotationState.Front:
				inertion.Y += 100;
				slashSprite.ZIndex = 1;
				slashSprite.Play("front");
				break;
			case RotationState.Back:
				inertion.Y -= 100;
				slashSprite.ZIndex = -1;
				slashSprite.Play("back");
				break;
			case RotationState.Right:
				inertion.X += 100;
				slashSprite.ZIndex = 1;
				slashSprite.Play("right");
				break;
			case RotationState.Left:
				inertion.X -= 100;
				slashSprite.ZIndex = 1;
				slashSprite.Play("left");
				break;
			default: break;
		}
	}

	public void SwingWeapon(){
		heldSprite.Visible = true;
	
		if(rState == RotationState.Right){
			heldSprite.FlipH = false;
			heldSprite.FlipV = false;
			if(attacking>0.125f){
				heldSprite.Rotation = -Mathf.Pi*0.5f;
				heldSprite.Position = new Vector2(0,-10);
			}
			else{
				heldSprite.Rotation = Mathf.Pi*0.5f;
				heldSprite.Position = new Vector2(10,10);
			}
			sprite.Play("Attack Side");
		}
		else if(rState == RotationState.Left){
			heldSprite.FlipH = false;
			heldSprite.FlipV = true;
			if(attacking>0.125f){
				heldSprite.Rotation = -Mathf.Pi*0.5f;
				heldSprite.Position = new Vector2(0,-10);
			}
			else{
				heldSprite.Rotation = Mathf.Pi*0.5f;
				heldSprite.Position = new Vector2(-10,10);
			}
			sprite.Play("Attack Side");
		}
		if(rState == RotationState.Front){
			heldSprite.FlipH = false;
			heldSprite.FlipV = true;
			if(attacking>0.2f){
				heldSprite.Rotation = -Mathf.Pi*0.5f;
				heldSprite.ZIndex = -1;
				heldSprite.Position = new Vector2(-2,-10);
			}
			else if(attacking>0.1f){
				heldSprite.Rotation = Mathf.Pi*0.5f;
				heldSprite.ZIndex = 0;
				heldSprite.Position = new Vector2(-8,10);
			}
			else{
				heldSprite.Rotation = 0f;
				heldSprite.ZIndex = 0;
				heldSprite.Position = new Vector2(8,10);
			}
			sprite.Play("Attack Front");
		}
		if(rState == RotationState.Back){
			heldSprite.FlipH = false;
			heldSprite.FlipV = false;
			if(attacking>0.2f){
				heldSprite.Rotation = -Mathf.Pi*0.5f;
				heldSprite.FlipH = false;
				heldSprite.ZIndex = 0;
				heldSprite.Position = new Vector2(2,-10);
			}
			else if(attacking>0.1f){
				heldSprite.Rotation = 0;
				heldSprite.FlipH = false;
				heldSprite.ZIndex = -2;
				heldSprite.Position = new Vector2(8,-2);
			}
			else{
				heldSprite.Rotation = 0f;
				heldSprite.FlipH = true;
				heldSprite.ZIndex = -2;
				heldSprite.Position = new Vector2(-8,-2);
			}
			sprite.Play("Attack Back");
		}
	}

}



}












