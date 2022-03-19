using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    public abstract class Piece
    {
        public string PiecePic { get; set; }
        public string Color { get; set; }
        public virtual bool CheckMove(Board b, int X, int Y)
        {
            bool check;

            if (b.CB[X, Y] == "+ ")
                check = true;
            else
                check = false;
            return check;
        }


    }

    public class BlackPiece : Piece
    {
        public BlackPiece()
        {
            this.PiecePic = "X ";
            this.Color = "Black";

        }

    }

    public class WhitePiece : Piece
    {
        public WhitePiece()
        {
            this.PiecePic = "O ";
            this.Color = "White";

        }

    }
}
