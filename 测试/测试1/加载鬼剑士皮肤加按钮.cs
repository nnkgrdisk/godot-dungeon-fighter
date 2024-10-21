using Godot;
using System;

public partial class 加载鬼剑士皮肤加按钮 : Button
{
	public 加载鬼剑士皮肤 parent;
	public Label label;
	Callable callable;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetNode<加载鬼剑士皮肤>("..");
		label = GetNode<Label>("../Label");
		callable = new Callable(this,"on_click");

		this.Connect("button_up",callable);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public void on_click()
	{
		parent.YIndex = parent.YIndex+1;
		label.Text = parent.YIndex.ToString();
	}
}
