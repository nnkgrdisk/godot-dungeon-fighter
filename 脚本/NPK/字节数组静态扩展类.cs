using Godot;
using System;
using System.Data.SqlTypes;
using System.Text;

/// <summary>
/// 为方便读取NPK文件时操作字节数组，需要的一些函数放在这里
/// </summary>
public static class 字节数组静态扩展类
{
    public static byte[] 复制字节数组(this byte[] _source, int _start, int _length)
    {
        byte[] result;
        result = new byte[_length];

        for (int i = 0; i < _length; i++)
        {
            result[i] = _source[_start + i];
        }

        return result;
    }
    public static int 到数字(this byte[] _source, int _start = 0)
    {
        int result;

        result = BitConverter.ToInt32(_source, _start);

        return result;
    }
    public static string 到字符串(this byte[] _source)
    {
        string result;

        result = System.Text.Encoding.UTF8.GetString(_source);

        return result;
    }
    /// <summary>
    /// 特殊函数，仅用于解密IMG名字，具体方法参考https://nnkgrdisk.github.io/00/web.html
    /// </summary>
    /// <returns></returns>
    public static string 到解密字符串(this byte[] _source)
    {
        string result;
        string key;
        byte[] result_bytes;

        key = @"puchikon@neople dungeon and fighter DNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNFDNF ";
        result_bytes = new byte[256];

        for (int i = 0; i < 256; i++)
        {
            result_bytes[i] = (byte)(_source[i] ^ key[i]);
        }

        result = System.Text.Encoding.UTF8.GetString(result_bytes);
        result = result.Substring(0, result.IndexOf(".img") + ".img".Length);//img名字固定以".img"结尾

        return result;
    }
    public static void 调试输出(this byte[] _bytes)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append('[');
        for (int i = 0; i < _bytes.Length - 1; i++)
        {
            sb.Append(_bytes[i]);
            sb.Append(',');
        }
        sb.Append(_bytes[_bytes.Length - 1]);
        sb.Append(']');
        GD.Print(sb.ToString());
    }
}
