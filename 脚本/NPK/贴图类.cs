using Godot;
using System;

public class 贴图类
{
	public readonly string 名称;
	public readonly bool 是指针类型;
	public readonly ImageTexture 贴图;
	public readonly Vector2 坐标;
	public readonly Vector2 帧域;
	public 贴图类(string _name,bool _is_pointer,ImageTexture _texture,Vector2 _position,Vector2 _canvas_size)
	{
		this.名称 = _name;
		this.是指针类型 = _is_pointer;
		this.贴图 = _texture;
		this.坐标 = _position;
		this.帧域 = _canvas_size;
	}
}
