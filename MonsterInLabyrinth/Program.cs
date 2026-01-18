using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthMonsters
{
    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    class Monster
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Dir { get; set; }
        
        public int PrevX { get; set; } = -1;
        public int PrevY { get; set; } = -1;

        public char GetSymbol()
        {
            switch (Dir)
            {
                case Direction.Up: return '^';
                case Direction.Right: return '>';
                case Direction.Down: return 'v';
                case Direction.Left: return '<';
                default: return '?';
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string lineW = Console.ReadLine();
                if (string.IsNullOrEmpty(lineW)) return;
                int width = int.Parse(lineW);

                string lineH = Console.ReadLine();
                if (string.IsNullOrEmpty(lineH)) return;
                int height = int.Parse(lineH);

                char[][] map = new char[height][];
                List<Monster> monsters = new List<Monster>();

                for (int y = 0; y < height; y++)
                {
                    string row = Console.ReadLine();
                    if (row.Length < width) row = row.PadRight(width, '.'); 
                    map[y] = row.ToCharArray();

                    for (int x = 0; x < width; x++)
                    {
                        char c = map[y][x];
                        if ("^>v<".Contains(c))
                        {
                            Direction d = Direction.Up;
                            if (c == '>') d = Direction.Right;
                            else if (c == 'v') d = Direction.Down;
                            else if (c == '<') d = Direction.Left;

                            monsters.Add(new Monster { X = x, Y = y, Dir = d });
                        }
                    }
                }

                for (int step = 1; step <= 20; step++)
                {
                    foreach (var monster in monsters)
                    {
                        map[monster.Y][monster.X] = '.'; 

                        MoveMonster(monster, map, width, height);

                        map[monster.Y][monster.X] = monster.GetSymbol();
                    }

                    PrintMap(map);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
            }
        }

        static void MoveMonster(Monster m, char[][] map, int width, int height)
        {
            (int dx, int dy) GetOffsets(Direction d)
            {
                switch (d)
                {
                    case Direction.Up: return (0, -1);
                    case Direction.Right: return (1, 0);
                    case Direction.Down: return (0, 1);
                    case Direction.Left: return (-1, 0);
                    default: return (0, 0);
                }
            }

            Direction rightDir = (Direction)(((int)m.Dir + 1) % 4);
            Direction leftDir = (Direction)(((int)m.Dir + 3) % 4);
            Direction frontDir = m.Dir;

            var (rdx, rdy) = GetOffsets(rightDir);
            int rx = m.X + rdx;
            int ry = m.Y + rdy;

            bool isRightFree = IsFree(map, rx, ry, width, height);
            

            if (isRightFree && (rx != m.PrevX || ry != m.PrevY))
            {
                m.Dir = rightDir;
                return; 
            }

            var (fdx, fdy) = GetOffsets(frontDir);
            int fx = m.X + fdx;
            int fy = m.Y + fdy;

            if (IsFree(map, fx, fy, width, height))
            {

                m.PrevX = m.X;
                m.PrevY = m.Y;
                m.X = fx;
                m.Y = fy;
                return;
            }

           
            m.Dir = leftDir;
        }

        static bool IsFree(char[][] map, int x, int y, int width, int height)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return false;
            
            if (map[y][x] == 'X') return false;

            if ("^>v<".Contains(map[y][x])) return false;

            return true;
        }

        static void PrintMap(char[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                Console.WriteLine(new string(map[y]));
            }
            Console.WriteLine();
        }
    }
}