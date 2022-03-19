using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IFN563
{
    class Controller
    {
        private static Controller controllPanel = null;
        private static readonly object padlock = new object();
        Controller()
        {

        }
        public static Controller ControllPanel
        {
            get
            {
                lock (padlock)
                {
                    if (controllPanel == null)
                    {
                        controllPanel = new Controller();
                    }
                    return controllPanel;
                }
            }
        }
        //private bool turn = false;
        public string Winner { get; set; }
        public Player Player { get; set; }
        public Player P1 { get; set; }
        public Player P2 { get; set; }

        public bool isWin = false;

        public int GameType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        public bool Turn { get; set; }
        public Counter step = new Counter();
        Counter undostep = new Counter();

        //As a 
        public EachStepDetail eSD;
        public History newHistory() { return new History(eSD); }
        public void setHistory(History history) { eSD = history.ESD; }
        public string ListDetails() { return eSD.GetDetails(); }
        public void storeHistory()
        {
            eSD = new EachStepDetail(GameType, step.currentCount, Player, X, Y);
            Manager.OB.Add(Manager.OB.histories, newHistory());

        }
        //public void GetPlayDetails(int gametype, Board b, Player P1, Player P2)
        //{
        //    this.GameType = gametype;
        //    this.P1 = P1;
        //    this.P2 = P2;
        //    if (P1.piece.Color == "Black")
        //        Turn = !Turn;
        //    else
        //        Turn = Turn;
        //}
        public string Play(int gametype, Board b, Player P1, Player P2)
        {
            Y = 8;
            X = 16;
            this.GameType = gametype;
            this.P1 = P1;
            this.P2 = P2;



            Turn = !Turn;
            while (!isWin)
            {

                if (GameType == 1)
                {

                    switch (Turn)
                    {
                        case true:

                            AI aI = new AI();
                            aI.Thinking(X, Y);
                            X = aI.X;
                            Y = aI.Y;
                            Player = P1;
                            if (Player.piece.CheckMove(b, X / 2, Y))
                            {
                                step.Increase();
                                storeHistory();
                                DropPiece(b);
                                isWin = Judgement.Win.CheckWin(b, X / 2, Y);
                                if (isWin)
                                    Winner = Player.Name;
                                Turn = !Turn;
                                break;
                            }
                            break;
                        case false:

                            Player = P2;
                            Move(b);
                            break;
                    }

                }
                else if (GameType == 2)
                    switch (Turn)
                    {
                        case true:
                            Player = P1;
                            Move(b);
                            break;

                        case false:
                            Player = P2;
                            Move(b);
                            break;
                    }

            }
            return Winner;
        }
        public void Move(Board b)
        {
            Helper.Help.HelpTurn(Player);
            Console.SetCursorPosition(X, Y);
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (Y > 0 && Y < b.Size)
                        Y--;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (Y >= 0)
                        Y++;
                    if (Y >= b.Size)
                        Y = b.Size - 1;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    if (X > 0)
                        X -= 2;
                    if (X <= 0)
                        X = 0;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (X / 2 < b.Size)
                        X += 2;
                    if (X / 2 >= b.Size)
                        X = (b.Size - 1) * 2;
                    break;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    Manager.OB.FormatList(Manager.OB.temphistories);
                    undostep.currentCount = 0;
                    if (Player.piece.CheckMove(b, X / 2, Y))
                    {
                        step.Increase();
                        storeHistory();
                        DropPiece(b);

                        isWin = Judgement.Win.CheckWin(b, X / 2, Y);
                        if (isWin)
                            Winner = Player.Name;
                        Turn = !Turn;
                        break;
                    }
                    else
                    {
                        break;
                    }

                case ConsoleKey.U:
                    Undo();
                    DropPiece(b);
                    break;
                case ConsoleKey.Q:
                    Console.SetCursorPosition(0, 19);
                    Console.WriteLine("please press any key to exit...");
                    Console.ReadKey();
                    isWin = true;


                    break;
                case ConsoleKey.R:
                    Redo();
                    DropPiece(b);
                    break;
                case ConsoleKey.F2:
                    Save();
                    DropPiece(b);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Helper.Help.Message("save");

                    break;
                case ConsoleKey.F3:
                    Manager.OB.FormatList(Manager.OB.histories);
                    Loading();
                    Console.SetCursorPosition(0, 17);
                    Helper.Help.Message("loading");
                    Helper.Help.Message("continue");
                    DropPiece(b);

                    break;

                default:
                    break;

            }
            Console.SetCursorPosition(X, Y);



        }
        public void Save()
        {
            const string fileName = "GameRecord.dat";
            FileStream saveFile = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter pen = new StreamWriter(saveFile);
            for (int i = 0; i < step.currentCount; i++)
            {
                pen.WriteLine(Manager.OB.Get(Manager.OB.histories, i).ListDetails());
            }
            pen.Close();
            saveFile.Close();

        }
        public void Redo()
        {

            if (undostep.currentCount >= 2)
            {
                Manager.OB.Add(Manager.OB.histories, Manager.OB.Get(Manager.OB.temphistories, undostep.currentCount - 1));
                Manager.OB.Delete(Manager.OB.temphistories, undostep.currentCount - 1);
                undostep.Reduce(); step.Increase();
                Manager.OB.Add(Manager.OB.histories, Manager.OB.Get(Manager.OB.temphistories, undostep.currentCount - 1));
                Manager.OB.Delete(Manager.OB.temphistories, undostep.currentCount - 1);
                undostep.Reduce(); step.Increase();
            }

        }

        public void Undo()
        {
            if (step.currentCount >= 2)
            {
                Manager.OB.Add(Manager.OB.temphistories, Manager.OB.Get(Manager.OB.histories, step.currentCount - 1));
                Manager.OB.Delete(Manager.OB.histories, step.currentCount - 1);
                step.Reduce(); undostep.Increase();
                Manager.OB.Add(Manager.OB.temphistories, Manager.OB.Get(Manager.OB.histories, step.currentCount - 1));
                Manager.OB.Delete(Manager.OB.histories, step.currentCount - 1);
                step.Reduce(); undostep.Increase();
            }

        }
        public void DropPiece(Board b)
        {
            b.SetBoard();
            for (int i = 0; i < step.currentCount; i++)
            {

                X = Manager.OB.Get(Manager.OB.histories, i).ESD.X;
                Y = Manager.OB.Get(Manager.OB.histories, i).ESD.Y;
                Player = Manager.OB.Get(Manager.OB.histories, i).ESD.Player;
                b.CB[X / 2, Y] = Player.piece.PiecePic;
            }
            b.DrawBoard();
        }
        public int Loading()
        {

            const string DELIM = ",";
            const string fileName = "GameRecord.dat";
            FileStream loadFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(loadFile);
            string record;
            string[] fields;
            record = read.ReadLine();
            int x, y;

            while (record != null)
            {

                fields = record.Split(DELIM);
                this.GameType = int.Parse(fields[0]);
                Controller.ControllPanel.step.currentCount = int.Parse(fields[1]);
                Player p;
                x = int.Parse(fields[5]);
                y = int.Parse(fields[6]);
                if (fields[2] == "P2")
                {

                    p = P2;
                    P2.Name = fields[3];
                    Controller.ControllPanel.Turn = true;
                }
                else
                {
                    p = P1;
                    P1.Name = fields[3];
                    Controller.ControllPanel.Turn = false;

                }

                Controller.ControllPanel.eSD = new EachStepDetail(GameType, step.currentCount, p, x, y);
                Manager.OB.Add(Manager.OB.histories, Controller.ControllPanel.newHistory());
                record = read.ReadLine();
            }
            read.Close();
            loadFile.Close();
            return GameType;

        }
    }
}
