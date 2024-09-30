# godot_dungeon_fighter

使用godot引擎制作的程序。 

设想的目标是通过对dnf游戏资源的读取实现单机版游戏（弥天大坑......） 。 

要运行此程序，需要godot 4.2.2 mono x64，并安装好.net环境。 

目前使用的外部代码： 

1. ExtractorSharp npk相关算法 https://github.com/d-mod/ExtractorSharp 
2. zlib 图形数据解压缩 https://www.zlib.net/ 

使用的外部dll： 
1. zlib.dll 自源码编译的64位版本。
2. System.Drawing.Common.dll nuget上下载的。