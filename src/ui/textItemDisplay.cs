using Godot;
using System;
using Entities;
using invSys;
using GUI;

namespace GUI{
public partial class TextItemDisplay : Panel
{
	private VBoxContainer container;
	public SlotPanel ownerSlotPanel;

	public TextItemDisplay(ItemInstance item, Vector2 cords,SlotPanel ownerSlotPanel_){
		ownerSlotPanel = ownerSlotPanel_;
		Position = cords;
		if(item.item.description != null) Size = new Vector2(200,48+item.item.description.Length*30);
		else Size = new Vector2(200,48);
		container = new();
		container.Name = "VBoxCont";
		container.SetAnchorsPreset(LayoutPreset.FullRect);
		AddChild(container);

		Label mainLabel = new();
		mainLabel.Text = item.item.name;
		//mainLabel.AddThemeFontSizeOverride("bruh",24);
		container.AddChild(mainLabel);
		if(item.item.description == null) return;
		container.AddChild(new ColorRect(){CustomMinimumSize = new Vector2(0,1),Color = new Color(1f,1f,1f)});

		foreach(String i in item.item.description){
			Label tmp = new();
			//tmp.AddThemeFontSizeOverride("bruh1",16);
			tmp.Text = i;
			container.AddChild(tmp);
		}

		
	}
}

}
