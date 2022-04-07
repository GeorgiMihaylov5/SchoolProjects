using System;

namespace QueenChess
{
    class Program
    {
        private const int LENGTH = 8;
        static int[,] board = new int[LENGTH, LENGTH];
        static int queen = 1;
        static void Main(string[] args)
        {
            while (true)
            {
                Display();

                Console.Write("Enter position: ");
                string position = Console.ReadLine(); //position of queen


                int x = int.Parse(position[1].ToString());
                int y = int.Parse(position[0].ToString());

                if (board[y, x] == 0)
                {
                    AddQueen(y, x);
                }
                else if (board[y, x] % 2 != 0)
                {
                    queen = 1;
                    for (int i = 0; i < LENGTH; i++)
                    {
                        for (int j = 0; j < LENGTH; j++)
                        {
                            if (board[i,j] == board[y, x] + 1)
                            {
                                board[i, j] = 0;
                            }
                        }
                    }

                    for (int i = 0; i < LENGTH; i++)
                    {
                        for (int j = 0; j < LENGTH; j++)
                        {
                            if (board[i, j] % 2 != 0 && board[y, x] != board[i, j])
                            {
                                AddQueen(i, j);
                            }
                        }
                    }

                    board[y, x] = 0;
                }
                else
                {
                    Console.WriteLine("Try again!");
                    continue;
                }
            }
        }

        static void AddQueen(int y, int x)
        {
            board[y, x] = queen;

            int yTemp = y - 1;

            //sides
            while (yTemp >= 0)
            {
                board[yTemp, x] = queen + 1;
                yTemp--;
            }
            yTemp = y + 1;

            while (yTemp < 8)
            {
                board[yTemp, x] = queen + 1;
                yTemp++;
            }

            int xTemp = x - 1;
            while (xTemp >= 0)
            {
                board[y, xTemp] = queen + 1;
                xTemp--;
            }
            xTemp = x + 1;
            while (xTemp < 8)
            {
                board[y, xTemp] = queen + 1;
                xTemp++;
            }

            //diagonals
            xTemp = x - 1;
            yTemp = y - 1;
            while (xTemp >= 0 && yTemp >= 0)
            {
                board[yTemp, xTemp] = queen + 1;
                yTemp--;
                xTemp--;
            }
            xTemp = x - 1;
            yTemp = y + 1;
            while (xTemp >= 0 && yTemp < 8)
            {
                board[yTemp, xTemp] = queen + 1;
                yTemp++;
                xTemp--;
            }
            xTemp = x + 1;
            yTemp = y + 1;
            while (xTemp < 8 && yTemp < 8)
            {
                board[yTemp, xTemp] = queen + 1;
                yTemp++;
                xTemp++;
            }
            xTemp = x + 1;
            yTemp = y - 1;
            while (xTemp < 8 && yTemp >= 0)
            {
                board[yTemp, xTemp] = queen + 1;
                yTemp--;
                xTemp++;
            }
            queen += 2;
        }
        static void Display()
        {
            for (int i = 0; i < LENGTH; i++)
            {
                for (int j = 0; j < LENGTH; j++)
                {
                    if (board[i, j] < 10)
                    {
                        Console.Write($"0{board[i, j]} ");
                        continue;
                    }
                    Console.Write($"{board[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            for (int i = 0; i < LENGTH; i++)
            {
                for (int j = 0; j < LENGTH; j++)
                {
                    Console.Write($"{i}{j} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
