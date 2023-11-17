using System;
using System.Collections.Generic;

namespace TaskManagerApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TaskManager taskManager = new TaskManager();

			while (true)
			{
				DisplayMenu();

				int choice = GetChoice();

				switch (choice)
				{
					case 1:
						taskManager.AddTask();
						break;
					case 2:
						taskManager.ViewTasks();
						break;
					case 3:
						taskManager.MarkTaskAsComplete();
						break;
					case 4:
						taskManager.DeleteTask();
						break;
					case 5:
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
			}
		}

		static void DisplayMenu()
		{
			Console.WriteLine("Task Manager for Rajababu Swain");
			Console.WriteLine("1. Add Task");
			Console.WriteLine("2. View Tasks");
			Console.WriteLine("3. Mark Task as Complete");
			Console.WriteLine("4. Delete Task");
			Console.WriteLine("5. Exit");
		}

		static int GetChoice()
		{
			Console.Write("Enter your choice: ");
			if (int.TryParse(Console.ReadLine(), out int choice))
			{
				return choice;
			}
			return 0;
		}
	}

	internal class TaskManager
	{
		private List<string> tasks;

		public TaskManager()
		{
			tasks = new List<string>();
		}

		internal void AddTask()
		{
			Console.Write("Enter task: ");
			string task = 
				Console.ReadLine();
			//tasks.Add(task);
			Console.WriteLine("Task added successfully.");
		}

		internal void DeleteTask()
		{
			ViewTasks();
			Console.Write("Enter the task number to delete: ");
			if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
			{
				tasks.RemoveAt(taskNumber - 1);
				Console.WriteLine("Task deleted successfully.");
			}
			else
			{
				Console.WriteLine("Invalid task number.");
			}
		}

		internal void MarkTaskAsComplete()
		{
			ViewTasks();
			Console.Write("Enter the task number to mark as complete: ");
			if (int.TryParse(Console.ReadLine(), out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
			{
				Console.WriteLine($"Task '{tasks[taskNumber - 1]}' marked as complete.");
			}
			else
			{
				Console.WriteLine("Invalid task number.");
			}
		}

		internal void ViewTasks()
		{
			Console.WriteLine("Tasks:");
			for (int i = 0; i < tasks.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {tasks[i]}");
			}
		}
	}
}
