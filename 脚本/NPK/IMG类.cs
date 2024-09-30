using Godot;
using System;

public class IMG类
{
	public byte[] 源数据;

	/// <summary>
	/// <para>把IO读出来的字节数组转为C#类的实例。</para>
	/// <para>IMG类实例生成后不能直接读取数据，但提供IMGV2、4、5类型确定函数及转换函数，请转换至指定类型后再读取相应数据。</para>
	/// </summary>
	/// <param name="_source"></param>
	public IMG类(byte[] _source)
	{
		this.源数据 = _source;
	}
	public int 取版本()
	{
		int result;

		result = BitConverter.ToInt32(this.源数据.复制字节数组(24,4));

		return result;
	}
	public static explicit operator IMGV2类(IMG类 _self)
	{
		IMGV2类 result = new IMGV2类(_self.源数据);

		return result;
	}
}
