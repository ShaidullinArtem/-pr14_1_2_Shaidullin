using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Task_One(int n)
		{
			Stack<int> stack = new Stack<int>();

			for (int i = 1; i <= n; i++) { stack.Push(i); }

			Console.WriteLine($"Размерность стека: {stack.Count}");
			Console.WriteLine($"Верхний элемент стека: {stack.Peek()}");
			Console.Write("Содержимое стека: ");
			while (stack.Count > 0) { Console.Write($"{stack.Pop()} "); }
			stack.Clear();
			Console.WriteLine($"\nНовая размерность стека: {stack.Count}");
		}
		static string Task_Two_A(string expressionGet)
		{
			using (StreamWriter writer = new StreamWriter("t.txt")) { writer.Write(expressionGet); }
			Console.WriteLine("Выражение записано в файл `t.txt`");

			string expressionSet = File.ReadAllText("t.txt");
			Stack<char> stack = new Stack<char>();

			bool balanced = true;
			int i1 = 0;

			while (i1 < expressionSet.Length && balanced)
			{
				char ch = expressionSet[i1];
				if (ch == '(') { stack.Push(ch); }
				else if (ch == ')')
				{
					if (stack.Count == 0) { balanced = false; }
					else stack.Pop();
				}
				i1++;
			}
			if (balanced && stack.Count == 0) { return "Скобки сбалансированы"; }
			else if (stack.Count == 0) { return $"Возможно лишняя ) скобка в позиции {i1}"; }
			else { return $"Возможна лишняя ( скобка в позиции {expressionSet.Length - stack.Count}"; }
		}
		static string Task_Two_B(string expression)
		{
			Stack<char> stack1 = new Stack<char>();

			for (int i = 0; i < expression.Length; i++)
			{
				char ch = expression[i];
				if (ch == '(') { stack1.Push(ch); }
				else if (ch == ')')
				{
					if (stack1.Count > 0 && stack1.Peek() == '(') { stack1.Pop(); }
					else { stack1.Push(ch); }
				}
			}

			// Удаление лишних скобок в начале и конце
			while (expression.Length > 0 && expression[0] == ')')
			{
				expression = expression.Remove(0, 1);
			}
			while (expression.Length > 0 && expression[expression.Length - 1] == '(')
			{
				expression = expression.Remove(expression.Length - 1, 1);
			}

			// Удаление лишних скобок в середине
			while (stack1.Count > 0)
			{
				char ch = stack1.Pop();
				if (ch == '(') { expression += ')'; }
				else if (ch == ')') { expression = expression.Remove(expression.LastIndexOf(')'), 1); }
			}

			File.WriteAllText("t1.txt", expression);
			return $"Новое выражение: {expression} - записано в файл.";
		}

		static void Main(string[] args)
		{
			// задание 1
			try
			{
				Console.Write("n = ");
				int n = int.Parse(Console.ReadLine());
				Task_One(n);
			}
			catch (Exception ex) { Console.WriteLine(ex); }

			// задание 2а
			Console.WriteLine("Введите математическое выражение: ");
			string expressionGet = Console.ReadLine();
			Console.WriteLine(Task_Two_A(expressionGet));
			// задание 2б
			string expression = File.ReadAllText("t.txt");
			Console.WriteLine(Task_Two_B(expression));


			Console.ReadKey();
		}
	}
}
