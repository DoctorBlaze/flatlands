using Godot;
using System;
using Damaging;
using System.Formats.Asn1;
using Statistycs;

namespace Entities{
public partial class Entity : CharacterBody2D
{

	/// <summary>
	/// includes hitbox, animated sprite, etc
	/// </summary>
	protected Node2D body;
	public string bodyPath = "";


	//____________________________________________________________________________
	//stats system
	//____________________________________________________________________________
	public float health;
	public Statistycs.StatsBunch basicStats;
	public Statistycs.StatsBunch genericStats;

	//public Vector3 walkDirection;

	//____________________________________________________________________________
	// entity elements
	//____________________________________________________________________________
	protected AnimatedSprite2D sprite;
	protected CollisionShape2D hitbox;


	/// <summary>
	/// where this entity currently looking at
	/// </summary>
	public RotationState rState;

	//main
	/// <summary>
	/// main scene that's being loaded when entity spawned
	/// </summary>
	public Node2D gameWorld;
	//____________________________________________________________________________
	// loading
	//____________________________________________________________________________
	public void LoadBody(){
		PackedScene ps;

		if(body != null) body.QueueFree();
		ps = ResourceLoader.Load<PackedScene>(bodyPath);
		if(ps == null) return;
		body = ps.Instantiate<Node2D>();
		if(body == null) return;

		Godot.Collections.Array<Node> children = body.GetChildren();
		
		foreach(Node ch in children){
			body.RemoveChild(ch);
			AddChild(ch);
		}


		body.QueueFree();
		
		OnBodyLoaded();
	}

	//____________________________________________________________________________
	// movement
	//____________________________________________________________________________

	public Vector2 movementInertion;

	/// <summary>
	/// Direction where entity walks on it's own
	/// </summary>
	public Vector2 walkDir;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = walkDir*basicStats.walkSpeed;
		
		//velocity = walkDir;
		
		if(walkDir != Vector2.Zero){
			if(walkDir.Y > 0) rState = RotationState.Front;
			else if(walkDir.Y < 0) rState = RotationState.Back;
			else if(walkDir.X > 0) rState = RotationState.Right;
			else if(walkDir.X < 0) rState = RotationState.Left;			
		}

		Velocity = velocity;
		
		MoveAndSlide();
	}

	public override void _Ready()
	{
		CalcStats();
	}

	public Entity(){
		BasicStatsInit();
		CalcStats();
	}



	public virtual void CalcStats(){
		genericStats = basicStats;
		//genericStats.AddModifier(new Modifier(1.0f,StatList.walkSpeed,false));
	}

	public virtual DamageResult RecieveDamage(float damage, DamageType damageType, Entity owner, float[] addParams, Vector3 pos){
		float resDmg = 0.0f;
		if(damageType == DamageType.projectile){
			if(addParams[0] >= genericStats.armor){
				addParams[0] -= genericStats.armor;
				resDmg = damage / genericStats.projectileResistance / genericStats.resistance;
			}
			else{
				resDmg = damage / genericStats.projectileResistance * (50 + new Random().Next()%50)*0.01f / genericStats.resistance;
			}
		}
		else if(damageType == DamageType.explosion){
			resDmg = damage / genericStats.explosionResistance / genericStats.resistance;;
		}
		else if(damageType == DamageType.pure){
			resDmg = damage / genericStats.resistance;
		}
		else{
			resDmg = damage;
		}

		health -= resDmg;

		DamageResult damageResult = new(){
			resultDamage = resDmg,
			target = this,
			damagePosition = pos,
			targetDied = false
		};

		if(health <= 0f){OnDeath();
		damageResult.targetDied = true;}

		OnDamageRecieve(damageResult);
		owner?.OnDamageDeal(damageResult);
		
		return damageResult;
	}


	public virtual void BasicStatsInit(){
		basicStats.maxHealth = 100.0f;
		basicStats.regeneration = 0.1f;
		basicStats.walkSpeed = 16f;
		basicStats.jumpStrength = 4.0f;
		basicStats.damage = 1.0f;
		basicStats.projectileDamage = 25.0f;
		basicStats.projectilePierce = 1.0f;
		basicStats.projectileSpeed = 1.0f;
		basicStats.critDamage = 0.0f;
		basicStats.critChance = 0.0f;
		basicStats.explosionDamage = 20.0f;
		basicStats.explosionRadius = 1.0f;
		basicStats.explosionKnockback = 1.0f;
		basicStats.armor = 1.0f;
		basicStats.resistance = 1.0f;
		basicStats.projectileResistance = 1.0f;
		basicStats.explosionResistance = 1.0f;
		basicStats.thermalResistance = 1.0f;
	}



	//____________________________________________________________________________
	//entity events
	//____________________________________________________________________________
	protected virtual void OnBodyLoaded(){
		hitbox = GetNode<CollisionShape2D>("hitbox");
		sprite = GetNode<AnimatedSprite2D>("sprite");
	}

	public virtual void OnSpawn(){
		
	}

	public virtual void OnDamageRecieve(DamageResult damageResult){

	}

	public virtual void OnDamageDeal(DamageResult damageResult){

	}

	public virtual void OnInteract(){

	}

	public virtual void OnDeath(){

	}

	//____________________________________________________________________________
	//animation
	//____________________________________________________________________________
	public virtual void PlayAnimation(){
		
	}
}

}
