using System;
using System.Collections.Generic;

public class BankAccount
{
	public string AccountNumber { get; }
	public string Owner { get; set; }
	public decimal Balance
	{
		get
		{
			decimal balance = 0;
			foreach (var transaction in allTransactions)
			{
				balance += transaction.Amount;
			}
			return balance;
		}
	}

	private List<Transaction> allTransactions = new List<Transaction>();

	public BankAccount(string accountNumber, string owner, decimal initialBalance)
	{
		AccountNumber = accountNumber;
		Owner = owner;
		MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
	}

	public void MakeDeposit(decimal amount, DateTime date, string note)
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
		}
		var deposit = new Transaction(amount, date, note);
		allTransactions.Add(deposit);
	}

	public void MakeWithdrawal(decimal amount, DateTime date, string note)
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
		}
		if (Balance - amount < 0)
		{
			throw new InvalidOperationException("Not sufficient funds for this withdrawal");
		}
		var withdrawal = new Transaction(-amount, date, note);
		allTransactions.Add(withdrawal);
	}

	public string GetAccountHistory()
	{
		var report = new System.Text.StringBuilder();

		report.AppendLine("Date\t\tAmount\tNote");
		foreach (var transaction in allTransactions)
		{
			report.AppendLine($"{transaction.Date.ToShortDateString()}\t{transaction.Amount}\t{transaction.Notes}");
		}

		return report.ToString();
	}
}

public class Transaction
{
	public decimal Amount { get; }
	public DateTime Date { get; }
	public string Notes { get; }

	public Transaction(decimal amount, DateTime date, string notes)
	{
		Amount = amount;
		Date = date;
		Notes = notes;
	}
}

public class ConsoleHandler
{
	public void DisplayMessage(string message)
	{
		Console.WriteLine(message);
	}

	public string GetInput()
	{
		return Console.ReadLine();
	}
}

class Program
{
	static void Main()
	{
		var consoleHandler = new ConsoleHandler();
		var account = new BankAccount("12345", "Rajababu Swain", 1000);

		consoleHandler.DisplayMessage($"Account {account.AccountNumber} created for {account.Owner} with initial balance of {account.Balance}");

		account.MakeDeposit(50000, DateTime.Now, "Salary deposit");
		consoleHandler.DisplayMessage($"Balance after deposit: {account.Balance}");

		account.MakeWithdrawal(3000, DateTime.Now, "Rent payment");
		consoleHandler.DisplayMessage($"Balance after withdrawal: {account.Balance}");

		consoleHandler.DisplayMessage("Transaction history:");
		consoleHandler.DisplayMessage(account.GetAccountHistory());
	}
}
