using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class 主场景 : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NPK类 npk;
		NPK类 npk2;
		Sprite2D sprite;
		byte[] image_buffer;
		System.Drawing.Bitmap image_bitmap;
		IMGV2类 source;
		System.Drawing.Size size;
		ImageTexture texture;

		GD.Print("hello world");

		npk = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_interface_bandi(Transformed).npk");
		//npk.调试输出();

		npk2 = NPK读取器类.读取NPK(@"C:\Users\Administrator\Desktop\sprite_character_swordman_effect_cutin(Transformed).npk");
		//npk2.调试输出();
		
		sprite = new Sprite2D();
		this.AddChild(sprite);
		sprite.Position = new Vector2(0,0);
		sprite.Centered = false;

		Godot.Image image = new Godot.Image();

		source = (IMGV2类)npk2.IMG表[0];

		size = new System.Drawing.Size(source.图块索引表[7].图像宽,source.图块索引表[7].图像高);
		GD.Print("宽："+size.Width+"高："+size.Height);
		
		image_buffer = source.图块数据表[7].源数据;
		image_bitmap = 图块数据类.RGB数组到Bitmap(image_buffer,size,图块数据类.ColorBits.ARGB_8888,true);
		
		
		image.LoadBmpFromBuffer(图块数据类.BitMap到字节数组(image_bitmap,System.Drawing.Imaging.ImageFormat.Bmp));
		
		texture = new ImageTexture();

		texture.SetImage(image);

		sprite.Texture = texture;
		//GD.Print(sprite.Texture.GetSize());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
