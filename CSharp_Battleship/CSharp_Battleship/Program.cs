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
            public string name;

            public int[,] board = new int[10, 10];
            public int[,] guessBoard = new int[10, 10];

            public Point[] carrier = new Point[5];
            public Point[] battleship = new Point[4];
            public Point[] destroyer = new Point[3];
            public Point[] submarine = new Point[3];
            public Point[] patrolBoat = new Point[2];

            public Player(string name)
            {
                this.name = name;
            }

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

        static public Point[] GetShip(string ship, int shipSize)
        {
            var ret = new Point[shipSize];
            var done = false;
            while(!done)
            {
                Console.WriteLine("Enter start point of " + ship + ":");
                string start = Console.ReadLine();
                Console.WriteLine("Enter end point of " + ship + ":");
                string end = Console.ReadLine();
                var startArr = start.Split(',');
                var endArr = end.Split(',');
                if (start.Length != 3 || end.Length != 3)
                {
                    Console.WriteLine("Sorry that ship was invalid, please try again");
                    continue;
                }
                int startArr1;
                int startArr2;
                int endArr1;
                int endArr2;
                try
                {
                    int.TryParse(startArr[0], out startArr1);
                    Console.WriteLine(startArr1);
                    int.TryParse(startArr[1], out startArr2);
                    Console.WriteLine(startArr2);
                    if (startArr1 < 0 || startArr1 > 9 || startArr2 < 0 || startArr2 > 9)
                    {
                        Console.WriteLine("Sorry that ship was invalid, please try again");
                        continue;
                    }
                    int.TryParse(endArr[0], out endArr1);
                    Console.WriteLine(endArr1);
                    int.TryParse(endArr[1], out endArr2);
                    Console.WriteLine(endArr2);
                    if (endArr1 < 0 || endArr1 > 9 || endArr2 < 0 || endArr2 > 9)
                    {
                        Console.WriteLine("Sorry that ship was invalid, please try again");
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Sorry that ship was invalid, please try again");
                    continue;
                }

                Console.WriteLine(Math.Abs(startArr1 - endArr1));
                Console.WriteLine(Math.Abs(startArr2 - endArr2));
                if (Math.Abs(startArr1 - endArr1) == 0 && Math.Abs(startArr2 - endArr2) == shipSize-1) 
                {
                    if (startArr2 > endArr2)
                    {
                        for (int i = endArr2; i <= startArr2; i++)
                        {
                            ret[i - endArr2] = (new Point { X = startArr1, Y = i });
                        }
                        return ret;
                    }
                    for (int i = startArr2; i <= endArr2; i++)
                    {
                        ret[i - startArr2] = (new Point { X = startArr1, Y = i });
                    }
                    return ret;

                }
                else if (Math.Abs(startArr1 - endArr1) == shipSize-1 && Math.Abs(startArr2 - endArr2) == 0)
                {
                    if (startArr1 > endArr1)
                    {
                        for (int i = endArr1; i <= startArr1; i++)
                        {
                            ret[i - endArr1] = (new Point { X = i, Y = startArr2 });
                        }
                        return ret;
                    }
                    for (int i = startArr1; i <= endArr1; i++)
                    {
                        ret[i - startArr1] = (new Point { X = i, Y = startArr2 });
                    }
                    return ret;
                }
                Console.WriteLine("Sorry that ship was invalid, please try again");

            }
            return ret;

        }

        static public Player populateShips(Player player)
        {


            return player;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first player's name:");
            string p1Name = Console.ReadLine();
            Console.WriteLine("Enter the seccond player's name:");
            string p2Name = Console.ReadLine();

            var player1 = new Player(p1Name);
            var player2 = new Player(p2Name);
            Console.WriteLine(player1.guessBoard[0,1]);

            var test = GetShip("test", 4);
            foreach (var item in test)
            {
                Console.WriteLine(item.X + "," + item.Y);
            }
            
            Console.WriteLine();

        }
    }
}
