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
	public 贴图类[][] 贴图表;
	/// <summary>
	/// <para>把IO读出来的字节数组转为C#类的实例。</para>
	/// <para>具体参考《关于DNF的多媒体包NPK文件的那些事儿（1）》链接：https://blog.csdn.net/u010274704/article/details/77319001</para>
	/// </summary>
	/// <param name="_source">建议使用FileStream.Read()读取字节数组</param>
	public NPK类(byte[] _source)
	{
		int sindex;//_source的读取下标

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


		this.贴图表 = new 贴图类[this.IMG表.Length][];
		for (int i = 0; i < this.IMG总数; i++)
		{			
			if (this.IMG表[i].取版本() == 2)
			{
				this.贴图表[i] = new 贴图类[((IMGV2类)IMG表[i]).图块数据表.Length];	
				continue;
			}
			if (IMG表[i].取版本() == 4)
			{
				//now do nothing
				continue;
			}
			if (IMG表[i].取版本() == 5)
			{
				//now do nothing
				continue;
			}
		}
	}
	public 贴图类 取贴图表成员(int _img_index,int _png_index)
	{
		if(贴图表[_img_index][_png_index]!=null)
		{
			
			return 贴图表[_img_index][_png_index];
		}

		//if 贴图表[_img_index][_png_index] == null

		if(this.IMG表[_img_index].取版本() == 2)
		{
			贴图表[_img_index][_png_index] = new 贴图类(
			IMG索引表[_img_index].文件名+$"/{_png_index}.png",
			((IMGV2类)IMG表[_img_index]).图块索引表[_png_index].是指针类型(),
			((IMGV2类)IMG表[_img_index]).取ImageTexture(_png_index),
			new Vector2(((IMGV2类)IMG表[_img_index]).图块索引表[_png_index].X坐标,((IMGV2类)IMG表[_img_index]).图块索引表[_png_index].Y坐标),
			new Vector2(((IMGV2类)IMG表[_img_index]).图块索引表[_png_index].帧域宽,((IMGV2类)IMG表[_img_index]).图块索引表[_png_index].帧域高)
			);
			return 贴图表[_img_index][_png_index];
		}

		return null;
	}
}