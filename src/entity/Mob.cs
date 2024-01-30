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

	protected float wanderingTime = 0f;

	//____________________________________________________________________________
	//SEARCHING TARGETS
	//____________________________________________________________________________

	protected float searchRadius=400f;

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


	protected Vector2 randomDir = Vector2.Zero;


	public override void _Ready()
	{	
		base._Ready();

		rnd = new Random();

		targetTypes = new List<Type>();

		LoadBody();

	}



	//---------------------------------------
	protected MobWalkState walkState  = MobWalkState.WanderAround;
	protected MobGoingType goingType = MobGoingType.Towards;
	protected MobAction action = MobAction.Idle;

	//____________________________________________________________________________
	//TICKING
	//____________________________________________________________________________
	/*
	public override void _PhysicsProcess(double delta)
	{
		if(walkState == MobWalkState.GoTowards){
			walkDir = currentDir;
		}
		else if(walkState == MobWalkState.WanderAround){
		}
		base._PhysicsProcess(delta);
		PlayAnimation();

	}*/
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


	protected Random rnd;

	float aiTimer = 0.5f;

	float aiTimerMin=0.5f;
	float aiTimerMax=1f;
	protected virtual void UpdateAI(){
		if(target == null){
			target = epool?.GetNearEntity(Position,searchRadius,targetTypes);
			return;
		}
		SetNewGoal(target.Position);
	}

	public override void OnDeath(){
		target = null;
		
		epool.DestroyEntity(this);
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
