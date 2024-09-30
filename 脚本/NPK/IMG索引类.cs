using Godot;
using System;

public class IMG索引类
{
	public int 文件偏移;
	public int 文件大小;
	public string 文件名;

	/// <summary>
	/// 把IO读出来的字节数组转为C#类的实例，传入字节数组长度限定264
	/// </summary>
	/// <param name="_bytes"></param>
	public IMG索引类(byte[] _bytes)
	{
		int bindex;

		bindex = 0;

		this.文件偏移 = _bytes.复制字节数组(bindex, 4).到数字();
		bindex += 4;
		this.文件大小 = _bytes.复制字节数组(bindex, 4).到数字();
		bindex += 4;
		this.文件名 = _bytes.复制字节数组(bindex, 256).到解密字符串();
		bindex += 256;
	}
	public void 调试输出()
	{
		GD.Print("文件偏移："+this.文件偏移);
		GD.Print("文件大小："+this.文件大小);
		GD.Print("文件名："+this.文件名);
	}
}
