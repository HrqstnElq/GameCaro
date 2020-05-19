using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class frmMain : Form
    {
        Play gamePlay = new Play();
        ChessBoard chessBoard = new ChessBoard(Config.numChess, Config.numChess);
        Graphics plnGrap;
        NetworkSocket Socket = new NetworkSocket();
        bool _lanEnable = false;
        Thread threadListen;
        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = Config.MainFormSize;
            panel_ChessBoard.Size = Config.ChessBoardSize;
            panel_menu.Size = Config.MenuSize;
            plnGrap = panel_ChessBoard.CreateGraphics();
            gamePlay.eventChienThang += GamePlay_eventChienThang;

        }

        private void GamePlay_eventChienThang(object sender, EventArgs e)
        {
            int player = (int)sender;
            if (player == 1)
                MessageBox.Show("Đỏ thắng");
            else if (player == 2)
                MessageBox.Show("Xanh thắng");
            panel_ChessBoard.Enabled = false;
        }
        #region MenuStrip
        private void newGameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (gamePlay.CheDoChoi == 0)
                MessageBox.Show("Chọn chế độ chơi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (gamePlay.CheDoChoi == 1)
            {
                plnGrap.Clear(panel_ChessBoard.BackColor);
                gamePlay.NguoiVsNguoi(plnGrap);
                this.Invoke((MethodInvoker)(() => { panel_ChessBoard.Enabled = true; }));
            }      
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
            if (gamePlay.SanSang == true)
            {
                gamePlay.veBanCo(plnGrap);
                gamePlay.veLaiCacQuanCoDaDanh(plnGrap);
            }
        }

        private void radio_pvp_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_pvp.Checked == true)
                gamePlay.CheDoChoi = 1;
            swift_lanMode.Enabled = radio_pvp.Checked;
        }

        private void radio_pvc_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_pvc.Checked == true)
                gamePlay.CheDoChoi = 2;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_lanEnable == true)
            {
                if(Socket.ConnectServerr(txb_IP.Text))
                {
                    this.Text = "Caro - Client : ";
                    btn_Start.OnHoverBaseColor = Color.Red;
                    Listen();
                    newGameToolStripMenuItem.PerformClick();
                    panel_ChessBoard.Enabled = false;
                }
                else
                {
                    Socket.CreateServer();
                    this.Text = "Caro - Server : " + txb_IP.Text;
                    Socket.Connected += Socket_Connected;
                    btn_Start.BaseColor = Color.Green;
                    Listen();
                }
                btn_Start.Enabled = false;
            }
            else
            {
                btn_Start.Enabled = false;
                if (gamePlay.CheDoChoi == 1)
                    gamePlay.NguoiVsNguoi(plnGrap);
                else if (gamePlay.CheDoChoi == 2)
                    MessageBox.Show("Chua lam");
                else if (gamePlay.CheDoChoi == 0)
                {
                    MessageBox.Show("Chưa chọn chế độ chơi");
                    btn_Start.Enabled = true;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void Socket_Connected(object sender, EventArgs e)
        {
            newGameToolStripMenuItem.PerformClick();
            panel_ChessBoard.Enabled = true;
        }

        private void Listen()
        {
            threadListen = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Point data = (Point)Socket.Receive();
                        if (data != null)
                        {
                            ProcessData(data);
                            this.Invoke((MethodInvoker)(() => { panel_ChessBoard.Enabled = true; }));
                        }
                    }
                    catch { }
                }
            });
            threadListen.IsBackground = true;
            threadListen.Start();
        }

        private void ProcessData(Point data)
        {
            gamePlay.danhCo(plnGrap, data);
        }
        private void panel_ChessBoard_MouseClick(object sender, MouseEventArgs e)
        {
            bool danhThanhcong = false;
            if (gamePlay.SanSang == true)
                danhThanhcong = gamePlay.danhCo(plnGrap, e.Location);
            if (_lanEnable == true)
            {
                if (danhThanhcong == true)
                {
                    Socket.Send(e.Location);
                    panel_ChessBoard.Enabled = false;
                }
            }
        }

        private void panel_ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (gamePlay.SanSang == true)
                gamePlay.toSangO(plnGrap, e.Location);
            
        }
        private void swift_lanMode_CheckedChanged(object sender, EventArgs e)
        {
            _lanEnable = swift_lanMode.Checked;
            panel_IP.Enabled = swift_lanMode.Checked;
            pnl_timer.Enabled = swift_lanMode.Checked;
        }


        #endregion

    }
}
