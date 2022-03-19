using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563
{
    public class History
    {
        public EachStepDetail ESD { get; set; }
        public History(EachStepDetail eSD) { this.ESD = eSD; }
        public string ListDetails() { return ESD.GetDetails(); }



    }
    public class EachStepDetail
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Player Player { get; set; }

        public int Count { get; set; }

        public int GameMode { get; set; }
        public EachStepDetail(int gamemode, int count, Player p, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Player = p;
            this.Count = count;
            this.GameMode = gamemode;
        }
        public string GetDetails()
        {
            return GameMode + "," + Count + "," + Player.Role + "," + Player.Name + "," + Player.piece.Color + "," + X + "," + Y;
        }
    }
    public sealed class Manager
    {
        private static Manager ob = null;
        private static readonly object padlock = new object();
        Manager() { }
        public static Manager OB
        {
            get
            {
                lock (padlock)
                {
                    if (ob == null)
                    {
                        ob = new Manager();
                    }
                    return ob;
                }
            }
        }
        public List<History> histories = new List<History>();
        public List<History> temphistories = new List<History>();
        public void Add(List<History> ls, History detail)
        {
            ls.Add(detail);
        }
        public History Get(List<History> ls, int index)
        {

            if (index < 0)
            {

                return null;
            }
            else
            {


                return ls[index];
            }
        }
        public void Delete(List<History> ls, int index)
        {

            ls.RemoveAt(index);


        }
        public void FormatList(List<History> ls)
        {
            ls.Clear();
        }

    }
}
