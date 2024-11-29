using System;
using Godot;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public static class ExDump
{
    public static void Dump(this Dictionary<int, string> _dic)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < _dic.Count; i++)
        {
            KeyValuePair<int, string> item = _dic.ElementAt(i);
            sb.Append($"{item.Key.ToString()} : {item.Value.ToString()}");
            if (i < _dic.Count - 1) { sb.Append(","); }
        }
        sb.Append("]");
        GD.Print(sb.ToString());
    }
}