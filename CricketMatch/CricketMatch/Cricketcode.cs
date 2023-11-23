
using System;
namespace CricketMatch
{ }
public class CricketMatch1
{
	private readonly int overs;
	private int runsRequired;
	private int runsScored;
	private int ballsPlayed;
	private int wicketsLost;
	private string battingTeam;
	private string bowlingTeam;

	public CricketMatch1(int overs, string battingTeam, string bowlingTeam)
	{
		this.overs = overs;
		this.battingTeam = battingTeam;
		this.bowlingTeam = bowlingTeam;
	}

	public void SimulateMatch()
	{
		Console.WriteLine($"World Cup Final: {battingTeam} vs {bowlingTeam}");
		Console.WriteLine($"Total Overs: {overs}");

		// Perform Toss
		Toss();

		Console.WriteLine($"{battingTeam} needs to chase the target to win the World Cup!");

		SetTargetScore();

		while (ballsPlayed < overs * 6 && runsRequired >= 0 && wicketsLost < 10)
		{
			PlayBall();
		}

		DisplayResult();
	}

	private void Toss()
	{
		Console.WriteLine("Tossing the coin...");
		Random random = new Random();
		bool battingTeamWinsToss = random.Next(2) == 0;

		if (battingTeamWinsToss)
		{
			Console.WriteLine($"{battingTeam} wins the toss and chooses to bat.");
		}
		else
		{
			Console.WriteLine($"{bowlingTeam} wins the toss and chooses to bowl.");
			// Swap batting and bowling teams
			string temp = battingTeam;
			battingTeam = bowlingTeam;
			bowlingTeam = temp;
		}
	}

	private void SetTargetScore()
	{
		Random random = new Random();
		// Set a random target score between 200 and 300 for the World Cup Final
		runsRequired = random.Next(200, 301);
		Console.WriteLine($"{bowlingTeam} has set a target of {runsRequired} runs for {battingTeam} to win the World Cup!");
	}

	private void PlayBall()
	{
		Random random = new Random();
		int runsThisBall = random.Next(0, 7);
		ballsPlayed++;

		if (runsThisBall == 0)
		{
			Console.WriteLine($"Ball {ballsPlayed}: Dot Ball");
		}
		else
		{
			Console.WriteLine($"Ball {ballsPlayed}: {runsThisBall} runs scored");
		}

		runsScored += runsThisBall;
		runsRequired -= runsThisBall;

		if (runsThisBall == 0 || random.NextDouble() < 0.2)
		{
			wicketsLost++;
			Console.WriteLine($"Wicket! {wicketsLost} wickets down");
		}

		DisplayScore();
	}

	private void DisplayScore()
	{
		Console.WriteLine($"Score: {runsScored}/{wicketsLost} in {ballsPlayed} balls");
		Console.WriteLine($"{battingTeam} requires {runsRequired} runs to win the World Cup");
	}

	private void DisplayResult()
	{
		Console.WriteLine("\nWorld Cup Final Match Over!");
		if (runsScored >= runsRequired)
		{
			Console.WriteLine($"Congratulations! {battingTeam} won the World Cup by {10 - wicketsLost} wickets.");
		}
		else
		{
			Console.WriteLine($"Sorry! {battingTeam} lost the World Cup Final by {runsRequired - runsScored} runs.");
		}
	}
}

class cric
{
	static void Main()
	{
		// Creating a World Cup Final match between India and Australia
		CricketMatch1 worldCupFinal = new CricketMatch1(50, "IND", "AUS");
		worldCupFinal.SimulateMatch();
	}
}
