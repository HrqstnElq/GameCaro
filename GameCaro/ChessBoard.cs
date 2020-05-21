using System.Drawing;

namespace GameCaro
{
    internal class ChessBoard
    {
        private Image _imageO = new Bitmap(Properties.Resources.o);
        private Image _imageX = new Bitmap(Properties.Resources.x);
        private Pen myPen = new Pen(Color.DarkBlue, 1);
        private Pen highLight = new Pen(Color.OrangeRed, 1);
        private Point _oldPoint; //dung de ngung to sang o dang dc chuot tro vao
        private int _numLine;
        private int _numColmn;

        public int NumLine { get => _numLine; set => _numLine = value; }
        public int NumColmn { get => _numColmn; set => _numColmn = value; }

        public ChessBoard()
        {
            NumLine = 0;
            NumColmn = 0;
        }

        public ChessBoard(int column, int line)
        {
            this.NumColmn = column;
            this.NumLine = line;
        }

        //ve ban co
        public void DrawChessBoard(Graphics g)
        {
            for (int i = 0; i <= NumColmn; i++)
            {
                g.DrawLine(myPen, new Point(i * 30, 0), new Point(i * 30, NumColmn * 30));
            }
            for (int i = 0; i <= NumLine; i++)
            {
                g.DrawLine(myPen, new Point(0, i * 30), new Point(NumLine * 30, i * 30));
            }
        }

        //ve quan co
        public void DrawChess(Graphics g, Point point, int player)
        {
            //quan o
            if (player == 1)
            {
                g.DrawImage(_imageO, point);
            }
            else //quan x
            {
                g.DrawImage(_imageX, point);
            }
        }

        public void HighLight(Graphics g, Point point)
        {
            if (g != null && _oldPoint != point)
            {
                g.DrawRectangle(myPen, _oldPoint.X, _oldPoint.Y, 30, 30);
                g.DrawRectangle(highLight, point.X, point.Y, 30, 30);
                _oldPoint = point;
            }
        }
    }
}