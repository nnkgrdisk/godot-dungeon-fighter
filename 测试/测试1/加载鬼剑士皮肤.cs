using Godot;
using System;

public partial class 加载鬼剑士皮肤 : Node
{
	Sprite2D sprite;
	string path;
	NPK类 npk;
	int xindex = 0;
	public int XIndex
	{
		get { return xindex; }
		set
		{
			xindex = value;
			on_index_change();
		}
	}
	int yindex = 0;
	public int YIndex
	{
		get { return yindex; }
		set
		{
			yindex = value;
			on_index_change();
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//path = @"C:\Users\Administrator\Desktop\测试用npk\sprite_character_swordman_equipment_avatar_skin阉割版.NPK";
		//path = @"C:\Users\Administrator\Desktop\测试用npk\sprite_character_swordman_equipment_avatar_skin.NPK";
		path = @"C:\Users\Administrator\Desktop\测试用npk\sprite_character_swordman_equipment_avatar_skin - 46.NPK";
		sprite = GetNode<Sprite2D>("Sprite2D");
		npk = NPK读取器类.读取NPK(path);
		sprite.Texture = npk.取贴图表成员(0, 0).贴图;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	void on_index_change()
	{
		sprite.Texture = npk.取贴图表成员(xindex, yindex).贴图;
	}
}
