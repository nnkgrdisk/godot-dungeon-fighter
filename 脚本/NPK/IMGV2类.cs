using Godot;
using System;

public class IMGV2类
{
	public string 文件头;
	public int 图块索引表大小;//指内存占用大小
	public int 保留;
	public int 版本号;
	public int 图块索引表数目;
	public IMGV2图块索引类[] 图块索引表;
	public 图块数据类[] 图块数据表;//图块是图像数据块的简写，img内保存的图像数据块需要转换到bitmap，再转换到png才能被godot正确识别。

	/// <summary>
	/// 参考《关于DNF的多媒体包NPK文件的那些事儿（3） - IMGV2》链接：https://blog.csdn.net/u010274704/article/details/77528054
	/// </summary>
	/// <param name="_bytes"></param>
	public IMGV2类(byte[] _bytes)
	{
		int bindex;

		bindex = 0;

		this.文件头 = _bytes.复制字节数组(bindex, 16).到字符串();
		bindex += 16;
		this.图块索引表大小 = _bytes.复制字节数组(bindex, 4).到数字();
		bindex += 4;
		this.保留 = _bytes.复制字节数组(bindex, 4).到数字();
		bindex += 4;
		this.版本号 = _bytes.复制字节数组(bindex, 4).到数字();
		bindex += 4;
		this.图块索引表数目 = _bytes.复制字节数组(bindex, 4).到数字();
		bindex += 4;

		图块索引表 = new IMGV2图块索引类[this.图块索引表数目];

		for (int i = 0; i < 图块索引表.Length; i++)
		{		
			if (_bytes.复制字节数组(bindex, 4).到数字() == 0x11)//判断索引是否为指针类型
			{
				图块索引表[i] = new IMGV2图块索引类(_bytes.复制字节数组(bindex, 8));
				bindex += 8;
			}
			else
			{
				图块索引表[i] = new IMGV2图块索引类(_bytes.复制字节数组(bindex, 36));
				bindex += 36;
			}
		}

		图块数据表 = new 图块数据类[this.图块索引表数目];

		for (int i = 0; i < 图块数据表.Length; i++)
		{
			if (图块索引表[i].是指针类型())
			{
				图块数据表[i] = new 图块数据类(new byte[0]);
				bindex += 0;
			}
			else
			{
				图块数据表[i] = new 图块数据类(_bytes.复制字节数组(bindex, 图块索引表[i].图像大小));
				bindex += 图块索引表[i].图像大小;
			}
		}
	}
	public ImageTexture 取ImageTexture(int _index)
	{
		int recursion_limit = 0;
		int max_recursion_limit = 5;
		int image_index = _递归找图片型图块索引(_index);
		return this.图块数据表[image_index].取ImageTexture(this.图块索引表[image_index]);
		int _递归找图片型图块索引(int _index)
		{
			if(this.图块索引表[_index].是指针类型()==false || recursion_limit==max_recursion_limit)
			{
				return _index;
			}
			else
			{
				recursion_limit = recursion_limit+1;
				return _递归找图片型图块索引(this.图块索引表[_index].压缩状态或指向帧号);
			}
		}
	}
	public Vector2 取贴图坐标(int _index)
	{
		return new Vector2(
			this.图块索引表[_index].X坐标,
			this.图块索引表[_index].Y坐标
		);
	}
}
