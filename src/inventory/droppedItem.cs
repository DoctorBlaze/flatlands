using Godot;
using System;
using invSys;
using Entities;

namespace invSys{

/// <summary>
/// item, placed in the world. has gravity and maybe collsison
/// </summary>
public partial class DroppedItem : RigidBody3D
{
	public float pickupCooldown;
	public ItemInstance containedItem;
	public float gravity;

	//public MeshInstance3D itemMesh;
	public Sprite3D icon;

	public CollisionShape3D itemHitbox;
	

	public DroppedItem(ItemInstance itemToDrop){
		pickupCooldown = 2;
		GravityScale = 0.984f;
		containedItem = itemToDrop;

		//generate hitbox (from code)
		itemHitbox = new CollisionShape3D();
		itemHitbox.Shape = new BoxShape3D();
		

		if(containedItem == null) return; // get the path of object
		string pth = containedItem.item.scene;

		//load mesh for this item
		//Mesh meshObj = ResourceLoader.Load(pth) as Mesh;
		//itemMesh = new MeshInstance3D();
		//itemMesh.Position = new Vector3(0,-0.25f,0);
		AddChild(itemHitbox);

		icon = new Sprite3D();
		icon.Texture = ResourceLoader.Load(containedItem.item.icon) as Texture2D;
		AddChild(icon);
	}

	public DroppedItem(Item item){
		ItemInstance itemToDrop = new ItemInstance(item);

		pickupCooldown = 2;
		GravityScale = 0.984f;
		containedItem = itemToDrop;

		//generate hitbox (from code)
		itemHitbox = new CollisionShape3D();
		itemHitbox.Shape = new BoxShape3D();
		itemHitbox.Scale = new Vector3(0.5f,0.5f,0.5f);

		if(containedItem == null) return; // get the path of object
		string pth = containedItem.item.scene;

		//load mesh for this item
		//Mesh meshObj = ResourceLoader.Load(pth) as Mesh;
		//itemMesh = new MeshInstance3D();
		//itemMesh.Position = new Vector3(0,-0.25f,0);
		AddChild(itemHitbox);

		icon = new Sprite3D();
		icon.Texture = ResourceLoader.Load(containedItem.item.icon) as Texture2D;
		AddChild(icon);
	}

	public DroppedItem(){
		gravity = 9.8f;
		pickupCooldown = 2;
		itemHitbox = new CollisionShape3D();
		itemHitbox.Shape = new BoxShape3D();
		itemHitbox.Scale = new Vector3(0.32f,0.32f,0.32f);
		icon = new Sprite3D();

		AddChild(itemHitbox);
		AddChild(icon);
	}


	public override void _Ready()
	{
		itemHitbox.Scale = new Vector3(0.32f,0.32f,0.32f);
		SetCollisionLayerValue(8,true);
		SetCollisionMaskValue(8,true);

		SetCollisionLayerValue(1,false);
		SetCollisionMaskValue(1,true);
		

		icon.Billboard = BaseMaterial3D.BillboardModeEnum.Enabled;
		icon.TextureFilter = BaseMaterial3D.TextureFilterEnum.Nearest;
		icon.PixelSize = 0.02f;
		icon.Shaded = true;
		//CollisionLayer = 2;

		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(pickupCooldown>0) pickupCooldown -= (float)delta;
	}
}

}
