using Godot;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class 图块数据类
{
	public byte[] 源数据;
	public 图块数据类(byte[] _bytes)
	{
		this.源数据 = _bytes;
	}
	public enum ColorBits
	{
		ARGB_1555 = 0x0e,
		ARGB_4444 = 0x0f,
		ARGB_8888 = 0x10,
		LINK = 0x11,
		DXT_1 = 0x12,
		DXT_3 = 0x13,
		DXT_5 = 0x14,
		UNKNOWN = 0x00
	}
	public static System.Drawing.Bitmap RGB数组到Bitmap(byte[] data, Size size, ColorBits bits,bool is_not_enctypt)
	{
		var bytes_size = size.Width * size.Height * (bits == ColorBits.ARGB_8888 ? 4 : 2);
            if (is_not_enctypt) {
                data = Zlib类.Zlib解压缩(data, bytes_size);
            }
		var ms = new MemoryStream(data);
		data = new byte[size.Width * size.Height * 4];
		for (var i = 0; i < data.Length; i += 4)
		{
			ReadColor(ms, bits, data, i);
		}
		ms.Close();
		return FromArray(data, size);
	}

	#pragma warning disable CA1416
	public static byte[] BitMap到字节数组(System.Drawing.Image image,ImageFormat format)
	{
        using(MemoryStream ms = new MemoryStream())
        {
            image.Save(ms, format);
            return ms.ToArray();
        }
	}
	public static int Read(Stream stream, int length, out byte[] buf)
	{
		buf = new byte[length];
		return stream.Read(buf, 0, length);
	}
	public static void ReadColor(Stream stream, int bits, byte[] target, int offset)
	{
		byte[] bs;
		if (bits == (int)ColorBits.ARGB_8888)
		{
			Read(stream, 4, out bs);
			bs.CopyTo(target, offset);
			return;
		}
		byte a = 0;
		byte r = 0;
		byte g = 0;
		byte b = 0;
		Read(stream, 2, out bs);
		switch (bits)
		{
			case (int)ColorBits.ARGB_1555:
				a = (byte)(bs[1] >> 7);
				r = (byte)((bs[1] >> 2) & 0x1f);
				g = (byte)((bs[0] >> 5) | ((bs[1] & 3) << 3));
				b = (byte)(bs[0] & 0x1f);
				a = (byte)(a * 0xff);
				r = (byte)((r << 3) | (r >> 2));
				g = (byte)((g << 3) | (g >> 2));
				b = (byte)((b << 3) | (b >> 2));
				break;
			case (int)ColorBits.ARGB_4444:
				a = (byte)(bs[1] & 0xf0);
				r = (byte)((bs[1] & 0xf) << 4);
				g = (byte)(bs[0] & 0xf0);
				b = (byte)((bs[0] & 0xf) << 4);
				break;
		}
		target[offset + 0] = b;
		target[offset + 1] = g;
		target[offset + 2] = r;
		target[offset + 3] = a;
	}

	public static void ReadColor(Stream stream, ColorBits bits, byte[] target, int offset)
	{
		ReadColor(stream, (int)bits, target, offset);
	}

	#pragma warning disable CA1416
	public static System.Drawing.Bitmap FromArray(byte[] data, Size size)
	{
		
		var bmp = new System.Drawing.Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
		var bmpData = bmp.LockBits(new Rectangle(Point.Empty, size), ImageLockMode.WriteOnly,
			PixelFormat.Format32bppArgb);
		Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
		bmp.UnlockBits(bmpData);
		return bmp;
	}
}
