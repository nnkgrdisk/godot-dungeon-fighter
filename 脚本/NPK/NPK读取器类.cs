using Godot;
using System;
using System.IO;
public static class NPK读取器类
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_文件路径">文件在磁盘上的完整路径，如"C:\Users\Administrator\Desktop\sprite_interface_bandi(Transformed).npk"</param>
    /// <returns >NPK类的实例</returns>
	public static NPK类 读取NPK(string _文件路径)
    {
        FileStream fs;
        byte[] bytes;
        NPK类 npk;

        fs = new FileStream(_文件路径,FileMode.Open);
        bytes = new byte[fs.Length];
        fs.Read(bytes,0,(int)fs.Length);
        npk = new NPK类(bytes);

        fs.Close();
        
        return npk;
    }
}