using System;
using System.Data;
using System.Windows.Forms;

namespace qlks
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;
        public Login()
        {
            InitializeComponent();
            connect = new Connect();
        }

        private void cbAnHienMK_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cbAnHienMK.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnDangNhap_Click(object sender, System.EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable dataTable = connect.DataTable($"SELECT MaNhanVien, LoaiNguoiDung FROM tblNhanVien WHERE MaNhanVien = N'{txtUsername.Text.Trim()}' AND MatKhau = N'{txtPassword.Text.Trim()}'");
                if (dataTable.Rows.Count == 1 && Convert.ToBoolean(dataTable.Rows[0][1].ToString()))
                {
                    // Khởi tạo và hiển thị form Home
                    MaNhanVien.manhanvien = int.Parse(dataTable.Rows[0][0].ToString());
                    HomeForm homeForm = new HomeForm();
                    homeForm.FormClosed += HomeForm_FormClosed; // Gắn sự kiện FormClosed
                    homeForm.Show();

                    // Ẩn và đóng form đăng nhập
                    this.Hide();
                }
                else if (dataTable.Rows.Count == 1 && !Convert.ToBoolean(dataTable.Rows[0][1].ToString()))
                {
                    // Khởi tạo và hiển thị form Home
                    MaNhanVien.manhanvien = int.Parse(dataTable.Rows[0][0].ToString());
                    HomeForm homeForm = new HomeForm(false);
                    homeForm.FormClosed += HomeForm_FormClosed; // Gắn sự kiện FormClosed
                    homeForm.Show();
                    // Ẩn và đóng form đăng nhập
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không chính xác!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Đóng form đăng nhập
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void LinkLbQuenMk_Click(object sender, EventArgs e)
        {
            new QuenMatKhau().ShowDialog();
        }
    }
}