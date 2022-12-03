using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: add restriction when placing boats so they cant overlap
//fix strings and make them better
//clean up code
namespace CSharp_Battleship
{
    internal class Program
    {

        public class Point
        {
            public int X;
            public int Y;

            public Point(int X, int Y)
                { this.X = X; this.Y = Y; }

            //create a string to point thing

        }

        public class Ship
        {
            public Point[] points;
            public string name;

            public int health;
            public Ship(Point[] points, string name)
            {
                this.points = points;
                this.name = name;
                health = points.Length;
            }
        }

        public class Player
        {
            public string name;

            public string[,] board = new string[10, 10];
            public string[,] guessBoard = new string[10, 10];

            public Ship carrier = new Ship(new Point[5], "carrier");
            public Ship battleship = new Ship(new Point[4], "battleship");
            public Ship destroyer = new Ship(new Point[3], "destroyer");
            public Ship submarine = new Ship(new Point[3], "submarine");
            public Ship patrolBoat = new Ship(new Point[2], "patrol boat");

            public int shipsLeft = 5;

            public Player(string name)
            {
                this.name = name;
            }

        }

        static public string[,] ClearBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "~";
                }
            }
            return board;
        }

        static public Point[] fillShip(string ship, int shipSize, string playerName)
        {
            var ret = new Point[shipSize];
            var done = false;
            while(!done)
            {
                Console.WriteLine("Enter start point of "+ playerName + "'s " + ship + "(Length = " + shipSize + "):");
                string start = Console.ReadLine();
                Console.WriteLine("Enter end point of " + playerName + "'s " + ship + "(Length = " + shipSize + "):");
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
                    int.TryParse(startArr[1], out startArr2);
                    if (startArr1 < 0 || startArr1 > 9 || startArr2 < 0 || startArr2 > 9)
                    {
                        Console.WriteLine("Sorry that ship was invalid, please try again");
                        continue;
                    }
                    int.TryParse(endArr[0], out endArr1);
                    int.TryParse(endArr[1], out endArr2);
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

                //Console.WriteLine(Math.Abs(startArr1 - endArr1));
                //Console.WriteLine(Math.Abs(startArr2 - endArr2));
                if (Math.Abs(startArr1 - endArr1) == 0 && Math.Abs(startArr2 - endArr2) == shipSize-1) 
                {
                    if (startArr2 > endArr2)
                    {
                        for (int i = endArr2; i <= startArr2; i++)
                        {
                            ret[i - endArr2] = (new Point(startArr1, i));
                        }
                        return ret;
                    }
                    for (int i = startArr2; i <= endArr2; i++)
                    {
                        ret[i - startArr2] = (new Point(startArr1, i));
                    }
                    return ret;

                }
                else if (Math.Abs(startArr1 - endArr1) == shipSize-1 && Math.Abs(startArr2 - endArr2) == 0)
                {
                    if (startArr1 > endArr1)
                    {
                        for (int i = endArr1; i <= startArr1; i++)
                        {
                            ret[i - endArr1] = (new Point(i, startArr2));
                        }
                        return ret;
                    }
                    for (int i = startArr1; i <= endArr1; i++)
                    {
                        ret[i - startArr1] = (new Point(i, startArr2));
                    }
                    return ret;
                }
                Console.WriteLine("Sorry that ship was invalid, please try again");

            }
            return ret;

        }

        static public string[,] putPointsOnGrid(string[,] grid, Point[] points, string name)
        {
            foreach (var point in points)
            {
                grid[point.X, point.Y] = name;
            }
            return grid;
        }

        static public Player populateShips(Player player)
        {
            player.board = ClearBoard(player.board);
            player.guessBoard = ClearBoard(player.guessBoard);

            player.carrier.points = fillShip(player.carrier.name, player.carrier.health, player.name);
            player.board = putPointsOnGrid(player.board, player.carrier.points, "C");
            printBoard(player.board);

            //player.battleship.points = fillShip(player.battleship.name, player.battleship.health, player.name);
            //player.board = putPointsOnGrid(player.board, player.battleship.points, "B");
            //printBoard(player.board);

            //player.destroyer.points = fillShip(player.destroyer.name, player.destroyer.health, player.name);
            //player.board = putPointsOnGrid(player.board, player.destroyer.points, "D");
            //printBoard(player.board);

            //player.submarine.points = fillShip(player.submarine.name, player.submarine.health, player.name);
            //player.board = putPointsOnGrid(player.board, player.submarine.points, "S");
            //printBoard(player.board);

            //player.patrolBoat.points = fillShip(player.patrolBoat.name, player.patrolBoat.health, player.name);
            //player.board = putPointsOnGrid(player.board, player.patrolBoat.points, "P");
            //printBoard(player.board);
            //Console.Clear();
            return player;
        }

        static public void printBoard(string[,] board)
        {
            for (int i = 0; i < Math.Sqrt(board.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(board.Length); j++)
                {
                    Console.Write(board[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        static public bool takeTurn(Player activePlayer, Player nonActivePlayer)
        {
            Console.WriteLine(activePlayer.name + "'s guesses:");
            printBoard(activePlayer.guessBoard);
            Console.WriteLine(activePlayer.name + "'s board:");
            printBoard(activePlayer.board);

            var done = false;
            int shotPoint1 = 0;
            int shotPoint2 = 0;
            while (!done)
            {
                Console.WriteLine(activePlayer.name + ", please enter a guess:");
                var shot = Console.ReadLine();
                var shotArr = shot.Split(',');
                if (shotArr.Length != 2 || shot.Length != 3)
                {
                    Console.WriteLine("Sorry that shot was invalid, please try again");
                    continue;
                }

                try
                {
                    int.TryParse(shotArr[0], out shotPoint1);
                    int.TryParse(shotArr[1], out shotPoint2);
                    if (shotPoint1 < 0 || shotPoint1 > 9 || shotPoint2 < 0 || shotPoint2 > 9)
                    {
                        Console.WriteLine("Sorry that shot was invalid, please try again");
                        continue;
                    }
                    done = true;
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Sorry that shot was invalid, please try again");
                    continue;
                }
            }

            var shotPoint = new Point(shotPoint1, shotPoint2);
            var shotHit = checkHit(nonActivePlayer, shotPoint);
            if(shotHit)
            {
                activePlayer.guessBoard[shotPoint.X, shotPoint.Y] = "H";
                nonActivePlayer.board[shotPoint.X, shotPoint.Y] = "X";
            }
            else
            {
                activePlayer.guessBoard[shotPoint.X, shotPoint.Y] = "M";
            }
            return shotHit;
        }

        static public bool checkHit(Player player, Point guess)
        {
            var shot = player.board[guess.X, guess.Y];
            while (true)
            {
                switch (shot)
                {
                    case "~":
                        return false;
                    case "C":
                        player.carrier.health -= 1;
                        if (player.carrier.health == 0)
                        {
                            player.shipsLeft -= 1;
                        }
                        return true;
                    case "B":
                        player.battleship.health -= 1;
                        if (player.battleship.health == 0)
                        {
                            player.shipsLeft -= 1;
                        }
                        return true;
                    case "D":
                        player.destroyer.health -= 1;
                        if (player.destroyer.health == 0)
                        {
                            player.shipsLeft -= 1;
                        }
                        return true;
                    case "S":
                        player.submarine.health -= 1;
                        if (player.submarine.health == 0)
                        {
                            player.shipsLeft -= 1;
                        }
                        return true;
                    case "P":
                        player.patrolBoat.health -= 1;
                        if(player.patrolBoat.health == 0)
                        {
                            player.shipsLeft -= 1;
                        }
                        return true;

                    default:
                        Console.WriteLine("Invalid shot please try again");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first player's name:");
            string p1Name = Console.ReadLine();
            Console.WriteLine("Enter the seccond player's name:");
            string p2Name = Console.ReadLine();

            var player1 = populateShips(new Player(p1Name));
            var player2 = populateShips(new Player(p2Name));

            var activePlayer = player1;
            var nonActivePlayer = player2;
            Player holder;
            while (player1.shipsLeft != 0 && player2.shipsLeft != 0)
            {
                var same = takeTurn(activePlayer, nonActivePlayer);
                if (!same)
                {
                    Console.Clear();
                    printBoard(activePlayer.guessBoard);
                    Console.WriteLine("Sorry that guess was wrong please give control to" + nonActivePlayer.name + "then press any key");
                    Console.ReadLine();
                    Console.Clear();
                    holder = activePlayer;
                    activePlayer = nonActivePlayer;
                    nonActivePlayer = holder;
                }
            }
            if (player1.shipsLeft == 0)
            {
                Console.WriteLine("Congrats" + player2.name + ", you won!");
            }
            else
            {
                Console.WriteLine("Congrats" + player1.name + ", you won!");
            }

            Console.ReadLine();

        }
    }
}
