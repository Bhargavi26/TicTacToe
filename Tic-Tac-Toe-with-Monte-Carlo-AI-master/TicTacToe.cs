using System;

namespace Tic_Tac_Toe
{
	public class TicTacToe 
	{


	public  char[] board;
	public static  char userMarker = 'X';
	public static  char aiMarker = 'O';
	public char winner;

		public static char UserMarker
		{
			get
			{
				return userMarker;
			}
			set
			{
				userMarker = value;
			}
		}

		public TicTacToe()
	{
		board = TicTacToe.setBoard();
		// board[0].length for length of row
		// board.length for length of col
				this.winner = '-';
	}

	public static char[] setBoard()
	{
		char[] board = new char[9];
		for (int i = 0; i < board.Length; i++)
		{
			board[i] = '-';
		}
		return board;
	}

	// using a seperate board for simulations  
	public char[] setSimulationBoard()
	{
		char[] simulationBoard = new char[9];
		for (int i = 0; i < board.Length; i++)
		{
			switch (board[i])
			{
				case '-':
					simulationBoard[i] = '-';
					break;
				case 'X':
					simulationBoard[i] = UserMarker;
					break;
				case 'O':
					simulationBoard[i] = aiMarker;
					break;
			}

		}
		this.winner = '-';
		return simulationBoard;
	}

	public void resetForBackToGamePlay()
	{
		this.winner = '-';
	}

	// TRUE = user
	// FALSE = AI

	// Everything takes in a board now so that we can test simulation boards
	// and the true boards easier.

	public void playTurn(int spot, char marker, char[] board)
	{
		if (spot >= 1 && spot < board.Length + 1)
		{
			board[spot - 1] = marker;
		}
	}

	// check if spot is in range
	public bool withinRange(int number)
	{
		return number > 0 && number < board.Length + 1;
	}

	// check if spot isn't taken
	public bool SpotTaken(int spot, char[] board)
	{
		return board[spot - 1] != '-';
	}

	public void printBoard(char[] board)
	{
			// Attempting to create...
			// | - | - | -
			// ------------
			// | - | - | -
			// ------------
			// | - | - | -


			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine();
		for (int i = 0; i < board.Length; i++)
		{
			if (i % 3 == 0 && i != 0)
			{
					Console.WriteLine();
					Console.WriteLine("-------------");
			}
				Console.Write(" | " + board[i]);

		}
			Console.WriteLine();
			Console.ResetColor();
		}
		

	public void printIndexBoard(char[] board)
	{
			Console.WriteLine();
		for (int i = 0; i < board.Length; i++)
		{
			if (i % 3 == 0 && i != 0)
			{
					Console.WriteLine();
					Console.WriteLine("-------------");
			}
				Console.Write(" | " + (i + 1));

		}
			Console.WriteLine();
	}


	public bool isThereAWinner(char[] board)
	{
		// winning conditions
		bool diagonalsAndMiddles = (rightDi(board) || leftDi(board) || middleRow(board) || secondCol(board)) && board[4] != '-';
		bool topAndFirst = (topRow(board) || firstCol(board)) && board[0] != '-';
		bool bottomAndThird = (bottomRow(board) || thirdCol(board)) && board[8] != '-';
		if (diagonalsAndMiddles)
		{
			this.winner = board[4];
		}
		else if (topAndFirst)
		{
			this.winner = board[0];
		}
		else if (bottomAndThird)
		{
			this.winner = board[8];
		}

		return diagonalsAndMiddles || topAndFirst || bottomAndThird;

	}

	public bool topRow(char[] board)
	{
		return board[0] == board[1] && board[1] == board[2];
	}

	public bool middleRow(char[] board)
	{
		return board[3] == board[4] && board[4] == board[5];
	}

	public bool bottomRow(char[] board)
	{
		return board[6] == board[7] && board[7] == board[8];
	}

	public bool firstCol(char[] board)
	{
		return board[0] == board[3] && board[3] == board[6];
	}

	public bool secondCol(char[] board)
	{
		return board[1] == board[4] && board[4] == board[7];
	}

	public bool thirdCol(char[] board)
	{
		return board[2] == board[5] && board[5] == board[8];
	}

	public bool rightDi(char[] board)
	{
		return board[0] == board[4] && board[4] == board[8];
	}

	public bool leftDi(char[] board)
	{
		return board[2] == board[4] && board[4] == board[6];
	}


	public bool isTheBoardFilled(char[] board)
	{
		for (int i = 0; i < board.Length; i++)
		{
			if (board[i] == '-')
			{
				return false;
			}
		}
		return true;
	}

	public String gameOver(char[] board)
	{
		bool didSomeoneWin = isThereAWinner(board);
		if (didSomeoneWin)
		{
			return "We have a winner! The winner is '" + this.winner + "'s";
		}
		else if (isTheBoardFilled(board))
		{
			this.winner = '-';
			return "Draw: Game Over!";
		}
		else
		{
			return "notOver";
		}
	}


}

}
