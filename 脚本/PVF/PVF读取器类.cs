using Godot;
using System;
using System.IO;

public static class PVF读取器类
{
	public static string 读取PVF脚本(string _path)
	{
		string str = "";
		using (StreamReader reader = new StreamReader(_path))
		{
			str = reader.ReadToEnd();
		}
		return str;
	}
}
