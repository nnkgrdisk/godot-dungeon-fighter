using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class 主场景 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("hello world");
		资源加载单例类.唯一实例.初始化();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
