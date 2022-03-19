using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    public class Board
    {
        private readonly string element = "+ ";
        public int X { get; set; }
        public int Y { get; set; }
        public string Element { get; }
        public int Size { get; set; }

        public string[,] CB { get; set; }
        public Board(int size)
        {
            this.Size = size;

            CB = new string[Size, Size];

        }
        public void SetBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    CB[i, j] = element;
                }
            }
        }
        public void DrawBoard()
        {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            //Console.Clear();
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    Console.Write(CB[j, i]);
                }
                Console.WriteLine();
            }
            Console.ResetColor();

            Helper.Help.Notify();

        }
    }
}
