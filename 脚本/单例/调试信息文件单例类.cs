using Godot;
using System;
using System.IO;

public class 调试信息文件单例类
{
	public static 调试信息文件单例类 唯一实例 = new 调试信息文件单例类();
	private string 文件路径;
	StreamWriter 文件写入器;
	public 调试信息文件单例类 打开文件(string _file_path)
	{
		this.文件路径 = _file_path;
		this.文件写入器 = new StreamWriter(this.文件路径,true);
		return this;
	}
	public 调试信息文件单例类 输出信息(string _info)
	{
		this.文件写入器.WriteLine(_info);
		return this;
	}
	public void 关闭文件()
	{
		this.文件写入器.Close();		
	}
}
