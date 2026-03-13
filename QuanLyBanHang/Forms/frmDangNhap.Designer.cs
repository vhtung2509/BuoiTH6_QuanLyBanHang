namespace QuanLyBanHang.Forms
{
    partial class frmDangNhap
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
            label1 = new Label();
            txtTenDangNhap = new TextBox();
            label2 = new Label();
            txtMatKhau = new TextBox();
            btnDangNhap = new Button();
            btnHuyBo = new Button();
            picHinhAnh = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(429, 108);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 0;
            label1.Text = "Tên đăng nhập:";
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(429, 131);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(295, 27);
            txtTenDangNhap.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(429, 183);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 2;
            label2.Text = "Mật khẩu:";
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(429, 206);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.Size = new Size(295, 27);
            txtMatKhau.TabIndex = 3;
            txtMatKhau.KeyDown += txtMatKhau_KeyDown;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new Point(429, 257);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(110, 29);
            btnDangNhap.TabIndex = 4;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnHuyBo
            // 
            btnHuyBo.Location = new Point(614, 257);
            btnHuyBo.Name = "btnHuyBo";
            btnHuyBo.Size = new Size(110, 29);
            btnHuyBo.TabIndex = 5;
            btnHuyBo.Text = "Hủy bỏ";
            btnHuyBo.UseVisualStyleBackColor = true;
            btnHuyBo.Click += btnHuyBo_Click;
            // 
            // picHinhAnh
            // 
            picHinhAnh.Image = Properties.Resources.log_in;
            picHinhAnh.Location = new Point(157, 108);
            picHinhAnh.Name = "picHinhAnh";
            picHinhAnh.Size = new Size(170, 178);
            picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picHinhAnh.TabIndex = 6;
            picHinhAnh.TabStop = false;
            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(877, 450);
            Controls.Add(picHinhAnh);
            Controls.Add(btnHuyBo);
            Controls.Add(btnDangNhap);
            Controls.Add(txtMatKhau);
            Controls.Add(label2);
            Controls.Add(txtTenDangNhap);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            ((System.ComponentModel.ISupportInitialize)picHinhAnh).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        public TextBox txtTenDangNhap;
        private Label label2;
        public TextBox txtMatKhau;
        private Button btnDangNhap;
        private Button btnHuyBo;
        private PictureBox picHinhAnh;
    }
}