using Godot;
using GUI;
using System;


public partial class SkillTree : Panel
{
	// Called when the node enters the scene tree for the first time.

	private Control movableNode;

	private Vector2 windowMotion;



	public override void _Ready()
	{
		movableNode = GetNode<Control>("MovableNode");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		windowMotion = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down")*-12.0f;
		movableNode.Position+= windowMotion;

	}




}

