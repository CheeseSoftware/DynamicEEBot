using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicEEBot
{
    public struct Position
    {
        public int x;
        public int y;
        public int cost;

        public Position(int x, int y, int cost)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
        }
    }

    class Square
    {
        public int x;
        public int y;
        public int G;
        public int H;
        public Square parent;
        public int F
        {
            get
            {
                return G + H;
            }
        }
        public Square(int x, int y, int G, int H, Square parent)
        {
            this.x = x;
            this.y = y;
            this.G = H;
            this.H = H;
            this.parent = parent;
        }

        public override bool Equals(object o)
        {
            Square square = o as Square;
            return square.x == x && square.y == y;
        }

        public bool Equals(Square square)
        {
            return square.x == x && square.y == y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + 15 + x.GetHashCode() + y.GetHashCode();
        }

        public static bool operator !=(Square a, Square b)
        {
            if ((object)b == null || (object)a == null)
            {
                return (object)b != (object)a;
            }
            return a.x != b.x || a.y != b.y;
        }

        public static bool operator ==(Square a, Square b)
        {
            if ((object)b == null || (object)a == null)
            {
                return (object)b == (object)a;
            }
            return a.x == b.x && a.y == b.y;
        }
    }

    /// <summary>
    /// Här försöker vi hitta den bästa vägen till målet på kartan.
    /// </summary>
    class PathFinding
    {
        public Square[,] closedSquares = new Square[128, 128];
        public Square[,] openSquares = new Square[128, 128];
        public List<Square> openSquaresList = new List<Square>();

        private Position[] adjacentSquares = new Position[8] { 
                new Position(1, 1, 14), 
                new Position(-1, 1, 14), 
                new Position(-1, -1, 14),
                new Position(1, -1, 14),
                new Position(1, 0, 10), 
                new Position(-1, 0, 10),
                new Position(0, 1, 10),
                new Position(0, -1, 10)};

        public Stack<Square> Start(int startX, int startY, int targetX, int targetY, Bot bot)
        {
            closedSquares = new Square[bot.room.width, bot.room.height];
            openSquares = new Square[bot.room.width, bot.room.height];
            openSquaresList = new List<Square>();

            int h = CalculateH(startX, startY, targetX, targetY);
            Square start = new Square(startX, startY, 0, h, null);
            openSquares[startX, startY] = start;
            openSquaresList.Add(start);
            Square current = start;
            while (openSquaresList.Count > 0)
            {
                //Find square with lowest F score in openlist
                current = openSquaresList.First();
                foreach (Square s in openSquaresList)
                {
                    if (s.G + s.H < current.G + current.H)
                    {
                        current = s;
                    }
                }

                //Check if current square is target. if so, construct and return path
                if (current.x == targetX && current.y == targetY)
                {
                    Stack<Square> temppath = ReconstructPath(new Stack<Square>(), current);
                    Stack<Square> newpath = new Stack<Square>();
                    while (temppath.Count > 0)
                    {
                        Square s = temppath.Pop();
                        if (bot.room.getBlock(0, s.x, s.y).blockId == 4 || bot.room.getBlock(0, s.x, s.y).blockId == 32 ||bot.room.getBlock(0, s.x, s.y).blockId == 0)
                        {
                            newpath.Push(s);
                        }
                        else
                            break;

                    }
                    newpath = new Stack<Square>(newpath);
                    Console.WriteLine(newpath.Count);
                    string str = "";
                    foreach (Square s in newpath)
                        str += "X:" + s.x + " Y:" + s.y + " \n";
                    Console.WriteLine(str);
                    return newpath;
                }

                //Switch current square from openlist to closedlist
                closedSquares[current.x, current.y] = current;
                openSquares[current.x, current.y] = null;
                for (int i = 0; i < openSquaresList.Count; i++)
                {
                    Square temp = openSquaresList[i];
                    if (temp.x == current.x && temp.y == current.y)
                    {
                        openSquaresList.RemoveAt(i);
                        break;
                    }
                }

                //Check the neighbours of the current square
                for (int i = 0; i < 8; i++)
                {
                    Square neighbour = new Square(
                        current.x + adjacentSquares[i].x,
                        current.y + adjacentSquares[i].y,
                        current.G + adjacentSquares[i].cost,
                        CalculateH(current.x + adjacentSquares[i].x, current.y + adjacentSquares[i].y, targetX, targetY),
                        current);
                    if (neighbour.x < 0 || neighbour.y < 0 || neighbour.x > bot.room.width || neighbour.y > bot.room.height)
                        continue;
                    if (bot.room.getBlock(0, neighbour.x, neighbour.y).blockId != 4)
                        neighbour.G += 10000;
                    //neighbour.G += bot.room.getBlock(0, neighbour.x, neighbour.y);

                    int neighborInClosedF = 0;
                    Square temp = closedSquares[neighbour.x, neighbour.y];
                    if (temp != null)
                        neighborInClosedF = temp.G + temp.H;

                    //If it's in closedsquares and its F is over the one in closedsquares, dont care about it
                    if (closedSquares[neighbour.x, neighbour.y] != null && (neighbour.G + neighbour.H) >= neighborInClosedF)
                        continue;
                    //If it isn't in opensquares or its F is lower than the one in closedsquares, add it or swap them
                    if ((openSquares[neighbour.x, neighbour.y] == null || ((neighbour.G + neighbour.H) < neighborInClosedF) && bot.room.getBlock(0, neighbour.x, neighbour.y).blockId == 4))
                    {
                        neighbour.parent = current;
                        openSquares[neighbour.x, neighbour.y] = neighbour;
                        openSquaresList.Add(neighbour);
                    }
                }
            }
            return null;
        }

        Stack<Square> ReconstructPath(Stack<Square> currentstack, Square current)
        {
            if (current.parent != null)
            {
                currentstack.Push(current.parent);
                currentstack = ReconstructPath(currentstack, current.parent);
            }
            else
                currentstack.Push(current);

            return currentstack;
        }

        public int CalculateH(int x1, int y1, int x2, int y2)
        {
            int deltaX = Math.Abs(x1 - x2);
            int deltaY = Math.Abs(y1 - y2);
            return (Math.Max(deltaX, deltaY) - Math.Min(deltaX, deltaY)) + 14 * Math.Min(deltaX, deltaY);
        }
    }
}
