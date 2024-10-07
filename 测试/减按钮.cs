using Godot;
using System;

public partial class 减按钮 : Button
{
	主场景 根节点;
	Sprite2D sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		根节点 = GetNode<主场景>("..");
		sprite = GetNode<Sprite2D>("../Sprite2D");
		this.Connect("button_up",new Callable(this,"on_click"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void on_click()
	{
		根节点.index=16;
		sprite.Texture = ((IMGV2类)根节点.npk2.IMG表[9]).取ImageTexture(根节点.index);
		sprite.Position = 根节点.npk2.取贴图表成员(9,根节点.index).坐标;
		sprite.Centered = false;
		GD.Print(根节点.index);
		GD.Print(new Vector2(
			((IMGV2类)根节点.npk2.IMG表[9]).图块索引表[根节点.index].图像宽,
			((IMGV2类)根节点.npk2.IMG表[9]).图块索引表[根节点.index].图像高
		));		
		GD.Print(new Vector2(
			((IMGV2类)根节点.npk2.IMG表[9]).图块索引表[根节点.index].帧域宽,
			((IMGV2类)根节点.npk2.IMG表[9]).图块索引表[根节点.index].帧域高
		)
		);
	}	
}
