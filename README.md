# godot_dungeon_fighter

使用godot引擎制作的程序。 

设想的目标是通过对dnf游戏资源的读取实现单机版游戏（弥天大坑......） 。 

目前对NPK的解析，仅实现IMGV2的加载，IMGV4、5的部分正在施工......

##

要运行此项目，需要godot 4.2.2 mono x64，并安装好.net环境。 

目前使用的外部代码： 

1. ExtractorSharp npk相关算法 https://github.com/d-mod/ExtractorSharp 
2. zlib 图形数据解压缩 https://www.zlib.net/ 

使用的外部dll： 
1. zlib.dll 自源码编译的64位版本。
2. System.Drawing.Common.dll nuget上下载的。

如果无法正常运行，在排除.net runtime问题后，请依次检查： 

1. Zlib类.cs中zlib.dll引用位置是否正确。 
2. 尝试使用vs2019卸载System.Drawing.Common.dll，在nuget上下载system.drawing.common.8.0.8.nupkg，使用vs2019打开项目，加载这个nupkg。