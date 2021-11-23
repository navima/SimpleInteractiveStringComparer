using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleInteractiveStringComparer
{
	class Program
	{
		static StringBuilder @string = new();
		static void Main(string[] args)
		{
			Console.WriteLine($"Please supply strings to compare, separated with \",\"");
			string listStr = Console.ReadLine();
			@string.AppendLine(listStr);
			string[] list = listStr.Split(",");
			SortedSet<string> res = new(new InteractiveComparer(@string));
			foreach (var item in list)
			{
				res.Add(item);
			}
			int i = 0;
			foreach (var item in res)
				Console.WriteLine($"{(res.Count - 1 - i++) / ((float)res.Count - 1) * 9 + 1:0.0}: {item}");
			File.WriteAllText("./compareresults" + DateTime.Now.ToString("yyyy MM dd HH mm")+".txt", @string.ToString());
		}
		public static void Write(params object[] oo)
		{
			foreach (var o in oo)
				switch (o)
				{
					case null:
						Console.ResetColor();
						break;
					case ConsoleColor oCol:
						Console.ForegroundColor = oCol;
						break;
					default:
						Console.Write(o.ToString());
						break;
				}
		}
	}

	internal class InteractiveComparer : IComparer<string>
	{
		static StringBuilder @string = new();
		public InteractiveComparer(StringBuilder strB)
		{
			@string = strB;
		}
		public int Compare(string x, string y)
		{
			Program.Write(ConsoleColor.Green, x, null, " or ", ConsoleColor.Green, y, null, "? (0/1): ");
			string inResponse = Console.ReadLine();
			@string.AppendLine(inResponse);
			return inResponse == "0" ? -1 : +1;
		}
	}

}