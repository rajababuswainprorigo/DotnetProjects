using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskManagerApp.Tests
{
	public class Task
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public bool IsCompleted { get; set; }
	}

	public class TaskManager
	{
		private List<Task> tasks = new List<Task>();

		public int AddTask()
		{
			int taskId = tasks.Count + 1;
			Task newTask = new Task { Id = taskId, Description = "Sample Task" };
			tasks.Add(newTask);
			return taskId;
		}

		public void DeleteTask(int taskId)
		{
			tasks.RemoveAll(task => task.Id == taskId);
		}

		public void MarkTaskAsComplete(int taskId)
		{
			Task taskToComplete = tasks.Find(task => task.Id == taskId);
			if (taskToComplete != null)
			{
				taskToComplete.IsCompleted = true;
			}
		}

		public List<Task> GetCompletedTasks()
		{
			return tasks.FindAll(task => task.IsCompleted);
		}

		public int GetTaskCount()
		{
			return tasks.Count;
		}

		public string ViewTasks()
		{
			if (tasks.Count == 0)
			{
				return "No tasks";
			}

			// You can customize the output based on your requirements
			foreach (var task in tasks)
			{
				Console.WriteLine($"Task {task.Id}: {task.Description} - {(task.IsCompleted ? "Completed" : "Not Completed")}");
			}

			return "Tasks displayed";
		}
	}

	[TestClass]
	public class TaskManagerTesting
	{
		[TestMethod]
		public void ValidateAddTask()
		{
			// Arrange
			TaskManager taskManager = new TaskManager();

			// Act
			int taskId = taskManager.AddTask();

			// Assert
			Assert.AreEqual(1, taskManager.GetTaskCount());
		}

		[TestMethod]
		public void ValidateDeleteTask()
		{
			// Arrange
			TaskManager taskManager = new TaskManager();
			int taskId = taskManager.AddTask(); // Add a task to delete later

			// Act
			taskManager.DeleteTask(taskId);

			// Assert
			Assert.AreEqual(0, taskManager.GetTaskCount());
		}

		[TestMethod]
		public void MarkTaskAsComplete_ValidInput_TaskMarkedAsComplete()
		{
			// Arrange
			TaskManager taskManager = new TaskManager();
			int taskId = taskManager.AddTask(); // Add a task to mark as complete later

			// Act
			taskManager.MarkTaskAsComplete(taskId);

			// Assert
			List<Task> completedTasks = taskManager.GetCompletedTasks();
			Assert.AreEqual(1, completedTasks.Count);
			Assert.AreEqual(taskId, completedTasks[0].Id);
		}

		[TestMethod]
		public void ViewTasks_EmptyTaskList_DisplayNoTasksMessage()
		{
			// Arrange
			TaskManager taskManager = new TaskManager();

			// Act
			string result = CaptureConsoleOutput(() => taskManager.ViewTasks());

			// Assert
			StringAssert.Contains("No tasks", result);
		}

		// Helper method to capture console output
		private string CaptureConsoleOutput(Action action)
		{
			using (System.IO.StringWriter sw = new System.IO.StringWriter())
			{
				Console.SetOut(sw);
				action.Invoke();
				return sw.ToString().Trim();
			}
		}
	}
}
