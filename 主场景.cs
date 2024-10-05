using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class 主场景 : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NPK类 npk;
		NPK类 npk2;
		Sprite2D sprite;
		IMGV2类 source;
		ImageTexture texture;

		GD.Print("hello world");

		npk = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_interface_bandi(Transformed).npk");

		npk2 = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_character_swordman_effect_cutin(Transformed).npk");
		
		sprite = new Sprite2D();
		this.AddChild(sprite);
		source = (IMGV2类)npk2.IMG表[0];
		texture = source.取ImageTexture(7);
		sprite.Texture = texture;
		sprite.Position = source.取贴图坐标(7);
		sprite.Centered = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
