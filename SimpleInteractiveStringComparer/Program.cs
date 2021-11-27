using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleInteractiveStringComparer
{
	class Program
	{
		static void Main()
		{
			StringBuilder inputEcho = new();
			Console.WriteLine("Please supply strings to compare, separated with \",\"");
			string impustListStr = Console.ReadLine();
			inputEcho.AppendLine(impustListStr);
			string[] list = impustListStr.Split(",");
			SortedSet<string> res = new(new InteractiveComparer(inputEcho));
			foreach (var item in list)
				res.Add(item);
			int i = 0;
			foreach (var item in res)
				Console.WriteLine($"{(res.Count - 1 - i++) / ((float)res.Count - 1) * 9 + 1:0.0}: {item}");
			File.WriteAllText("./compareresults" + DateTime.Now.ToString("yyyy MM dd HH mm") + ".txt", inputEcho.ToString());
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
		private readonly StringBuilder sb;
		public InteractiveComparer(StringBuilder sb) => this.sb = sb;
		public int Compare(string x, string y)
		{
			Program.Write(ConsoleColor.Green, x, null, " or ", ConsoleColor.Green, y, null, "? (0/1): ");
			string inResponse = Console.ReadLine();
			sb?.AppendLine(inResponse);
			return inResponse == "0" ? -1 : +1;
		}
	}
}