using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Godot;

public class LST类
{
    static Regex regex = new Regex(@"(?<index>\d{1,99})\r\n`(?<path>.*)`");
    public Dictionary<int, string> 数据字典 = new Dictionary<int, string>();
    public LST类(string _source)
    {
        MatchCollection collection = regex.Matches(_source);
        foreach (Match item in collection)
        {
            GroupCollection group = item.Groups;
            数据字典.Add(Convert.ToInt32(group["index"].Value), group["path"].Value);
        }
    }
}