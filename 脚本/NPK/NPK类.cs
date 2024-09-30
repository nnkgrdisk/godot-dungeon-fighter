using Godot;
using System;

/// <summary>
/// 
/// </summary>
public class NPK类
{
	public string 文件头;
	public int IMG总数;
	public IMG索引类[] IMG索引表;
	public byte[] 校验位;
	public IMG类[] IMG表;
	/// <summary>
	/// <para>把IO读出来的字节数组转为C#类的实例。</para>
	/// <para>具体参考《关于DNF的多媒体包NPK文件的那些事儿（1）》链接：https://blog.csdn.net/u010274704/article/details/77319001</para>
	/// </summary>
	/// <param name="_source">建议使用FileStream.Read()读取字节数组</param>
	public NPK类(byte[] _source)
	{
		int sindex;

		sindex = 0;

		this.文件头 = _source.复制字节数组(sindex, 16).到字符串();
		sindex += 16;
		this.IMG总数 = _source.复制字节数组(sindex, 4).到数字();
		sindex += 4;

		this.IMG索引表 = new IMG索引类[this.IMG总数];

		for (int i = 0; i < this.IMG总数; i++)
		{
			this.IMG索引表[i] = new IMG索引类(_source.复制字节数组(sindex, 264));
			sindex += 264;
		}

		this.校验位 = _source.复制字节数组(sindex, 32);//校验位固定为32位
		sindex += 32;

		this.IMG表 = new IMG类[this.IMG总数];

		for (int i = 0; i < this.IMG总数; i++)
		{
			this.IMG表[i] = new IMG类(_source.复制字节数组(sindex, this.IMG索引表[i].文件大小));
			sindex += this.IMG索引表[i].文件大小;
		}
	}
	public void 调试输出()
	{
		GD.Print("文件头：" + this.文件头);

		GD.Print("IMG总数：" + this.IMG总数);

		GD.Print("<IMG索引表：>");
		foreach (var item in this.IMG索引表)
		{
			item.调试输出();
		}
		GD.Print("/<IMG索引表：>");

		this.校验位.调试输出();

		GD.Print("<IMG表：>");
		for (int i = 0; i < this.IMG总数; i++)
		{
			if(IMG表[i].取版本()==2)
			{
				IMGV2类 v2 = (IMGV2类)IMG表[i];
				v2.调试输出();
			}
		}
		GD.Print("</IMG表：>");
	}
}