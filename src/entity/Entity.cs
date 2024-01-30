using Godot;
using System;
using Damaging;
using System.Formats.Asn1;
using Statistycs;
using World;

namespace Entities{
public partial class Entity : CharacterBody2D
{
	protected Node2D body;

	Color normalColor = new Color(1f,1f,1f,1f);
	Color hurtColor = new Color(1f,0.5f,0.5f,1f);
	public float hframes = 0f;

	public EntityPool epool = null;


	public string bodyPath = "";


	//____________________________________________________________________________
	//stats system
	//____________________________________________________________________________
	public float health = 100f;
	public Statistycs.StatsBunch basicStats;
	public Statistycs.StatsBunch genericStats;

	//public Vector3 walkDirection;

	//____________________________________________________________________________
	// entity elements
	//____________________________________________________________________________
	protected AnimatedSprite2D sprite;
	//protected CollisionShape2D hitbox;


	/// <summary>
	/// where this entity currently looking at
	/// </summary>
	public RotationState rState;

	//main
	/// <summary>
	/// main scene that's being loaded when entity spawned
	/// </summary>
	//public Node2D gameWorld;
	//____________________________________________________________________________
	// loading
	//____________________________________________________________________________
	public void LoadBody(){
		PackedScene ps;
		

		//if(body != null) body.QueueFree();
		ps = ResourceLoader.Load<PackedScene>(bodyPath);
		if(ps == null) return;
		body = ps.Instantiate<Node2D>();
		if(body == null) return;

		AddChild(body);


		OnBodyLoaded();
	}

	//____________________________________________________________________________
	// movement
	//____________________________________________________________________________

	public Vector2 inertion;

	/// <summary>
	/// Direction where entity walks on it's own
	/// </summary>
	public Vector2 walkDir;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = walkDir*genericStats.walkSpeed;
		
		if(sprite != null){
		if(hframes >0f){
			hframes-=(float)delta;
			sprite.Modulate = hurtColor;
		}
		else{
			sprite.Modulate = normalColor;
		}}
		
		if(walkDir != Vector2.Zero){
			if(walkDir.Y > 0) rState = RotationState.Front;
			else if(walkDir.Y < 0) rState = RotationState.Back;
			else if(walkDir.X > 0) rState = RotationState.Right;
			else if(walkDir.X < 0) rState = RotationState.Left;			
		}

		if(Mathf.Abs(inertion.X) > 0.1f || Mathf.Abs(inertion.Y) > 0.1f){
			inertion.X *=0.9f; inertion.Y *=0.9f;
			velocity += inertion;
		}
		Velocity = velocity;

		MoveAndSlide();

	}

	public override void _Ready()
	{
		CalcStats();
	}

	public Entity(){
		inertion = new Vector2(0f,0f);
		BasicStatsInit();
		CalcStats();
	}



	public virtual void CalcStats(){
		genericStats = basicStats;
		
	}

	public virtual void RecieveDamage(float damage, DamageType damageType, Entity damager, Vector2 pos){
		Vector2 dir = (Position-pos).Normalized()*1200f;
		float resDmg = 0.0f;

		inertion = dir; hframes = 0.12f;
		
		resDmg = damage;

		health -= resDmg;



		if(health <= 0f){OnDeath();}

		//damager?.OnDamageDeal(damageResult);

	}


	public virtual void BasicStatsInit(){
		basicStats.maxHealth = 100.0f;
		basicStats.regeneration = 0.1f;
		basicStats.walkSpeed = 200f;
		basicStats.jumpStrength = 4.0f;
		basicStats.damage = 5.0f;
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
		//hitbox = body.GetNode<CollisionShape2D>("hitbox");
		sprite = body.GetNode<AnimatedSprite2D>("sprite");
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
