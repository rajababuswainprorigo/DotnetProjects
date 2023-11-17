namespace BankNewTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestClass]
		public class BankAccountTests
		{
			[TestMethod]
			public void MakeWithdrawal_WithInsufficientFunds_ThrowsException()
			{
				// Arrange
				var account = new BankAccount("12345", "John Doe", 1000);

				// Act and Assert
				Assert.ThrowsException<InvalidOperationException>(() =>
				{
					account.MakeWithdrawal(1500, DateTime.Now, "Withdrawal with insufficient funds");
				});
			}

			[TestMethod]
			public void GetAccountHistory_ReturnsCorrectFormat()
			{
				// Arrange
				var account = new BankAccount("12345", "John Doe", 1000);
				account.MakeDeposit(500, DateTime.Now, "Deposit 1");
				account.MakeWithdrawal(200, DateTime.Now, "Withdrawal 1");

				// Act
				string history = account.GetAccountHistory();

				// Assert
				StringAssert.Contains(history, "Deposit 1");
				StringAssert.Contains(history, "Withdrawal 1");
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentOutOfRangeException))]
			public void MakeWithdrawal_WithNegativeAmount_ThrowsException()
			{
				// Arrange
				var account = new BankAccount("12345", "John Doe", 1000);

				// Act
				account.MakeWithdrawal(-100, DateTime.Now, "Invalid withdrawal");
			}

			[TestMethod]
			public void MakeMultipleDepositsAndWithdrawals_CalculatesBalanceCorrectly()
			{
				// Arrange
				var account = new BankAccount("12345", "John Doe", 1000);

				// Act
				account.MakeDeposit(200, DateTime.Now, "Deposit 1");
				account.MakeWithdrawal(100, DateTime.Now, "Withdrawal 1");
				account.MakeDeposit(300, DateTime.Now, "Deposit 2");
				account.MakeWithdrawal(50, DateTime.Now, "Withdrawal 2");

				// Assert
				Assert.AreEqual(1350, account.Balance);
			}

			[TestMethod]
			public void MakeWithdrawal_WithZeroAmount_DoesNotChangeBalance()
			{
				// Arrange
				var account = new BankAccount("12345", "John Doe", 1000);

				// Act
				account.MakeWithdrawal(0, DateTime.Now, "Zero amount withdrawal");

				// Assert
				Assert.AreEqual(1000, account.Balance);
			}

		}
	
	}
}