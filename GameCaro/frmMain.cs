using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class frmMain : Form
    {
        private Play _gamePlay = new Play();
        private ChessBoard _chessBoard = new ChessBoard(Config.numChess, Config.numChess);
        private Graphics _plnGrap;

        private NetworkSocket _Socket = new NetworkSocket();
        private bool _lanEnable = false;
        private bool _isServer = false;
        private bool _isWinner = false;
        private Point _oldPoint;
        private Thread _threadListen;
        private DataSender _data;

        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = Config.MainFormSize;
            panel_ChessBoard.Size = Config.ChessBoardSize;
            panel_menu.Size = Config.MenuSize;
            _plnGrap = panel_ChessBoard.CreateGraphics();
            _gamePlay.eventChienThang += GamePlay_eventChienThang;

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string ip = _Socket.GetIPv4(NetworkInterfaceType.Ethernet);
            if (string.IsNullOrEmpty(ip))
                ip = _Socket.GetIPv4(NetworkInterfaceType.Wireless80211);
            txb_IP.Text = string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip;
        }
        #region MenuStrip
        private void newGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (_lanEnable == true)
            {
                _gamePlay.LuotDi = 2;
                _Socket.Send(new DataSender((int)Mode.NEW_GAME, new Point(0, 0)));
            }
            if (_gamePlay.CheDoChoi == 0)
                MessageBox.Show("Chọn chế độ chơi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (_gamePlay.CheDoChoi == 1)
            {
                _plnGrap.Clear(panel_ChessBoard.BackColor);
                _gamePlay.NguoiVsNguoi(_plnGrap);
                this.Invoke((MethodInvoker)(() => { panel_ChessBoard.Enabled = true; }));
            }
            if (_lanEnable == true)
                panel_ChessBoard.Enabled = _isServer;
            _isWinner = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Event
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn thoát ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    e.Cancel = true;
        }

        private void panel_ChessBoard_Paint(object sender, PaintEventArgs e)
        {
            if (_gamePlay.SanSang == true)
            {
                _gamePlay.veBanCo(_plnGrap);
                _gamePlay.veLaiCacQuanCoDaDanh(_plnGrap);
            }
        }

        private void radio_pvp_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_pvp.Checked == true)
                _gamePlay.CheDoChoi = 1;
            swift_lanMode.Enabled = radio_pvp.Checked;
        }

        private void radio_pvc_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_pvc.Checked == true)
                _gamePlay.CheDoChoi = 2;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_lanEnable == true)
            {
                if (_Socket.ConnectServerr(txb_IP.Text))
                {
                    this.Text = "Caro - Client : ";
                    //btn_Start.OnHoverBaseColor = Color.Red;
                    btn_Start.BaseColor = Color.Red;
                    Listen();
                }
                else
                {
                    _Socket.CreateServer();
                    this.Text = "Caro - Server : " + txb_IP.Text;
                    _Socket.Connected += Socket_Connected;
                    btn_Start.BaseColor = Color.Green;
                    Listen();
                    _isServer = true;
                }
                btn_Start.Enabled = false;
            }
            else
            {
                btn_Start.Enabled = false;
                if (_gamePlay.CheDoChoi == 1)
                    _gamePlay.NguoiVsNguoi(_plnGrap);
                else if (_gamePlay.CheDoChoi == 2)
                    MessageBox.Show("Chua lam");
                else if (_gamePlay.CheDoChoi == 0)
                {
                    MessageBox.Show("Chưa chọn chế độ chơi");
                    btn_Start.Enabled = true;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void Socket_Connected(object sender, EventArgs e)
        {
            //this.Invoke((MethodInvoker)(() => { panel_ChessBoard.Enabled = true; }));
        }

        private void Listen()
        {
            _threadListen = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        _data = (DataSender)_Socket.Receive();
                        if (_data != null)
                        {
                            ProcessData(_data);
                        }
                    }
                    catch { }
                }
            });
            _threadListen.IsBackground = true;
            _threadListen.Start();
        }

        private void ProcessData(DataSender data)
        {
            switch (data.Mode)
            {
                case ((int)Mode.NEW_GAME):
                    this.Invoke((MethodInvoker)(() =>
                    {
                        _isWinner = false;
                        _gamePlay.LuotDi = 2;
                        _plnGrap.Clear(panel_ChessBoard.BackColor);
                        _gamePlay.NguoiVsNguoi(_plnGrap);
                        panel_ChessBoard.Enabled = _isServer;
                    }));
                    break;
                case ((int)Mode.PLAYER_MARK):
                    this.Invoke((MethodInvoker)(() =>
                    {
                        _gamePlay.danhCo(_plnGrap, data.Point);
                        panel_ChessBoard.Enabled = true;
                    }));
                    break;
                case ((int)Mode.END_GAME):
                    this.Invoke((MethodInvoker)(() =>
                    {
                        _gamePlay.danhCo(_plnGrap, data.Point);
                        MessageBox.Show("Đối thủ thắng");
                        panel_ChessBoard.Enabled = false;
                    }));
                    break;
                case ((int)Mode.QUIT):
                    break;
                default:
                    break;
            }
        }
        private void panel_ChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            _oldPoint = e.Location;
            bool danhThanhcong = false;
            if (_gamePlay.SanSang == true)
                danhThanhcong = _gamePlay.danhCo(_plnGrap, e.Location);
            if (_lanEnable == true)
            {
                if (danhThanhcong == true && _isWinner == false)
                {
                    _Socket.Send(new DataSender((int)Mode.PLAYER_MARK, e.Location));
                    panel_ChessBoard.Enabled = false;
                }
            }
        }

        private void panel_ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (_gamePlay.SanSang == true)
                _gamePlay.toSangO(_plnGrap, e.Location);

        }
        private void swift_lanMode_CheckedChanged(object sender, EventArgs e)
        {
            _lanEnable = swift_lanMode.Checked;
            panel_IP.Enabled = swift_lanMode.Checked;
            pnl_timer.Enabled = swift_lanMode.Checked;
        }

        private void GamePlay_eventChienThang(object sender, EventArgs e)
        {
            if (_lanEnable == false)
            {
                int player = (int)sender;
                if (player == 1)
                    MessageBox.Show("Đỏ thắng");
                else if (player == 2)
                    MessageBox.Show("Xanh thắng");
                panel_ChessBoard.Enabled = false;
            }
            else
            {
                if (_isServer == true && _gamePlay.LuotDi == 1 || _isServer == false && _gamePlay.LuotDi == 2)
                {
                    _isWinner = true;
                    _Socket.Send(new DataSender((int)Mode.END_GAME, _oldPoint));
                    panel_ChessBoard.Enabled = false;
                    MessageBox.Show("Bạn thắng");
                }
            }
        }
        #endregion

        
    }
}
