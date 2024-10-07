using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class 主场景 : Node2D
{
	NPK类 npk;
	public NPK类 npk2;
	//Sprite2D sprite;
	public int index = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("hello world");

		//npk = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_interface_bandi(Transformed).npk");

		npk2 = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_character_swordman_effect_cutin(Transformed).npk");

		//sprite = new Sprite2D();
		//this.AddChild(sprite);
		//sprite.Texture = npk2.取贴图表成员(0, index).贴图;
		//sprite.Position = npk2.取贴图表成员(0, index).坐标;
		//sprite.Centered = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
