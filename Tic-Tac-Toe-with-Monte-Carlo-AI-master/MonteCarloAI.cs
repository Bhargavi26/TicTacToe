
using System;
using System.IO;


namespace Tic_Tac_Toe
{
	class MonteCarloAI
	{

		public int boardSize = 9;
		public int lengthOfRow = 3;
		public int lengthOfCol = 3;
		public Random rand = new Random();

		public int numberOfSimulations = 1000000;
		//^ the lower == computer more dumb
		public char NONE = '-';
		public char USER = 'X';
		public char AI = 'O';

		// Because we want the winners to stand out from the losers
		public int WIN_PTS = 100;
		public int LOSE_PTS = -1;
		public int DRAW_PTS = 0;
		public int MIN_PTS = -1000000000;
		public int NO_SPOT = -100;

		public MonteCarloAI()
		{

		}

		
		public int pickSpot(TicTacToe game)
		{
			// add one because when we play turns, we want it to look to be 1-9 based
			return this.runRandomSimulations(game) + 1;
		}

		public int chooseRandomSpot(char[] board)
		{
			int[] spotAvaliable = new int[9];
			int index = 0;
			for (int i = 0; i < boardSize; i++)
			{
				if (board[i] == this.NONE)
				{
					spotAvaliable[index++] = i;
				}
			}

			if (index == 0)
			{
				//System.out.println("no spot avaliable");
				return this.NO_SPOT; // no spot avaliable
			}
			// System.out.println("Spots avaliable" + index);

			return spotAvaliable[Rand() % index];
		}

		public int Rand()
		{
			return Math.Abs(rand.Next());
		}

		// KEY TO THE SIMULATIONS/AI:
		public int runRandomSimulations(TicTacToe game)
		{
			int[] winPoints = new int[9];
			int[] goodSpots = new int[9];
			char[] tempBoard = game.setSimulationBoard();
			int goodSpotIndex = 0;
			bool isThereAFreeSpace = false;
			int firstMove;
			// Where is there a good space? Setting it up.
			for (int i = 0; i < boardSize; i++)
			{
				if (tempBoard[i] == '-')
				{
					isThereAFreeSpace = true;
					winPoints[i] = 0;
				}
				else
				{
					// So we don't pick unavaliable spots
					winPoints[i] = this.MIN_PTS;
				}

			}

			// JUST IN CASE
			if (!isThereAFreeSpace)
			{
				throw new SystemException("There aren't any free spaces!");
			}



			//play it out yoo...
			for (int j = 0; j < this.numberOfSimulations; j++)
			{
				// set to -1 so we can track what the first move is
				firstMove = -1;
				int flipper = 0;
				// Play a game...
				while (game.gameOver(tempBoard).Equals("notOver"))
				{
					// find a spot
					int spot = this.chooseRandomSpot(tempBoard);

					if (spot == this.NO_SPOT /* meaning there was no spot avaliable */)
					{
						game.winner = this.NONE;
						// we are done with this game. 
						break;
					}

					// The computer is basically playing the user and the comp
					// But we just take one side.
					char marker = (flipper % 2 == 0) ? TicTacToe.aiMarker : TicTacToe.userMarker;
					// the spot
					// add one because when we play turns, we want it to look to be 1-9 based
					game.playTurn(spot + 1, marker, tempBoard);

					// take note if it's the first position
					if (firstMove == -1)
					{
						firstMove = spot;
					}
					flipper++;
				}

				// Who won with this first move?
				// Dealing points appropriately
				if (game.winner != this.NONE)
				{
					if (game.winner == this.AI)
					{
						winPoints[firstMove] += this.WIN_PTS;
					}
					else
					{
						winPoints[firstMove] += this.LOSE_PTS;
					}
				}
				else
				{
					winPoints[firstMove] += this.DRAW_PTS;
				}
				// Reseting the game for the next simulation
				tempBoard = game.setSimulationBoard();
			}

			game.resetForBackToGamePlay();
			// After running through all of stimulations...
			
			string path = @"TicTacToe_AI_Selection.txt";
			int max = this.MIN_PTS;
			// Finding spot with max points = Where AI should go
			using (StreamWriter sw = File.AppendText(path))
			{
				Console.WriteLine();
				Console.WriteLine("Points Per Spot: ");
				for (int k = 0; k < this.boardSize; k++)
				{
					if (max < winPoints[k])
					{
						max = winPoints[k];
					}
					sw.WriteLine((k + 1) + " : " + winPoints[k]);
				}
			



			// Could have multiple good candidates
			// Creating choices for candidates
			
				for (int b = 0; b < this.boardSize; b++)
				{
					if (winPoints[b] == max)
					{
						goodSpots[goodSpotIndex++] = b;
						sw.WriteLine("Best Spot = " + (b + 1));
						sw.WriteLine("");
					}
				}

				
				
			}

			Console.WriteLine();
			
			// Randomly choose a good one
			return goodSpots[Rand() % goodSpotIndex];



		}

	}

}
