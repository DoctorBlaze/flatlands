using Godot;
using System;
using Entities;
using Inventory;
using GUI;
using Statistycs;

namespace GUI{
public partial class TextItemDisplay : Panel
{
	private VBoxContainer container;
	public SlotPanel ownerSlotPanel;

	public TextItemDisplay(ItemInstance item, Vector2 cords,SlotPanel ownerSlotPanel_){
		char operation = '+';
		Label tmp;
		ownerSlotPanel = ownerSlotPanel_;
		Position = cords;
		Size = new Vector2(256,48);
		container = new();
		container.Name = "VBoxCont";
		container.SetAnchorsPreset(LayoutPreset.FullRect);
		AddChild(container);

		Label mainLabel = new();
		mainLabel.Text = item.item.name;
		container.AddChild(mainLabel);
		if(item.item.description == null) return;
		container.AddChild(new ColorRect(){CustomMinimumSize = new Vector2(0,1),Color = new Color(1f,1f,1f)});

		foreach(String i in item.item.description){
			tmp = new();
			tmp.Text = i;
			Size += new Vector2(0,30);
			if(Size.X < tmp.Size.X){
				Size = new Vector2(tmp.Size.X,Size.Y);
			}
			container.AddChild(tmp);
		}

		if(item.item is IItemModifier itemm){
			tmp = new();
			tmp.Text = "\n"+itemm.modifierType.ToString();
			Size += new Vector2(0,60);
			if(Size.X < tmp.Size.X){
				Size = new Vector2(tmp.Size.X,Size.Y);
			}
			container.AddChild(tmp);
			foreach(Modifier m in itemm.modifiers){
				if(m.type == Statistycs.ModifierType.Add) operation = '+';
				else operation = 'x';
				tmp = new();
				tmp.Modulate = new Color(0.5f,0.5f,1f,1f);
				tmp.Text = m.stat.ToString()+": "+operation+m.value;
				Size += new Vector2(0,30);
				if(Size.X < tmp.Size.X){
					Size = new Vector2(tmp.Size.X,Size.Y);
				}
				container.AddChild(tmp);
			}
		}

		
	}
}

}
