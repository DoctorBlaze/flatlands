using System;
using System.Collections.Generic;
using Godot;


namespace Entities{

public partial class Mob : Entity{
	//____________________________________________________________________________
	//TARGETING ENTITY
	//____________________________________________________________________________
	protected Node2D target;
	protected List<Type> targetTypes;

	public void SetTarget(Entity t){
		target = t;
	}


	//____________________________________________________________________________
	//SEARCHING TARGETS
	//____________________________________________________________________________

	protected Area2D searchArea;
	protected float searchRadius=100f;
	public void SetupSearchArea(){
		CollisionShape2D searchShape;
		searchShape = new CollisionShape2D();
		searchShape.Shape = new CircleShape2D();
		searchShape.Scale *= searchRadius;

		searchArea = new Area2D();
		searchArea.AddChild(searchShape);
		searchArea.ProcessPriority = 100;

	}

	//____________________________________________________________________________
	//REACHING GOALS
	//____________________________________________________________________________
	/// <summary>
	/// where to go
	/// </summary>
	protected Vector2 goal = Vector2.Zero; 
	protected Vector2 currentDir = Vector2.Zero; 
	protected float goalRadius=50f;
	public void SetNewGoal(Vector2 g){        
		if((g-Position).Length() < goalRadius) return;
		goal = g;
		walkState = MobWalkState.GoTowards;
		currentDir = (goal-Position).Normalized();
	}


	Vector2 randomDir;


	public Mob(){
		rnd = new Random();
		bodyPath = "res://entity/mob/BlueSlime.tscn";
		walkState = MobWalkState.GoTowards;
		goingType = MobGoingType.Towards;
	}
	public override void _Ready()
	{	
		base._Ready();

		targetTypes = new List<Type>();
		targetTypes.Add(typeof(Player));

		SetupSearchArea();

		LoadBody();
	}



	//---------------------------------------
	MobWalkState walkState  = MobWalkState.None;
	MobGoingType goingType = MobGoingType.Towards;
	MobAction action = MobAction.Idle;

	//____________________________________________________________________________
	//TICKING
	//____________________________________________________________________________
	
	public override void _PhysicsProcess(double delta)
	{
		if(walkState == MobWalkState.GoTowards){
			walkDir = currentDir*basicStats.walkSpeed;
		}
		else if(walkState == MobWalkState.WanderAround){
		}
		base._PhysicsProcess(delta);
		PlayAnimation();

	}
	public override void _Process(double delta)
	{
		if(aiTimer<0f){
			aiTimer = aiTimerMin + rnd.NextSingle()*(aiTimerMax-aiTimerMin);
			UpdateAI();
		}
		else{
			aiTimer-=(float)delta;
		}
	}


	Random rnd;

	float aiTimer = 1f;

	float aiTimerMin=0.5f;
	float aiTimerMax=1f;
	protected virtual void UpdateAI(){
		if(target == null) {GetTarget();return;}
		SetNewGoal(target.Position);
	}

	protected virtual void GetTarget(){
		//AddChild(searchArea);
		float range, minRange=10000f;
		Godot.Collections.Array<Node2D> bodies = searchArea.GetOverlappingBodies();


		if(bodies.Count <= 1) return;

		foreach(Node2D body in bodies){
			if(body == this) continue; //ignore self
			range = (body.Position-Position).Length();
			if(range < minRange){
				foreach(Type t in targetTypes){
					if(body.GetType() == t){
						GD.Print("selected target: ", body.ToString());
						minRange = range;
						target = body;
					}
				}
			}
			
		}
		//RemoveChild(searchArea);
	}



}

public enum MobWalkState{
	None,
	GoTowards,
	WanderAround
}

public enum MobGoingType{
	Towards,
	Around,
	Away
}

public enum MobAction{
	Idle,
	Chase,
	Attack
}

}
