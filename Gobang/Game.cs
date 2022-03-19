using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    abstract class Game
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public Player Robot { get; set; }

        public int GameType { get; set; }

        public int Option { get; set; }

        public string GameName { get; set; }

        public string Winner { get; set; }


        public virtual void StarGame()
        {
#pragma warning disable CA1416
            Console.SetWindowSize(60, 25);
#pragma warning restore CA1416
            Helper.Help.Menu();
            Option = Helper.Help.GetOption();
            switch (Option)
            {
                case 1:
                    GameMode();
                    break;
                case 2:
                    LoadGame();
                    break;
                case 0:
                    ExitGame();
                    break;
                default:
                    break;
            }

        }
        public virtual void GameMode()
        {
            Helper.Help.GameModeMenu();
            GameType = Helper.Help.GetOption();
            switch (GameType)
            {
                case 1:
                    Single();
                    break;
                case 2:
                    MultiPlayer();
                    break;
                case 0:
                    StarGame();
                    break;
                default:
                    break;
            }

        }
        public abstract void LoadGame();

        public virtual void ExitGame()
        {
            Console.SetCursorPosition(0, 19);
            Console.WriteLine("please press any key to exit...");
            Console.ReadKey();
            Console.WriteLine("Byebye!");

        }
        public virtual void Single()
        {
            InitPlayer();
            Robot.GetName();
            P2.GetName();
            Console.WriteLine("P2：{0}", P2.piece.Color);
            Helper.Help.Message("continue");
            Console.ReadKey();
            PlaySingle();
            GetWinner();
            ExitGame();
        }
        public virtual void MultiPlayer()
        {
            InitPlayer();
            P1.GetName();
            Console.WriteLine("P1：{0}", P1.piece.Color);
            P2.GetName();
            Console.WriteLine("P2：{0}", P2.piece.Color);
            Helper.Help.Message("continue");
            Console.ReadKey();
            PlayMulti();
            GetWinner();
            ExitGame();
        }
        public virtual void InitPlayer()
        {
            if (GameType == 1)
            {
                InitSinglePlay();
            }
            else
            {

                InitMultiPlay();

            }

        }
        public virtual void InitSinglePlay()
        {
            Robot = new Robot("Robot");

            P2 = new Human("P2");
            Robot.SetPieceColor();
            P2.SetPieceColor();
        }
        public virtual void InitMultiPlay()
        {
            P1 = new Human("P1");
            P1.SetPieceColor();
            P2 = new Human("P2");
            P2.SetPieceColor();
        }
        public virtual void GetWinner()
        {
            if (Controller.ControllPanel.Winner == null)
                Console.WriteLine("Bye-Bye!!!");
            Console.SetCursorPosition(0, 18);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("***{0}***", Winner = Controller.ControllPanel.Winner);
            Helper.Help.Message("win");
            Console.ResetColor();

            Console.WriteLine("Do you want to play it again?(Y/N):");
            String tmp = Console.ReadLine();
            tmp = tmp.ToUpper();
            if (tmp == "Y")
            {
                Manager.OB.FormatList(Manager.OB.histories);
                Controller.ControllPanel.isWin = false;
                StarGame();
            }

        }
        public abstract void PlaySingle();
        public abstract void PlayMulti();





    }
}
