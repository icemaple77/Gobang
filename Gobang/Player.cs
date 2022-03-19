using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    public abstract class Player
    {

        public string Name { get; set; }
        public string Role { get; set; }
        public Piece piece { get; set; }


        public abstract void GetName();
        public virtual void SetPieceColor()
        {
            if (Role == "P2")
            {
                piece = new WhitePiece();

            }
            else
            {
                piece = new BlackPiece();
            }

        }



    }

    public class Human : Player
    {
        public Human(string role)
        {
            this.Role = role;


        }
        public override void GetName()
        {
            Console.WriteLine("Please enter {0} name", Role);
            this.Name = Console.ReadLine();
        }

    }

    public class Robot : Player
    {
        public Robot(string role)
        {
            this.Role = role;
        }

        public override void GetName()
        {
            AI aI = new AI();
            this.Name = "AlphaGo";
            Console.WriteLine("Computer Player:{0}", Name);
        }
    }
}
