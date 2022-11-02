using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Battleship
{
    internal class Program
    {

        public class Point
        {
            public int X;
            public int Y;
        }

        public class Player
        {
            public int[,] board = new int[10, 10];
            public int[,] guessBoard = new int[10, 10];

            public Point[] carrier = new Point[5];
            public Point[] battleship = new Point[4];
            public Point[] destroyer = new Point[3];
            public Point[] submarine = new Point[3];
            public Point[] patrolBoat = new Point[2];

        }

        static public int[,] ClearBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 0;
                }
            }
            return board;
        }
        
        static public bool VerifyShip(Point[] ship)
        {
            for (int i = 0; i < ship.Length-2; i++)
            {
                if(ship[i].X != ship[i + 1].X)
                {
                    return false;
                }
            }
            for (int j = 0; j < ship.Length - 2; j++)
            {
                if (ship[j].Y != ship[j + 1].Y)
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            var player1 = new Player();
            var player2 = new Player();

        }
    }
}
