using Godot;
using System;
using System.IO;
/// <summary>
/// 测试2
/// 读取pvf各种类型的脚本并转换到相应的模型类。
/// 包含lst、chr、ani、equ、map等
/// </summary>
public partial class 测试2 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//读取LST文件();
		读取ANI文件();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void 读取LST文件()
	{
		string str = PVF读取器类.读取PVF脚本(@"D:\godot\projects\地下城与勇士_资源\PVF\character\character.lst");
		LST类 lst = new LST类(str);
		lst.数据字典.Dump();
	}
	public void 读取ANI文件()
	{
		string str = PVF读取器类.读取PVF脚本(@"D:\godot\projects\地下城与勇士_资源\PVF\character\swordman\animation\attack1.ani");
		ANI类 ani = new ANI类(str);
		//GD.Print(ani.tokens.Count);
		//ani.TokensDump();
		ani.FramesDump();
	}
}
