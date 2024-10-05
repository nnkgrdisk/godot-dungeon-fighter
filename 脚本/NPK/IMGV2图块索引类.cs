using Godot;
using System;

/// <summary>
/// <para>IMGV2、4、5的索引结构不尽相同，故分成几个类分开管理</para>
/// <para>参考《关于DNF的多媒体包NPK文件的那些事儿（3） - IMGV2》链接：https://blog.csdn.net/u010274704/article/details/77528054</para>
/// </summary>
public class IMGV2图块索引类
{
    public int 颜色系统或指针类型;//图像类型索引=颜色系统，指针类型索引=指针类型（0x11）
    public int 压缩状态或指向帧号;//图像类型索引=压缩状态，指针类型索引=指向帧号
    public int 图像宽;//以下部分指针类型索引的值均为-1
    public int 图像高;
    public int 图像大小;
    public int X坐标;
    public int Y坐标;
    public int 帧域宽;
    public int 帧域高;

    public IMGV2图块索引类(byte[] _bytes)
    {
        int bindex;

        bindex = 0;

        this.颜色系统或指针类型 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.压缩状态或指向帧号 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        if (this.颜色系统或指针类型 == 0x11)
        {
            this.图像宽 = -1;
            this.图像高 = -1;
            this.图像大小 = -1;
            this.X坐标 = -1;
            this.Y坐标 = -1;
            this.帧域宽 = -1;
            this.帧域高 = -1;
            return;
        }
        this.图像宽 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.图像高 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.图像大小 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.X坐标 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.Y坐标 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.帧域宽 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
        this.帧域高 = _bytes.复制字节数组(bindex, 4).到数字();
        bindex += 4;
    }
    public bool 是指针类型()
    {
        return 颜色系统或指针类型 == 0x11 ? true : false;
    }
    public bool 是Zlib压缩状态()
    {
        return this.压缩状态或指向帧号 == 0x06 ? true : false;
    }
}
