using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    [Serializable]
    class DataSender
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
