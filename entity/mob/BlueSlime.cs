using System;
using System.Collections.Generic;
using Godot;


namespace Entities{

public partial class Slime : Mob{
	protected const float jumpTime = 1f;
	protected float jumpCDMax = 0.4f;

	public float jumpValue = 1f;

	protected bool inAir = false;

	protected float attackCooldown = 1f;

	public Slime(){
		rnd = new Random();
		bodyPath = "res://scenes/entities/BlueSlime.tscn";
		walkState = MobWalkState.WanderAround;
		goingType = MobGoingType.Towards;
		searchRadius = 400f;

		basicStats.walkSpeed = 200f + rnd.NextSingle()*10f;
	}





	public override void _Ready()
	{	
		base._Ready();

		targetTypes.Add(typeof(Player));

	}



	//---------------------------------------

	//____________________________________________________________________________
	//TICKING
	//____________________________________________________________________________
	
	public override void _PhysicsProcess(double delta)
	{
		if(walkState == MobWalkState.GoTowards){
			if(inAir){
				walkDir = currentDir;
				if(attackCooldown < 0f){
					attackCooldown = 1f;
					if((target.Position-Position).Length() < 30f && target is Entity et) et.RecieveDamage(4f,Damaging.DamageType.melee,this,Position);
				}
				else{
					attackCooldown -= (float)delta;
				}
				
			}
			else{
				walkDir = Vector2.Zero;
			}
			
			if(jumpValue < 0f){
				if(inAir){
					inAir = false;
					jumpValue = jumpCDMax*rnd.NextSingle();
				}
				else{
					inAir = true;
					jumpValue = jumpTime;
				}
			}
			else{
				jumpValue -= (float)delta;
			}
			
		}
		else if(walkState == MobWalkState.WanderAround){
			if(wanderingTime > 1f){
				walkDir = randomDir;
			}
			else if(wanderingTime > 0f){
				walkDir = Vector2.Zero;
			}
			else{
				wanderingTime = rnd.NextSingle()*1.9f+1.1f;
			}
			
		}
		base._PhysicsProcess(delta);
		PlayAnimation();

	}



	float aiTimer = 0.5f;

	float aiTimerMin=0.5f;
	float aiTimerMax=1f;
	protected override void UpdateAI(){
		if(target == null){
			target = epool?.GetNearEntity(Position,searchRadius,targetTypes);
			//if(target == null) walkState = MobWalkState.WanderAround;
			return;
		}
		SetNewGoal(target.Position);
	}


	public override void PlayAnimation()
	{
		if(walkState == MobWalkState.GoTowards){
			if(inAir){sprite.Play("move"); return;}
		}
		sprite.Play("default");
	}



}

}
