﻿using System;
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

            public Point(int X, int Y)
                { this.X = X; this.Y = Y; }

            //create a string to point thing

        }

        public class Ship
        {
            public Point[] points;
            public string name;

            public int health;
            public int hits;
            public Ship(Point[] points, string name)
            {
                this.points = points;
                this.name = name;
                health = points.Length;
                hits = 0;
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

            player.battleship.points = fillShip(player.battleship.name, player.battleship.health, player.name);
            player.board = putPointsOnGrid(player.board, player.battleship.points, "B");
            printBoard(player.board);

            player.destroyer.points = fillShip(player.destroyer.name, player.destroyer.health, player.name);
            player.board = putPointsOnGrid(player.board, player.destroyer.points, "D");
            printBoard(player.board);

            player.submarine.points = fillShip(player.submarine.name, player.submarine.health, player.name);
            player.board = putPointsOnGrid(player.board, player.submarine.points, "S");
            printBoard(player.board);

            player.patrolBoat.points = fillShip(player.patrolBoat.name, player.patrolBoat.health, player.name);
            player.board = putPointsOnGrid(player.board, player.patrolBoat.points, "P");
            printBoard(player.board);
            Console.Clear();
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

            Console.WriteLine(activePlayer.name + ", please enter a guess:");
            string shot = Console.ReadLine();
            return true;
        }

        static public bool checkHit(Player player, Point guess)
        {
            //if player.board[guess.X, guess.y] != "~" player
                    return true;
        }

        static public bool checkLoss(Player player)
        {
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first player's name:");
            string p1Name = Console.ReadLine();
            Console.WriteLine("Enter the seccond player's name:");
            string p2Name = Console.ReadLine();

            var player1 = populateShips(new Player(p1Name));
            var player2 = populateShips(new Player(p2Name));



            Console.ReadLine();

        }
    }
}
