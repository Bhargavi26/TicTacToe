using System;
using System.IO;

namespace Tic_Tac_Toe
{
	public class TicTacToeApplication
	{


		public TicTacToe newGame = new TicTacToe();
		private MonteCarloAI ai = new MonteCarloAI();

		//internal MonteCarloAI Ai { get => ai; set => ai = value; }

		public void run()
		{
			// The game runs here....

			newGame.printIndexBoard(newGame.board);
			newGame.printBoard(newGame.board);

			while (newGame.gameOver(newGame.board).Equals("notOver"))
			{
				// We want to cycle through the turns of user and AI

				// User Input!
				// Getting User's Row
				int userSpot;
				int r = 0;
				do
				{
					Console.WriteLine();
					if (r != 0)
					{
						Console.WriteLine("That spot is already taken! Try again!");
					}
					else
					{
						Console.WriteLine("It's your turn!");
					}
					Console.WriteLine();
					// Getting User's SPot
					Console.WriteLine("Pick a Spot: ");
					userSpot = stringToIntCheck(newGame);
					r++;
				} while /* Get new stuff if the spot is taken */ (newGame.SpotTaken(userSpot, newGame.board));

				// Play User's Turn
				newGame.playTurn(userSpot, TicTacToe.userMarker, newGame.board);

				// Print board
				newGame.printBoard(newGame.board);

				// Check for Winning Condition
				if (!newGame.gameOver(newGame.board).Equals("notOver"))
				{
					newGame.printBoard(newGame.board);
					Console.WriteLine();
					Console.WriteLine(newGame.gameOver(newGame.board));
					return;
				}
				Console.WriteLine();
				Console.WriteLine("It's my turn!");

				// AI Monte Carlo Simulation that gives back array of row and col
				// AI picks where it want to go
				int spot = ai.pickSpot(newGame);

				// AI play turn
				newGame.playTurn(spot, TicTacToe.aiMarker, newGame.board);
				newGame.printBoard(newGame.board);
				Console.WriteLine();
				Console.WriteLine("I picked " + spot + ".");
			}
			Console.WriteLine();
			Console.WriteLine(newGame.gameOver(newGame.board));

		}

		//Check if the input provided for selecting the spot is valid or not
		public int stringToIntCheck(TicTacToe newGame)
		{
			String userInput = Console.ReadLine().Trim();

			int value;

			//if the user input is a number
			if (int.TryParse(userInput, out value))
			{
				//checking if the input is in the range
				while (!newGame.withinRange(int.Parse(userInput)))
				{
					Console.WriteLine("Sorry, that is not a valid spot. Please try again.");
					userInput = Console.ReadLine().Trim();
				}
			}
			//if user input is not a number and it is some other character
			else
			{
				Console.WriteLine("Please enter a number");
				string X = Console.ReadLine().Trim();
				return int.Parse(X);
			}

			return int.Parse(userInput);


		}

		public static void Main(String[] args)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			
			Console.WriteLine("Welcome to Tic Tac Toe!");
			//string path = @"C:\TicTacToe_AI_Selection.txt";
			if (File.Exists(@"TicTacToe_AI_Selection.txt"))
			{
				FileStream fs = File.Open(@"TicTacToe_AI_Selection.txt", FileMode.OpenOrCreate | FileMode.Truncate);
				fs.Close();
			}
			else
			{
				FileStream fs = File.Open(@"TicTacToe_AI_Selection.txt", FileMode.OpenOrCreate);
				fs.Close();
			}
			Console.ResetColor();
			//File.WriteAllText(@"D:\file.txt", string.Empty);
			//FileStream fs = new FileStream(@"D:\file.txt", FileMode.Truncate);

			new TicTacToeApplication().run();
			
			Console.Read();

		}
	}
}
