using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    class Play
    {
        private Random random = new Random(); //dung de random o may danh neu may danh di luot dau tien
        private ChessBoard chessBoard;
        private Chess[,] arrChess; //mang quan co
        private Chess oVuaDanh;
        private int _cheDoChoi = 0;
        private int _luotDi = 0;
        private bool _sanSang = false;
        Stack<Chess> _nuocDaDi;

        //butve
        Pen penDrawLine;
        SolidBrush penBlack;
        SolidBrush penWhite;

        public int CheDoChoi { get => _cheDoChoi; set => _cheDoChoi = value; }
        public int LuotDi { get => _luotDi; set => _luotDi = value; }
        public bool SanSang { get => _sanSang; set => _sanSang = value; }

        public event EventHandler eventChienThang;

        public Play()
        {
            chessBoard = new ChessBoard(Config.numChess, Config.numChess);
            penDrawLine = new Pen(Color.DarkKhaki, 1);
            penBlack = new SolidBrush(Color.Black);
            penWhite = new SolidBrush(Color.White);
            SanSang = false;

            //khai bao mang cac o co
            arrChess = new Chess[Config.numChess,Config.numChess];
           
        }
        
        public void veBanCo(Graphics g)
        {
            chessBoard.DrawChessBoard(g);
        }

        public void khoiTaoMangOCo()
        {
            for (int i = 0; i < Config.numChess; i++)
            {
                for (int j = 0; j < Config.numChess; j++)
                {
                    if (arrChess[i, j] == null)
                        arrChess[i, j] = new Chess(i, j, 0);
                    else
                        arrChess[i, j].Player = 0;
                }
            }
        }

        //danh co
        public bool danhCo(Graphics g, Point mousePoint)
        {
            int dong = mousePoint.Y / Config.ChessSize.Height;
            int cot = mousePoint.X / Config.ChessSize.Width;         
            //ngan nguoi choi click vao giua cac vach
            if(mousePoint.Y % Config.ChessSize.Height != 0 && mousePoint.X % Config.ChessSize.Width != 0)
            {
                //chi danh vao nhung o trong
                if(arrChess[dong,cot].Player == 0)
                {
                    if(_luotDi ==1)
                    {
                        chessBoard.DrawChess(g, new Point(cot * Config.ChessSize.Width, dong * Config.ChessSize.Height),_luotDi);
                        arrChess[dong, cot].Player = 1;
                        Chess chess = new Chess(arrChess[dong,cot].Dong, arrChess[dong,cot].Cot, arrChess[dong,cot].Player);
                        _nuocDaDi.Push(chess);

                        _luotDi = 2;
                    }
                    else
                    {
                        chessBoard.DrawChess(g, new Point(cot * Config.ChessSize.Width, dong * Config.ChessSize.Height), _luotDi);
                        arrChess[dong, cot].Player = 2;
                        Chess chess = new Chess(arrChess[dong, cot].Dong, arrChess[dong, cot].Cot, arrChess[dong, cot].Player);
                        _nuocDaDi.Push(chess);

                        _luotDi = 1;
                    }
                    oVuaDanh = arrChess[dong, cot];
                    if (ThangRoi())
                        eventChienThang?.Invoke(oVuaDanh.Player, new EventArgs());
                    return true;
                }
                return false;
            }
            return false;
        }

        public void veLaiCacQuanCoDaDanh(Graphics g)
        {
            if (_nuocDaDi != null && _nuocDaDi.Count != 0) 
            {
                foreach (var item in _nuocDaDi)
                {
                    chessBoard.DrawChess(g, new Point(item.Cot * Config.ChessSize.Width, item.Dong * Config.ChessSize.Width), item.Player);
                }
            }
        }

        public void toSangO(Graphics g, Point mousePoint)
        {
            int dong = mousePoint.Y / Config.ChessSize.Height;
            int cot = mousePoint.X / Config.ChessSize.Width;

            chessBoard.HighLight(g, new Point(cot * Config.ChessSize.Width, dong * Config.ChessSize.Height));
        }    
        
        public void NguoiVsNguoi(Graphics g)
        {
            oVuaDanh = null;
            _cheDoChoi = 1;
            if (LuotDi == 1)
                MessageBox.Show("Đỏ đánh trước");
            else
                MessageBox.Show("Xanh đánh trước");
            _sanSang = true;
            khoiTaoMangOCo();
            _nuocDaDi = new Stack<Chess>();
            veBanCo(g);         
        }


        #region KiemTraThang
        public bool ThangRoi()
        {
            if (oVuaDanh != null)
                return DuyenNgang() || DuyetDoc() || DuyetCheoChinh()|| DuyetCheoPhu();
            else
                return false;
        }

        private bool DuyenNgang()
        {
            int demTrai = 0;
            int demPhai = 0;
            for (int i = oVuaDanh.Cot; i >= 0; i--)
            {
                if (arrChess[oVuaDanh.Dong, i].Player == oVuaDanh.Player)
                    demTrai++;
                else
                    break;
            }
            for (int i = oVuaDanh.Cot + 1; i < Config.numChess; i++)
            {
                if (arrChess[oVuaDanh.Dong, i].Player == oVuaDanh.Player)
                    demPhai++;
                else
                    break;
            }
            return demTrai + demPhai >= 5 ? true : false ;
        }

        private bool DuyetDoc()
        {
            int demTren = 0;
            int demDuoi = 0;
            for(int i = oVuaDanh.Dong; i>=0; i--)
            {
                if (arrChess[i, oVuaDanh.Cot].Player == oVuaDanh.Player)
                    demTren++;
                else
                    break;
            }
            for (int i = oVuaDanh.Dong + 1; i < Config.numChess; i++) 
            {
                if (arrChess[i, oVuaDanh.Cot].Player == oVuaDanh.Player)
                    demDuoi++;
                else
                    break;
            }

            return demTren + demDuoi >= 5 ? true : false;
        }

        private bool DuyetCheoChinh()
        {
            int demTren = 0;
            int demDuoi = 0;
            for(int i= 0; i< Config.numChess; i++)
            {
                if (oVuaDanh.Dong - i < 0 || oVuaDanh.Cot - i < 0)
                    break;
                if (arrChess[oVuaDanh.Dong - i, oVuaDanh.Cot - i].Player == oVuaDanh.Player)
                    demTren++;
                else
                    break;
            }
            for (int i = 1; i < Config.numChess; i++)
            {
                if (oVuaDanh.Dong + i >= Config.numChess || oVuaDanh.Cot + i >= Config.numChess)
                    break;
                if (arrChess[oVuaDanh.Dong + i, oVuaDanh.Cot + i].Player == oVuaDanh.Player)
                    demDuoi++;
                else
                    break;
            }
            return demTren + demDuoi >= 5 ? true : false;
        }
        private bool DuyetCheoPhu()
        {
            int demTren = 0;
            int demDuoi = 0;

            for (int i = 0; i < Config.numChess; i++)
            {
                if (oVuaDanh.Dong - i < 0 || oVuaDanh.Cot + i >= Config.numChess)
                    break;
                if (arrChess[oVuaDanh.Dong - i, oVuaDanh.Cot + i].Player == oVuaDanh.Player) 
                    demTren++;
                else
                    break;
            }
            for (int i = 1; i < Config.numChess; i++)
            {
                if (oVuaDanh.Dong + i >= Config.numChess || oVuaDanh.Cot - i < 0)
                    break;
                if (arrChess[oVuaDanh.Dong + i, oVuaDanh.Cot - i].Player == oVuaDanh.Player)
                    demDuoi++;
                else
                    break;
            }
            return demTren + demDuoi >= 5 ? true : false;
        }
        #endregion
    }
}
