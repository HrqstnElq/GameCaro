using System;
using System.Drawing;

namespace GameCaro
{
    [Serializable]
    internal class DataSender
    {
        private Point point;
        private int mode;

        public Point Point { get => point; set => point = value; }
        public int Mode { get => mode; set => mode = value; }

        public DataSender(int mode, Point point)
        {
            Point = point;
            Mode = mode;
        }
    }

    public enum Mode
    {
        NEW_GAME,
        PLAYER_MARK,
        END_GAME,
        QUIT
    }
}