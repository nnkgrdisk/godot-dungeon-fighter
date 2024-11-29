using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class ANI类
{
	public int frame_max = 0;
	public List<Token类> tokens = new List<Token类>();
	public List<Frame类> frames = new List<Frame类>();
	/// <summary>
	/// token表调试输出
	/// </summary>
	public void TokensDump()
	{
		foreach (var item in tokens)
		{
			GD.Print(item.type.ToString() + ":" + item.value);
		}
	}
	/// <summary>
	/// frame表调试输出
	/// </summary>
	public void FramesDump()
	{
		for (int i = 0; i < frames.Count; i++)
		{
			GD.Print($"{i}:");
			GD.Print($"\tpath:{frames[i].image.path}");
			GD.Print($"\tindex:{frames[i].image.index}");
			GD.Print($"\timage_pos:({frames[i].image_pos.X},{frames[i].image_pos.Y})");
			GD.Print($"\tdelay:{frames[i].delay}");
			for(int t = 0;t < frames[i].damage_boxs.Count;t++)
			{
				GD.Print($"\tdamage box:({frames[i].damage_boxs[t].start.X},{frames[i].damage_boxs[t].start.Y},{frames[i].damage_boxs[t].start.Z}),({frames[i].damage_boxs[t].end.X},{frames[i].damage_boxs[t].end.Y},{frames[i].damage_boxs[t].end.Z})");
			}			
		}
	}
	private string GetString(List<char> _chars)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < _chars.Count; i++)
		{
			if (_chars[i] != '\0')
			{
				sb.Append(_chars[i]);
			}
		}
		return sb.ToString();
	}
	public ANI类(string _source)
	{
		//词法分析变量声明
		List<char> chars = new List<char>();
		bool is_key = false;
		bool is_path = false;
		bool is_number = false;

		//frame list变量声明
		List<Token类> key_caches = new List<Token类>();
		List<Token类> value_caches = new List<Token类>();

		//词法分析
		for (int i = 0; i < _source.Length; i++)
		{
			if (_source[i] == '\t' || _source[i] == '\n')
			{
				if (is_key == true)
				{
					is_key = false;
					chars.Add(_source[i]);//end key token
					tokens.Add(new Token类(Token类.TokenType.key, GetString(chars)));
					chars.Clear();
					continue;
				}
				if (is_number == true)
				{
					is_number = false;
					tokens.Add(new Token类(Token类.TokenType.number, GetString(chars)));//end number token
					chars.Clear();
					continue;
				}
				if (is_path == true)
				{
					is_path = false;
					tokens.Add(new Token类(Token类.TokenType.path, GetString(chars)));//end path token
					chars.Clear();
					continue;
				}
				continue;
			}
			if (_source[i] == '[')
			{
				if (is_number == true)
				{
					is_number = false;
					tokens.Add(new Token类(Token类.TokenType.number, GetString(chars)));//finish number token
					chars.Clear();
					continue;
				}
				else if (is_path == true)
				{
					is_path = false;
					tokens.Add(new Token类(Token类.TokenType.path, GetString(chars)));//finish path token
					chars.Clear();
					continue;
				}
				else if (is_key == false)
				{
					is_key = true;
					chars.Add(_source[i]);//add key token
					continue;
				}
				continue;
			}
			if (_source[i] == ']')
			{
				is_key = false;
				chars.Add(_source[i]);//finish key token
				tokens.Add(new Token类(Token类.TokenType.key, GetString(chars)));
				chars.Clear();
				continue;
			}
			if (_source[i] == '`' || is_path == true)
			{
				is_path = true;
				chars.Add(_source[i]);//add path token
				continue;
			}
			if (is_key == true)
			{
				chars.Add(_source[i]);//add key token
				continue;
			}
			if ((char.IsNumber(_source[i]) || _source[i] == '-' || _source[i] == '.') && is_key == false)
			{
				is_number = true;
				chars.Add(_source[i]);//add number token
				continue;
			}
		}
		//词法分析结束
		//frame list
		foreach (Token类 item in tokens)
		{
			if (item.type == Token类.TokenType.key)
			{
				if (item.value == "[FRAME MAX]")
				{
					//不解析这个key，改用frames统计frame max
					key_caches.Add(item);
					continue;
				}
				if (item.value.Contains("[FRAME"))
				{
					frames.Add(new Frame类());
					continue;
				}
				//else any key
				if (key_caches.Count == 0)
				{
					key_caches.Add(item);
					continue;
				}
				if (key_caches.Count != 0)
				{
					if (key_caches[0].value == "[FRAME MAX]")
					{
						key_caches.RemoveAt(key_caches.Count - 1);
						value_caches.RemoveAt(value_caches.Count - 1);
						key_caches.Add(item);
						continue;
					}
					if (key_caches[0].value == "[IMAGE]")
					{
						frames.Last().image.path = value_caches[0].value;
						frames.Last().image.index = Convert.ToInt32(value_caches[1].value);
						key_caches.Clear();
						value_caches.Clear();
						key_caches.Add(item);
						continue;
					}
					if (key_caches[0].value == "[IMAGE POS]")
					{
						frames.Last().image_pos.X = Convert.ToInt32(value_caches[0].value);
						frames.Last().image_pos.Y = Convert.ToInt32(value_caches[1].value);
						key_caches.Clear();
						value_caches.Clear();
						key_caches.Add(item);
						continue;
					}
					if (key_caches[0].value == "[DELAY]")
					{
						frames.Last().delay = Convert.ToInt32(value_caches[0].value);
						key_caches.Clear();
						value_caches.Clear();
						key_caches.Add(item);
						continue;
					}
					if (key_caches[0].value == "[DAMAGE BOX]")
					{
						(Vector3 start, Vector3 end) v6 = new(new Vector3(), new Vector3());
						v6.start.X = Convert.ToInt32(value_caches[0].value);
						v6.start.Y = Convert.ToInt32(value_caches[1].value);
						v6.start.Z = Convert.ToInt32(value_caches[2].value);
						v6.end.X = Convert.ToInt32(value_caches[3].value);
						v6.end.Y = Convert.ToInt32(value_caches[4].value);
						v6.end.Z = Convert.ToInt32(value_caches[5].value);
						frames.Last().damage_boxs.Add(v6);
						key_caches.Clear();
						value_caches.Clear();
						key_caches.Add(item);
						continue;
					}
				}
			}
			if (item.type == Token类.TokenType.path)
			{
				value_caches.Add(item);
				continue;
			}
			if (item.type == Token类.TokenType.number)
			{
				value_caches.Add(item);
				continue;
			}
		}
		//frame list结束
		frame_max = frames.Count;
	}
	public class Token类
	{
		public TokenType type;
		public string value;
		public enum TokenType
		{
			key,
			path,
			number
		}
		public Token类(TokenType _type, string _value)
		{
			type = _type;
			value = _value;
		}
	}
	public class Frame类
	{
		public (string path, int index) image;
		public Vector2 image_pos;
		public int delay;
		public List<(Vector3 start, Vector3 end)> damage_boxs = new List<(Vector3 start, Vector3 end)>();
	}
}
