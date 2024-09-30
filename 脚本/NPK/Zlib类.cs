using Godot;
using System;
using System.Runtime.InteropServices;

public static class Zlib类
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="data"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	public static byte[] Zlib解压缩(byte[] data,int size)
	{
		return Decompress(data,size);
	}
	public static byte[] Decompress(byte[] data, int size)
	{
		var target = new byte[size];
		Decompress(target, ref size, data, data.Length);
		return target;
	}
	[DllImport(@"D:\godot\projects\地下城与勇士\zlib.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uncompress")]
	private static extern int Decompress([In][Out] byte[] dest, ref int destLen, byte[] source, int sourceLen);
}
