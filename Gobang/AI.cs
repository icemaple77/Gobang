using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    class AI
    {
        public int X { get; set; }
        public int Y { get; set; }
        public void CreateRandomNumber(out int m, out int n, int X, int Y)
        {
            m = X;
            n = Y;

            Random r = new Random();
            int k = r.Next(8);




            switch (k)
            {
                case 0:
                    m -= 2;

                    break;
                case 1:
                    m += 2;

                    break;
                case 2:

                    n--;
                    break;
                case 3:
                    n -= 2;
                    break;
                case 4:
                    m -= 2;
                    n--;
                    break;
                case 5:
                    m += 2;
                    n++;
                    break;
                case 6:
                    m += 2;
                    n--;
                    break;
                case 7:
                    m -= 2;
                    n++;
                    break;
                default:
                    m = X + 4;
                    n = Y - 2;
                    break;
            }

        }


        public void Thinking(int X, int Y)
        {
            int m = X;
            int n = Y;
            CreateRandomNumber(out m, out n, X, Y);
            if (m < 0 || m > 28 || n < 0 || n > 14)
            {
                int tmp;

                Random shazi = new Random();
                tmp = shazi.Next(0, 14);


                this.X = tmp * 2;
                this.Y = tmp;
            }
            else
            {
                this.X = m;
                this.Y = n;
            }



        }
    }
}
