using Godot;
using System;

public partial class 测试1 : Node2D
{
	
	public NPK类 npk2;
	public int index = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		npk2 = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_character_swordman_effect_cutin(Transformed).npk");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
