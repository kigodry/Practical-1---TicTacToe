using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public enum Winner
    {
        Draw = 0,
        O = -10,
        X = 10,
    }

    public enum Turn
    {
        O = -10,
        X = 10
    }

    internal class GameManager
    {
        public static Board board;

        public static List<Square> squares = new List<Square>();


        public static int turn = 10;

        public static int moveCount = 0;


        public static Symbol? AIPlayer = null;


        public static void InitGame(List<Button> buttons)
        {
            foreach (Button btn in buttons)
            {
                Square s = new Square(btn);

                s.Row = Grid.GetRow(btn);
                s.Column = Grid.GetColumn(btn);

                squares.Add(s);
            }

            board = new Board(3, squares);
        }

        public static void Turn(object sender, RoutedEventArgs e)
        {
            if ((AIPlayer == null) || ((Symbol)turn != AIPlayer))
            {
                Button btn = sender as Button;
                Square clickedSquare = Square.GetByName(squares, btn.Name);

                if (GameManager.IsMoveValid(clickedSquare, btn))
                {
                    GameManager.AddSymbol(clickedSquare, btn);
                    GameManager.AdvanceTurn();

                    clickedSquare = Square.GetByName(squares, btn.Name);

                    Winner? winner = FindWinner(clickedSquare);

                    if (winner != null)
                    {
                        MainWindow.AnnounceWinner(winner);
                        MainWindow.DisableBoard();
                    }
                }
            }
        }

        public static Symbol? SetSymbol(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                switch (s)
                {
                    case "X":
                        return Symbol.X;
                    case "O":
                        return Symbol.O;
                    case "Blank":
                        return Symbol.Blank;
                    default:
                        return null;
                }
            }

            return null;
        }

        public static void SetAIPlayer(Symbol? s)
        {
            AIPlayer = s;
        }

        public static void AITurn()
        {
            if (moveCount < 9)
            {
                
                Move move = Minimax.FindBestMove(board);
                
                
                Square clickedSquare = board.Squares[move.Row, move.Col];
                Button btn = clickedSquare.Button;

                
                if (GameManager.IsMoveValid(clickedSquare, btn))
                {
                    GameManager.AddSymbol(clickedSquare, btn);
                    GameManager.AdvanceTurn();


                    
                    clickedSquare = Square.GetByName(squares, btn.Name);

                    
                    Winner? winner = FindWinner(clickedSquare);

                    
                    if (winner != null)
                    {
                        MainWindow.AnnounceWinner(winner);
                        MainWindow.DisableBoard();
                    }
                }
            }
        }

        public static bool IsMoveValid(Square square, Button btn)
        {
            if (btn.Content == null)
            {
                return true;
            }
            return false;
        }

        public static void AddSymbol(Square s, Button btn)
        {
            s.Symbol = (Symbol)turn; 

            s.Button.Content = (Turn)turn; 
        }

        public static Winner? FindWinner(Square square)
        {
            int row = square.Row;
            int col = square.Column;

            
            for (int i = 0; i <= board.Size; i++)
            {
                if (board.Squares[row,i].Symbol != square.Symbol)
                {
                    break;
                }

                if (i == board.Size - 1)
                    return (Winner)square.Symbol;
            }

            
            for (int i = 0; i < board.Size; i++)
            {
                if (board.Squares[i,col].Symbol != square.Symbol)
                {
                    break;
                }

                if (i == board.Size - 1)
                    return (Winner)square.Symbol;
            }

            
            if (row == col)
            {
                for (int i = 0; i < board.Size; i++)
                {
                    if (board.Squares[i,i].Symbol != square.Symbol)
                    {
                        break;
                    }

                    if (i == board.Size - 1)
                        return (Winner)square.Symbol;
                }
            }

            
            if (row + col == board.Size - 1)
            {
                for (int i = 0; i < board.Size; i++)
                {
                    if (board.Squares[i, ((board.Size - 1) - i)].Symbol != square.Symbol)
                    {
                        break;
                    }

                    if (i == board.Size - 1)
                        return (Winner)square.Symbol;
                }
            }

            
            if (moveCount == (Math.Pow(board.Size,2)))
            {
                return Winner.Draw;
            }

            return null;
        }

        public static int Evaluate(Board board)
        { 

            for (int row = 0; row < board.Size; row++)
            {
                if ((board.Squares[row,0].Symbol == board.Squares[row,1].Symbol &&
                    board.Squares[row,1].Symbol == board.Squares[row,2].Symbol) &&
                    board.Squares[row,0].Symbol != Symbol.Blank)
                {
                    if (board.Squares[row,0].Symbol == Symbol.X)
                    {
                        return (int)Symbol.X;
                    }
                    else if (board.Squares[row,0].Symbol == Symbol.O)
                    {
                        return (int)Symbol.O;
                    }
                }
            }
            
                
            for (int col = 0; col < board.Size; col++)
            {
                if ((board.Squares[0,col].Symbol == board.Squares[1,col].Symbol &&
                    board.Squares[1,col].Symbol == board.Squares[2,col].Symbol) &&
                    board.Squares[0,col].Symbol != Symbol.Blank)
                {
                    if (board.Squares[0, col].Symbol == Symbol.X)
                    {
                        return (int)Symbol.X;
                    }
                    else if (board.Squares[col, 0].Symbol == Symbol.O)
                    {
                        return (int)Symbol.O;
                    }
                }
            }


            if ((board.Squares[0,0].Symbol == board.Squares[1,1].Symbol &&
                board.Squares[1,1].Symbol == board.Squares[2,2].Symbol) &&
                board.Squares[0,0].Symbol != Symbol.Blank)
            {
                if (board.Squares[0,0].Symbol == Symbol.X)
                {
                    return (int)Symbol.X;
                }
                else if (board.Squares[0,0].Symbol == Symbol.O)
                {
                    return (int)Symbol.O;
                }
            }

            
            if ((board.Squares[0, 2].Symbol == board.Squares[1, 1].Symbol &&
                board.Squares[1, 1].Symbol == board.Squares[2, 0].Symbol) &&
                board.Squares[0,2].Symbol != Symbol.Blank)
            {
                if (board.Squares[0,2].Symbol == Symbol.X)
                {
                    return (int)Symbol.X;
                }
                else if (board.Squares[0,2].Symbol == Symbol.O)
                {
                    return (int)Symbol.O;
                }
            }

            return 0; 
        }

        public static Symbol GetOpponent(Symbol s)
        {
            if (s == Symbol.O)
            {
                return Symbol.X;
            }
            else
            {
                return Symbol.O;
            }
        }

        private static void AdvanceTurn()
        {
            if (turn == 10)
            {
                turn = -10;
            }
            else
            {
                turn = 10;
            }

            moveCount++;
        }

        public static void ResetGame()
        {
            squares = new List<Square>();
            turn = 10;
            moveCount = 0;
        }
    }
}
