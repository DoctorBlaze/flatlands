using Godot;
using System;
using GUI;


namespace GUI{
public partial class ContainerDisplay : Control
{
	//fgdg
	public SlotPanel[] slotPanels;
	public invSys.ItemContainer contRef;
	public GUI.PlayerGUI guiOwner; //not the slot owner
	public Panel mainPanel;

	public float maxWeight;

	private Button moveButton;
	private bool movableWindow = false;


	public ContainerDisplay(Panel basePanel){
		
		mainPanel = basePanel;
		AddChild(mainPanel);

		moveButton = mainPanel.GetChild<Button>(3);
		//GD.Print(" child class got all props");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = new Vector2(240,60);
		Random rnd = new();
		Position = Position + new Vector2(rnd.Next(-60,120),rnd.Next(-40,60));
	}

	public void GenGrid(){
		GridContainer slotsGrid = mainPanel.GetChild<GridContainer>(0);
		Label contLabel = mainPanel.GetChild<Label>(1);
		
		slotPanels = new SlotPanel[contRef.items.Length];

		if(contRef == null) {
			GD.Print("no container ref");
			return;
		}

		if(contRef.contName != ""){
			contLabel.Text = contRef.contName;
		}
		else{
			contLabel.Text = "Container";
		}

		//slotsGrid.r = new Vector2(64,64);
		//GD.Print("minsize",slotsGrid.GetMinimumSize());
		//add new slots procedurally
		for(int i = 0; i < contRef.items.Length; ++i){
				slotPanels[i] = new(contRef, i)
				{
					CustomMinimumSize = new Vector2(64.0f, 64.0f)
				};
				slotPanels[i].pGUI = this.guiOwner;

			slotsGrid.AddChild(slotPanels[i]);
		}

		mainPanel.Size = new Vector2(mainPanel.Size.X,80.0f*(contRef.items.Length/6 + 2));
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
		   if (moveButton.IsHovered() && mouseButtonEvent.Pressed)
			{
				movableWindow = !movableWindow;
			}    
		}
		else if (@event is InputEventMouseMotion mouseMotionEvent && movableWindow)
		{
			Position = mouseMotionEvent.Position - moveButton.Position;
		}
	}

	public void SlotsUpdate(){
		for(int i = 0; i < slotPanels.Length; ++i){
			slotPanels[i]?.SlotUpdate();
		}

	}
}

}
