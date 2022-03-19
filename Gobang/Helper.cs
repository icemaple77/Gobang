using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Version 2.0 changing the NET.Core from 5.0 to 3.2 and Fixed the symbol of element for board.
/// 
/// </summary>
namespace IFN563
{
    public sealed class Helper
    {
        private static Helper help = null;
        private static readonly object padlock = new object();
        Helper() { }
        public static Helper Help
        {
            get
            {
                lock (padlock)
                {
                    if (help == null)
                    {
                        help = new Helper();
                    }
                    return help;
                }
            }
        }
        public void Menu()
        {
            Console.WriteLine("Loading...");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Select from the following options: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t1  Star Game\n\t2  Load Game\n\t0  Exit Game");
            Console.ResetColor();


        }
        public void GameModeMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Select the game mode: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t1 Single-Player Game \n\t2 Two-Player Game \n\t0  Back");
            Console.ResetColor();

        }
        public int GetOption()
        {

            int option;
            string str1 = Console.ReadLine();
            while (!int.TryParse(str1, out option))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid value. Please re-enter.");
                Console.ResetColor();
                str1 = Console.ReadLine();
            }
            return option;
        }
        public void Notify()
        {
            Console.SetCursorPosition(0, 17);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Move:↑ ↓ <- -> or W S A D  Drop：Enter or Space");
            Console.WriteLine("F2:SAVE  F3:LOAD  U:Undo  R:Redo  Q:Exit");
            Console.ResetColor();
        }
        public void Message(string option)
        {

            switch (option)
            {

                case "win":

                    Console.WriteLine("Congratulations!!!You Won the game!");
                    break;
                case "continue":
                    Console.WriteLine("Press any key to continue...");
                    break;
                case "loading":
                    Console.WriteLine("Loading... Success!!!");
                    break;
                case "save":
                    Console.WriteLine("Saving...Success!!!");
                    break;

            }
        }
        public void HelpTurn(Player player)
        {
            Console.SetCursorPosition(32, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*************");
            Console.SetCursorPosition(32, 3);
            Console.WriteLine("*Gomoku Game*");
            Console.SetCursorPosition(32, 4);
            Console.WriteLine("*************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(34, 5);
            Console.WriteLine("Turn:{0}", player.Role);
            Console.SetCursorPosition(34, 6);
            Console.WriteLine("Name:{0}", player.Name);
            Console.SetCursorPosition(34, 7);
            Console.WriteLine("Color:{0}", player.piece.Color);
            Console.ResetColor();

        }


    }
}
