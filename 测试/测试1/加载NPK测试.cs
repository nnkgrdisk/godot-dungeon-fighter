using Godot;
using System;

public partial class 加载NPK测试 : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string file_path = @"C:\Users\Administrator\Desktop\sprite_character(Transformed).npk";
		NPK类 npk = NPK读取器类.读取NPK(file_path);
		GD.Print("npk.IMG总数"+npk.IMG总数);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
