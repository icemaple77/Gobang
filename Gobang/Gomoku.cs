using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IFN563
{
    class Gomoku : Game
    {

        public Board b = new Board(15);

        public override void PlaySingle()
        {
            b.SetBoard();
            b.DrawBoard();
            //Controller.ControllPanel.GetPlayDetails (1, b, Robot,P2);
            Controller.ControllPanel.Play(1, b, Robot, P2);
        }
        public override void PlayMulti()
        {
            b.SetBoard();
            b.DrawBoard();
            //Controller.ControllPanel.GetPlayDetails(2, b, P1,P2);
            Controller.ControllPanel.Play(2, b, P1, P2);


        }

        public override void LoadGame()
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
                if (GameType == 1)
                    InitSinglePlay();
                else
                    InitMultiPlay();
                Controller.ControllPanel.step.currentCount = int.Parse(fields[1]);
                Player p;
                x = int.Parse(fields[5]);
                y = int.Parse(fields[6]);
                if (fields[2] == "P2")
                {
                    p = P2;
                    p.Name = fields[3];
                    Controller.ControllPanel.Turn = false;
                }
                else if (fields[2] == "P1")
                {
                    p = P1;
                    p.Name = fields[3];
                    Controller.ControllPanel.Turn = true;
                }
                else
                {
                    p = Robot;
                    p.Name = fields[3];
                    Controller.ControllPanel.Turn = true;

                }
                Controller.ControllPanel.eSD = new EachStepDetail(GameType, Controller.ControllPanel.step.currentCount, p, x, y);
                Manager.OB.Add(Manager.OB.histories, Controller.ControllPanel.newHistory());
                record = read.ReadLine();
            }
            read.Close();
            loadFile.Close();
            if (GameType == 1)
            {

                b.SetBoard();
                Controller.ControllPanel.DropPiece(b);
                b.DrawBoard();
                Controller.ControllPanel.Play(1, b, Robot, P2);

            }
            else
            {

                b.SetBoard();
                Controller.ControllPanel.DropPiece(b);
                b.DrawBoard();
                Controller.ControllPanel.Play(2, b, P1, P2);
            }

            GetWinner();
            ExitGame();
        }





    }
}
