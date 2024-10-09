using Godot;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class 资源加载单例类
{
	public static 资源加载单例类 唯一实例 = new 资源加载单例类();
	private string NPK资源路径;
	private string PVF资源路径;
	private NPK类[] npk表;
	public 资源加载单例类()
	{
	}
	public void 初始化()
	{
		string config_path;
		StreamReader sr;
		Regex npk_regex;
		Regex pvf_regex;
		string sr_str;
		DirectoryInfo npk_dir_info;
		FileInfo[] npk_dir_file_info_array;

		config_path = System.Environment.CurrentDirectory + "\\资源路径配置.txt";
		sr = new StreamReader(config_path);
		npk_regex = new Regex(@"\[NPK=(?<path>.{0,})\]");
		pvf_regex = new Regex(@"\[PVF=(?<path>.{0,})\]");
		sr_str = sr.ReadToEnd();

		this.NPK资源路径 = npk_regex.Match(sr_str).Groups["path"].Value;
		//GD.Print(this.NPK资源路径);
		this.PVF资源路径 = pvf_regex.Match(sr_str).Groups["path"].Value;
		//GD.Print(this.PVF资源路径);

		sr.Close();

		npk_dir_info = new DirectoryInfo(this.NPK资源路径);
		npk_dir_file_info_array = npk_dir_info.GetFiles();

		this.npk表 = new NPK类[npk_dir_file_info_array.Length];

		for (int i = 0; i < npk_dir_file_info_array.Length; i++)
		{
			GD.Print(npk_dir_file_info_array[i].FullName);
			this.npk表[i] = NPK读取器类.读取NPK(npk_dir_file_info_array[i].FullName);
			
		}
	}
}
