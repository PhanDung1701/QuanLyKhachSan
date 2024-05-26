namespace qlks
{
    partial class QuenMatKhau
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
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.txtdangky = new Sunny.UI.UITextBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiButton1 = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(145, 278);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(160, 23);
            this.uiLabel1.TabIndex = 4;
            this.uiLabel1.Text = "Email Đăng Ký :";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtdangky
            // 
            this.txtdangky.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdangky.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtdangky.Location = new System.Drawing.Point(340, 278);
            this.txtdangky.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtdangky.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtdangky.Name = "txtdangky";
            this.txtdangky.Padding = new System.Windows.Forms.Padding(5);
            this.txtdangky.ShowText = false;
            this.txtdangky.Size = new System.Drawing.Size(273, 29);
            this.txtdangky.TabIndex = 5;
            this.txtdangky.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtdangky.Watermark = "";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::qlks.Properties.Resources.userden;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(237, 25);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(300, 200);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 6;
            this.guna2PictureBox1.TabStop = false;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel2.Location = new System.Drawing.Point(145, 355);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(283, 23);
            this.uiLabel2.TabIndex = 7;
            this.uiLabel2.Text = "Kết Quả :";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiButton1
            // 
            this.uiButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiButton1.Location = new System.Drawing.Point(280, 431);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.RectColor = System.Drawing.Color.LavenderBlush;
            this.uiButton1.Size = new System.Drawing.Size(184, 35);
            this.uiButton1.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton1.TabIndex = 8;
            this.uiButton1.Text = "Lấy Lại Mật Khẩu";
            this.uiButton1.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 548);
            this.Controls.Add(this.uiButton1);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.txtdangky);
            this.Name = "QuenMatKhau";
            this.Text = "From Được Thực Hiện Bởi Nhóm 4 !";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtdangky;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton uiButton1;
    }
}