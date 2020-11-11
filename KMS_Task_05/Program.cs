using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Task_05
{
    class Program
    {
        static string[] map;

        static void Main(string[] args)
        {
            int movesGive = 65;
            const int mapSize = 15;
            map = new string[mapSize] { "==============================",
                                        "| |  |                       |",
                                        "| |    |   |    |======  =   |",
                                        "| |====|   |    |     |   =  |",
                                        "|               |  |  |    | |",
                                        "|====| |=========  =====   | |",
                                        "|    | |    |   |          | |",
                                        "|====| |  |   | |   | ====== |",
                                        "|    | =======| |== | | |    |",
                                        "| |= |          |   | | | |==|",
                                        "| |  ========|  | ==| | | |  |",
                                        "| |          |      |   |    |",
                                        "| |  ========| |===========| |",
                                        "| |            |             |",
                                        "================   ==========="};
            WriteArr(map);
            Console.SetCursorPosition(35, 0);
            Console.Write("Ваша задача пройти лабиринт за 65 ходов");
            Console.SetCursorPosition(35, 2);
            Console.Write("Управление:");
            Console.SetCursorPosition(35, 3);
            Console.Write("w - вверх");
            Console.SetCursorPosition(35, 4);
            Console.Write("s - вниз");
            Console.SetCursorPosition(35, 5);
            Console.Write("a - влево");
            Console.SetCursorPosition(35, 6);
            Console.Write("d - вправо");
            Console.SetCursorPosition(1, 1);
            MovePlayer(1, 1, map, movesGive);
            Console.ReadKey();
        }

        static void WriteArr(string[] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++, Console.WriteLine())
                Console.Write(arr[i]);
        }

        static void MovePlayer(int x, int y, string[] map, int moves)
        {
            int x1 = x;
            int y1 = y;
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.S:
                    y1 += 1;
                    if ((map[y1][x] == '=') || (map[y1][x] == '|'))
                    {
                        y1 = y;
                    }
                    else
                    {
                        y = y1;
                        moves -= 1;
                    }
                    if (y == 14)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы прошли лабиринт");
                        goto metka;
                    }
                    if (moves == 0)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы потратили все ходы");
                        goto metka;
                    }
                    break;

                case ConsoleKey.W:
                    y1 -= 1;
                    if ((map[y1][x] == '=') || (map[y1][x] == '|'))
                    {
                        y1 = y;
                    }
                    else
                    {
                        y = y1;
                        moves -= 1;
                    }
                    if (y == 14)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы прошли лабиринт");
                        goto metka;
                    }
                    if (moves == 0)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы потратили все ходы");
                        goto metka;
                    }
                    break;

                case ConsoleKey.D:
                    x1 += 1;
                    if ((map[y][x1] == '=') || (map[y][x1] == '|'))
                    {
                        x1 = x;
                    }
                    else
                    {
                        x = x1;
                        moves -= 1;
                    }
                    if (y == 14)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы прошли лабиринт");
                        goto metka;
                    }
                    if (moves == 0)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы потратили все ходы");
                        goto metka;
                    }
                    break;

                case ConsoleKey.A:
                    x1 -= 1;
                    if ((map[y][x1] == '=') || (map[y][x1] == '|'))
                    {
                        x1 = x;
                    }
                    else
                    {
                        x = x1;
                        moves -= 1;
                    }
                    if (y == 14)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы прошли лабиринт");
                        goto metka;
                    }
                    if (moves == 0)
                    {
                        Console.SetCursorPosition(40, 2);
                        Console.WriteLine("Вы потратили все ходы");
                        goto metka;
                    }
                    break;
            }
            Console.SetCursorPosition(35, 0);
            WriteInfo(moves);
            Console.SetCursorPosition(x, y);
            MovePlayer(x, y, map, moves);
        metka:;
        }

        static void WriteInfo(int moves)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            WriteArr(map);
            Console.SetCursorPosition(40, 0);
            Console.WriteLine($"Ходов осталось: {moves}");
        }
    }
}
