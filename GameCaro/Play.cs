using System;
using System.Collections.Generic;
using System.Drawing;
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
            arrChess = new Chess[Config.numChess, Config.numChess];

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
            if (mousePoint.Y % Config.ChessSize.Height != 0 && mousePoint.X % Config.ChessSize.Width != 0)
            {
                //chi danh vao nhung o trong
                if (arrChess[dong, cot].Player == 0)
                {
                    if (_luotDi == 1)
                    {
                        chessBoard.DrawChess(g, new Point(cot * Config.ChessSize.Width, dong * Config.ChessSize.Height), _luotDi);
                        arrChess[dong, cot].Player = 1;
                        Chess chess = new Chess(arrChess[dong, cot].Dong, arrChess[dong, cot].Cot, arrChess[dong, cot].Player);
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

        public void NguoiVsMay(Graphics g)
        {
            oVuaDanh = null;
            _luotDi = 1;
            _cheDoChoi = 2;
            _sanSang = true;
            _nuocDaDi = new Stack<Chess>();
            khoiTaoMangOCo();
            veBanCo(g);
            MayDanh(g);
        }


        #region KiemTraThang
        public bool ThangRoi()
        {
            if (oVuaDanh != null)
                return DuyenNgang() || DuyetDoc() || DuyetCheoChinh() || DuyetCheoPhu();
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
            return demTrai + demPhai >= 5 ? true : false;
        }

        private bool DuyetDoc()
        {
            int demTren = 0;
            int demDuoi = 0;
            for (int i = oVuaDanh.Dong; i >= 0; i--)
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
            for (int i = 0; i < Config.numChess; i++)
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

        #region AI

        private int[] MangDiemTanCong = new int[7] { 0, 4, 25, 246, 7300, 6561, 59049 };
        private int[] MangDiemPhongNgu = new int[7] { 0, 3, 24, 243, 2197, 19773, 177957 };
        public void MayDanh(Graphics g)
        {
            if (_nuocDaDi.Count == 0)
            {
                Random rd = new Random();
                danhCo(g, new Point(rd.Next(5, 15) * Config.ChessSize.Height + 1, rd.Next(5, 15) * Config.ChessSize.Width + 1));
            }
            else
            {
                Chess chess = TimKiemNuocDi();
                danhCo(g, new Point(chess.Cot * Config.ChessSize.Width + 1, chess.Dong * Config.ChessSize.Height + 1));
            }
        }


        private Chess TimKiemNuocDi()
        {
            Chess chess = new Chess();
            long DiemTanCong = 0;
            long DiemPhongNgu = 0;
            long DiemMax = 0;
            for (int i = 0; i < Config.numChess; i++)
            {
                for (int j = 0; j < Config.numChess; j++)
                {
                    if (arrChess[i, j].Player == 0)
                    {
                        long DiemTam = 0;

                        DiemTanCong = duyetTC_Ngang(i, j) + duyetTC_Doc(i, j) + duyetTC_CheoXuoi(i, j) + duyetTC_CheoNguoc(i, j);
                        DiemPhongNgu = duyetPN_Ngang(i, j) + duyetPN_Doc(i, j) + duyetPN_CheoXuoi(i, j) + duyetPN_CheoNguoc(i, j);
                        DiemTam = DiemTanCong > DiemPhongNgu ? DiemTanCong : DiemPhongNgu;
                        if(DiemMax < DiemTam)
                        {
                            DiemMax = DiemTam;
                            chess = new Chess(i, j, arrChess[i,j].Player);
                        }
                    }
                }

            }
            return chess;
        }

        #region Tan_cong
        public int duyetTC_Ngang(int dongHT, int cotHT)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichPhai = 0;
            int SoQuanDichTrai = 0;
            int KhoangChong = 0;

            //bên phải
            for (int dem = 1; dem <= 4 && cotHT < Config.numChess - 5; dem++)
            {

                if (arrChess[dongHT, cotHT + dem].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;
                }
                else
                    if (arrChess[dongHT, cotHT + dem].Player == 2)
                {
                    SoQuanDichPhai++;
                    break;
                }
                else KhoangChong++;
            }
            //bên trái
            for (int dem = 1; dem <= 4 && cotHT > 4; dem++)
            {
                if (arrChess[dongHT, cotHT - dem].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT, cotHT - dem].Player == 2)
                {
                    SoQuanDichTrai++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichPhai > 0 && SoQuanDichTrai > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichPhai + SoQuanDichTrai];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //duyệt dọc
        public int duyetTC_Doc(int dongHT, int cotHT)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichTren = 0;
            int SoQuanDichDuoi = 0;
            int KhoangChong = 0;

            //bên trên
            for (int dem = 1; dem <= 4 && dongHT > 4; dem++)
            {
                if (arrChess[dongHT - dem, cotHT].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT - dem, cotHT].Player == 2)
                {
                    SoQuanDichTren++;
                    break;
                }
                else KhoangChong++;
            }
            //bên dưới
            for (int dem = 1; dem <= 4 && dongHT < Config.numChess - 5; dem++)
            {
                if (arrChess[dongHT + dem, cotHT].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT + dem, cotHT].Player == 2)
                {
                    SoQuanDichDuoi++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichTren > 0 && SoQuanDichDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichTren + SoQuanDichDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //chéo xuôi
        public int duyetTC_CheoXuoi(int dongHT, int cotHT)
        {
            int DiemTanCong = 1;
            int SoQuanTa = 0;
            int SoQuanDichCheoTren = 0;
            int SoQuanDichCheoDuoi = 0;
            int KhoangChong = 0;

            //bên chéo xuôi xuống
            for (int dem = 1; dem <= 4 && cotHT < Config.numChess - 5 && dongHT < Config.numChess - 5; dem++)
            {
                if (arrChess[dongHT + dem, cotHT + dem].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT + dem, cotHT + dem].Player == 2)
                {
                    SoQuanDichCheoTren++;
                    break;
                }
                else KhoangChong++;
            }
            //chéo xuôi lên
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT > 4; dem++)
            {
                if (arrChess[dongHT - dem, cotHT - dem].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT - dem, cotHT - dem].Player == 2)
                {
                    SoQuanDichCheoDuoi++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichCheoTren > 0 && SoQuanDichCheoDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichCheoTren + SoQuanDichCheoDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }

        //chéo ngược
        public int duyetTC_CheoNguoc(int dongHT, int cotHT)
        {
            int DiemTanCong = 0;
            int SoQuanTa = 0;
            int SoQuanDichCheoTren = 0;
            int SoQuanDichCheoDuoi = 0;
            int KhoangChong = 0;

            //chéo ngược lên
            for (int dem = 1; dem <= 4 && cotHT < Config.numChess - 5 && dongHT > 4; dem++)
            {
                if (arrChess[dongHT - dem, cotHT + dem].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT - dem, cotHT + dem].Player == 2)
                {
                    SoQuanDichCheoTren++;
                    break;
                }
                else KhoangChong++;
            }
            //chéo ngược xuống
            for (int dem = 1; dem <= 4 && cotHT > 4 && dongHT < Config.numChess - 5; dem++)
            {
                if (arrChess[dongHT + dem, cotHT - dem].Player == 1)
                {
                    if (dem == 1)
                        DiemTanCong += 37;

                    SoQuanTa++;
                    KhoangChong++;

                }
                else
                    if (arrChess[dongHT + dem, cotHT - dem].Player == 2)
                {
                    SoQuanDichCheoDuoi++;
                    break;
                }
                else KhoangChong++;
            }
            //bị chặn 2 đầu khoảng chống không đủ tạo thành 5 nước
            if (SoQuanDichCheoTren > 0 && SoQuanDichCheoDuoi > 0 && KhoangChong < 4)
                return 0;

            DiemTanCong -= MangDiemPhongNgu[SoQuanDichCheoTren + SoQuanDichCheoDuoi];
            DiemTanCong += MangDiemTanCong[SoQuanTa];
            return DiemTanCong;
        }
        #endregion

        #region phòng ngự

        //duyệt ngang
        public int duyetPN_Ngang(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongPhai = 0;
            int KhoangChongTrai = 0;
            bool ok = false;


            for (int dem = 1; dem <= 4 && cotHT < Config.numChess - 5; dem++)
            {
                if (arrChess[dongHT, cotHT + dem].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT, cotHT + dem].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongPhai++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongPhai == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;

            for (int dem = 1; dem <= 4 && cotHT > 4; dem++)
            {
                if (arrChess[dongHT, cotHT - dem].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT, cotHT - dem].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTrai++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongTrai == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTrai + KhoangChongPhai + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        //duyệt dọc
        public int duyetPN_Doc(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT > 4; dem++)
            {
                if (arrChess[dongHT - dem, cotHT].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;

                }
                else
                    if (arrChess[dongHT - dem, cotHT].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTren++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;
            //xuống
            for (int dem = 1; dem <= 4 && dongHT < Config.numChess - 5; dem++)
            {
                //gặp quân địch
                if (arrChess[dongHT + dem, cotHT].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT + dem, cotHT].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongDuoi++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];
            return DiemPhongNgu;
        }

        //chéo xuôi
        public int duyetPN_CheoXuoi(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT < Config.numChess - 5 && cotHT < Config.numChess - 5; dem++)
            {
                if (arrChess[dongHT + dem, cotHT + dem].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT + dem, cotHT + dem].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTren++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;
            //xuống
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT > 4; dem++)
            {
                if (arrChess[dongHT - dem, cotHT - dem].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT - dem, cotHT - dem].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongDuoi++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaPhai + SoQuanTaTrai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        //chéo ngược
        public int duyetPN_CheoNguoc(int dongHT, int cotHT)
        {
            int DiemPhongNgu = 0;
            int SoQuanTaTrai = 0;
            int SoQuanTaPhai = 0;
            int SoQuanDich = 0;
            int KhoangChongTren = 0;
            int KhoangChongDuoi = 0;
            bool ok = false;

            //lên
            for (int dem = 1; dem <= 4 && dongHT > 4 && cotHT < Config.numChess - 5; dem++)
            {

                if (arrChess[dongHT - dem, cotHT + dem].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT - dem, cotHT + dem].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaPhai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongTren++;
                }
            }


            if (SoQuanDich == 3 && KhoangChongTren == 1 && ok)
                DiemPhongNgu -= 200;

            ok = false;

            //xuống
            for (int dem = 1; dem <= 4 && dongHT < Config.numChess - 5 && cotHT > 4; dem++)
            {
                if (arrChess[dongHT + dem, cotHT - dem].Player == 2)
                {
                    if (dem == 1)
                        DiemPhongNgu += 9;

                    SoQuanDich++;
                }
                else
                    if (arrChess[dongHT + dem, cotHT - dem].Player == 1)
                {
                    if (dem == 4)
                        DiemPhongNgu -= 170;

                    SoQuanTaTrai++;
                    break;
                }
                else
                {
                    if (dem == 1)
                        ok = true;

                    KhoangChongDuoi++;
                }
            }

            if (SoQuanDich == 3 && KhoangChongDuoi == 1 && ok)
                DiemPhongNgu -= 200;

            if (SoQuanTaPhai > 0 && SoQuanTaTrai > 0 && (KhoangChongTren + KhoangChongDuoi + SoQuanDich) < 4)
                return 0;

            DiemPhongNgu -= MangDiemTanCong[SoQuanTaTrai + SoQuanTaPhai];
            DiemPhongNgu += MangDiemPhongNgu[SoQuanDich];

            return DiemPhongNgu;
        }

        #endregion
        #endregion

    }

}
