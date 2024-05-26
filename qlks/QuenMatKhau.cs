using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace qlks
{
    public partial class QuenMatKhau : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;

        public QuenMatKhau()
        {
            InitializeComponent();
            uiLabel2.Text = "";
            connect = new Connect();
        }
        private void uiButton1_Click(object sender, EventArgs e)
        {
            string email = txtdangky.Text;
            if (email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập lại Email đăng ký !");
            }
            else
            {
                string query = "Select * from tblNhanVien where Email = '" + email + "'";
                DataTable data = connect.DataTable(query);
                if (data.Rows.Count != 0)
                {
                    uiLabel2.ForeColor = Color.Blue;
                    uiLabel2.Text = $"Mật Khẩu: {data.Rows[0][5].ToString()}";

                }
                else
                {
                    uiLabel2.ForeColor = Color.Red;
                    uiLabel2.Text = "Email của bạn chưa chính xác !";
                }
            }
        }
    }
}