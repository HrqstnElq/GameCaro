namespace GameCaro
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel_ChessBoard = new System.Windows.Forms.Panel();
            this.panel_menu = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_IP = new System.Windows.Forms.Panel();
            this.txb_IP = new Guna.UI.WinForms.GunaTextBox();
            this.lable_ip = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.swift_lanMode = new Guna.UI.WinForms.GunaSwitch();
            this.radio_pvc = new Guna.UI.WinForms.GunaRadioButton();
            this.radio_pvp = new Guna.UI.WinForms.GunaRadioButton();
            this.btn_Start = new Guna.UI.WinForms.GunaAdvenceButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_menu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_IP.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ChessBoard
            // 
            this.panel_ChessBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(216)))), ((int)(((byte)(224)))));
            this.panel_ChessBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_ChessBoard.Location = new System.Drawing.Point(310, 30);
            this.panel_ChessBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_ChessBoard.Name = "panel_ChessBoard";
            this.panel_ChessBoard.Size = new System.Drawing.Size(119, 750);
            this.panel_ChessBoard.TabIndex = 6;
            this.panel_ChessBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_ChessBoard_Paint);
            this.panel_ChessBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_ChessBoard_MouseClick);
            this.panel_ChessBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_ChessBoard_MouseMove);
            // 
            // panel_menu
            // 
            this.panel_menu.Controls.Add(this.groupBox2);
            this.panel_menu.Controls.Add(this.pictureBox1);
            this.panel_menu.Location = new System.Drawing.Point(13, 30);
            this.panel_menu.Margin = new System.Windows.Forms.Padding(4);
            this.panel_menu.Name = "panel_menu";
            this.panel_menu.Size = new System.Drawing.Size(293, 772);
            this.panel_menu.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_IP);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.btn_Start);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 228);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(281, 293);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // panel_IP
            // 
            this.panel_IP.Controls.Add(this.txb_IP);
            this.panel_IP.Controls.Add(this.lable_ip);
            this.panel_IP.Enabled = false;
            this.panel_IP.Location = new System.Drawing.Point(6, 215);
            this.panel_IP.Name = "panel_IP";
            this.panel_IP.Size = new System.Drawing.Size(265, 59);
            this.panel_IP.TabIndex = 10;
            // 
            // txb_IP
            // 
            this.txb_IP.BaseColor = System.Drawing.Color.White;
            this.txb_IP.BorderColor = System.Drawing.Color.Silver;
            this.txb_IP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txb_IP.FocusedBaseColor = System.Drawing.Color.White;
            this.txb_IP.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txb_IP.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txb_IP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txb_IP.Location = new System.Drawing.Point(44, 15);
            this.txb_IP.Name = "txb_IP";
            this.txb_IP.PasswordChar = '\0';
            this.txb_IP.SelectedText = "";
            this.txb_IP.Size = new System.Drawing.Size(218, 30);
            this.txb_IP.TabIndex = 8;
            this.txb_IP.Text = "127.0.0.1";
            // 
            // lable_ip
            // 
            this.lable_ip.AutoSize = true;
            this.lable_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable_ip.Location = new System.Drawing.Point(12, 21);
            this.lable_ip.Name = "lable_ip";
            this.lable_ip.Size = new System.Drawing.Size(26, 24);
            this.lable_ip.TabIndex = 9;
            this.lable_ip.Text = "IP";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.swift_lanMode);
            this.panel1.Controls.Add(this.radio_pvc);
            this.panel1.Controls.Add(this.radio_pvp);
            this.panel1.Location = new System.Drawing.Point(6, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 102);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "LAN";
            // 
            // swift_lanMode
            // 
            this.swift_lanMode.BaseColor = System.Drawing.SystemColors.Control;
            this.swift_lanMode.CheckedOffColor = System.Drawing.Color.DarkGray;
            this.swift_lanMode.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.swift_lanMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.swift_lanMode.Enabled = false;
            this.swift_lanMode.FillColor = System.Drawing.Color.White;
            this.swift_lanMode.Location = new System.Drawing.Point(178, 13);
            this.swift_lanMode.Name = "swift_lanMode";
            this.swift_lanMode.Size = new System.Drawing.Size(36, 32);
            this.swift_lanMode.TabIndex = 0;
            this.swift_lanMode.CheckedChanged += new System.EventHandler(this.swift_lanMode_CheckedChanged);
            // 
            // radio_pvc
            // 
            this.radio_pvc.BaseColor = System.Drawing.SystemColors.Control;
            this.radio_pvc.CheckedOffColor = System.Drawing.Color.Gray;
            this.radio_pvc.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(189)))));
            this.radio_pvc.FillColor = System.Drawing.Color.White;
            this.radio_pvc.Location = new System.Drawing.Point(4, 57);
            this.radio_pvc.Name = "radio_pvc";
            this.radio_pvc.Size = new System.Drawing.Size(111, 20);
            this.radio_pvc.TabIndex = 0;
            this.radio_pvc.Text = "Người vs Máy";
            this.radio_pvc.CheckedChanged += new System.EventHandler(this.radio_pvc_CheckedChanged);
            // 
            // radio_pvp
            // 
            this.radio_pvp.BaseColor = System.Drawing.SystemColors.Control;
            this.radio_pvp.CheckedOffColor = System.Drawing.Color.Gray;
            this.radio_pvp.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(189)))));
            this.radio_pvp.FillColor = System.Drawing.Color.White;
            this.radio_pvp.Location = new System.Drawing.Point(4, 13);
            this.radio_pvp.Name = "radio_pvp";
            this.radio_pvp.Size = new System.Drawing.Size(121, 20);
            this.radio_pvp.TabIndex = 0;
            this.radio_pvp.Text = "Người vs Người";
            this.radio_pvp.CheckedChanged += new System.EventHandler(this.radio_pvp_CheckedChanged);
            // 
            // btn_Start
            // 
            this.btn_Start.Animated = true;
            this.btn_Start.AnimationHoverSpeed = 0.07F;
            this.btn_Start.AnimationSpeed = 0.03F;
            this.btn_Start.BackColor = System.Drawing.Color.Transparent;
            this.btn_Start.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(189)))));
            this.btn_Start.BorderColor = System.Drawing.Color.Black;
            this.btn_Start.BorderSize = 1;
            this.btn_Start.CheckedBaseColor = System.Drawing.Color.Gray;
            this.btn_Start.CheckedBorderColor = System.Drawing.Color.Black;
            this.btn_Start.CheckedForeColor = System.Drawing.Color.White;
            this.btn_Start.CheckedImage = ((System.Drawing.Image)(resources.GetObject("btn_Start.CheckedImage")));
            this.btn_Start.CheckedLineColor = System.Drawing.Color.DimGray;
            this.btn_Start.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Start.FocusedColor = System.Drawing.Color.Empty;
            this.btn_Start.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.ForeColor = System.Drawing.Color.White;
            this.btn_Start.Image = null;
            this.btn_Start.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_Start.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(137)))), ((int)(((byte)(204)))));
            this.btn_Start.Location = new System.Drawing.Point(6, 144);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(55)))), ((int)(((byte)(153)))));
            this.btn_Start.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btn_Start.OnHoverForeColor = System.Drawing.Color.White;
            this.btn_Start.OnHoverImage = null;
            this.btn_Start.OnHoverLineColor = System.Drawing.Color.Black;
            this.btn_Start.OnPressedColor = System.Drawing.Color.White;
            this.btn_Start.Radius = 15;
            this.btn_Start.Size = new System.Drawing.Size(265, 57);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::GameCaro.Properties.Resources.avata;
            this.pictureBox1.Location = new System.Drawing.Point(5, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(276, 215);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(438, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.newGameToolStripMenuItem.Text = "New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click_1);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(438, 833);
            this.Controls.Add(this.panel_ChessBoard);
            this.Controls.Add(this.panel_menu);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel_menu.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel_IP.ResumeLayout(false);
            this.panel_IP.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_ChessBoard;
        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Guna.UI.WinForms.GunaAdvenceButton btn_Start;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI.WinForms.GunaRadioButton radio_pvc;
        private Guna.UI.WinForms.GunaRadioButton radio_pvp;
        private Guna.UI.WinForms.GunaSwitch swift_lanMode;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaTextBox txb_IP;
        private System.Windows.Forms.Label lable_ip;
        private System.Windows.Forms.Panel panel_IP;
    }
}

