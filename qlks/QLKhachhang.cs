using System;
using System.Windows.Forms;

namespace qlks
{
    public partial class QLKhachhang : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;
        public QLKhachhang()
        {
            InitializeComponent();
            connect = new Connect();
            connect.QueryData("SELECT * FROM tblKhachHang;", dgvKhachHang);
        }

        private void refesh_Form()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Clear();
            txtQuocTich.Clear();
            txtSoChungMinhThu.Clear();
            txtSoDienThoai.Clear();
            rdNam.Checked = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTimKiem.Clear();
            connect.QueryData("SELECT * FROM tblKhachHang;", dgvKhachHang);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenKhachHang.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(txtSoChungMinhThu.Text))
            {
                MessageBox.Show("Số chứng minh không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(txtQuocTich.Text))
            {
                MessageBox.Show("Quốc tịch không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string s = "";
                        if (rdNam.Checked)
                        {
                            s = "Nam";
                        }
                        else if (rdNu.Checked)
                        {
                            s = "Nữ";
                        }
                        else
                        {
                            s = "Khác";
                        }
                        connect.ExecuteNonQuery($"INSERT INTO tblKhachHang VALUES(N'{txtTenKhachHang.Text}', N'{txtSoChungMinhThu.Text}', N'{txtQuocTich.Text}', N'{s}', N'{txtSoDienThoai.Text}');");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Thêm thành công khách hàng vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblKhachHang WHERE MaKhachHang = {txtMaKhachHang.Text};");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xoá được khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sửa thành công khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refesh_Form();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenKhachHang.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(txtSoChungMinhThu.Text))
            {
                MessageBox.Show("Số chứng minh không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(txtQuocTich.Text))
            {
                MessageBox.Show("Quốc tịch không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(txtSoDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string s = "";
                        if (rdNam.Checked)
                        {
                            s = "Nam";
                        }
                        else if (rdNu.Checked)
                        {
                            s = "Nữ";
                        }
                        else
                        {
                            s = "Khác";
                        }
                        connect.ExecuteNonQuery($"UPDATE tblKhachHang SET TenKhachHang = N'{txtTenKhachHang.Text}', SoChungMinhThu = N'{txtSoChungMinhThu.Text}', QuocTich = N'{txtQuocTich.Text}', GioiTinh = N'{s}', SoDienThoai = N'{txtSoDienThoai.Text}' WHERE MaKhachHang = {txtMaKhachHang.Text}");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không sửa được khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Sửa thành công khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refesh_Form();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT * FROM tblKhachHang WHERE MaKhachHang LIKE '%{txtTimKiem.Text}%' OR TenKhachHang LIKE N'%{txtTimKiem.Text}%' OR SoChungMinhThu LIKE N'%{txtTimKiem.Text}%' OR QuocTich LIKE N'%{txtTimKiem.Text}%' OR GioiTinh LIKE N'%{txtTimKiem.Text}%' OR SoDienThoai LIKE N'%{txtTimKiem.Text}%';", dgvKhachHang);
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSoChungMinhThu.Text = dgvKhachHang.Rows[e.RowIndex].Cells[2].Value.ToString();
                string s = dgvKhachHang.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (s == "Nam")
                {
                    rdNam.Checked = true;
                }
                else if (s == "Nữ")
                {
                    rdNu.Checked = true;
                }
                else
                {
                    rdKhac.Checked = true;
                }
                txtQuocTich.Text = dgvKhachHang.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSoDienThoai.Text = dgvKhachHang.Rows[e.RowIndex].Cells[5].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }
    }
}