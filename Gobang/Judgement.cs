using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    public sealed class Judgement
    {
        private static Judgement win = null;
        private static readonly object padlock = new object();
        Judgement() { }
        public static Judgement Win
        {
            get
            {
                lock (padlock)
                {
                    if (win == null)
                    {
                        win = new Judgement();
                    }
                    return win;
                }
            }
        }
        public int Sum { get; set; }
        public bool CheckWin(Board b, int x, int y)
        {
            int AIIdea = Sum;

            int score = 0;
            int tempX = x, tempY = y;
            string[,] temp = b.CB;


            //1
            while (y > 0)
            {
                if (b.CB[tempX, tempY] == b.CB[x, --y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;
            while (y < 14)
            {
                if (b.CB[tempX, tempY] == b.CB[x, ++y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY; score = 0;

            //2 
            while (x > 0)
            {
                if (b.CB[tempX, tempY] == b.CB[--x, y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;
            while (x < 14)
            {
                if (b.CB[tempX, tempY] == b.CB[++x, y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY; score = 0;

            //3
            while (y > 0 && x > 0)
            {
                if (b.CB[tempX, tempY] == b.CB[--x, --y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;
            while (y < 14 && x < 14)
            {
                if (b.CB[tempX, tempY] == b.CB[++x, ++y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY; score = 0;

            //4 
            while (y > 0 && x < 14)
            {
                if (b.CB[tempX, tempY] == b.CB[++x, --y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;
            while (y < 14 && x > 0)
            {
                if (b.CB[tempX, tempY] == b.CB[--x, ++y])
                {
                    score++;
                    Sum++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            return false;

        }
    }
}
