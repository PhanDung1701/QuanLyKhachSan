using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace qlks
{
    public partial class QLNhanvien : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;

        public QLNhanvien()
        {
            InitializeComponent();
            connect = new Connect();
            connect.QueryData("SELECT MaNhanVien, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung, MatKhau, Email FROM tblNhanVien;", dgvNhanVien);
        }

        private void refesh_Form()
        {
            txtMaNhanVien.Text = "";
            txtHoTen.Clear();
            txtEmail.Clear();
            dtNgaySinh.Value = DateTime.Now;
            rbNam.Checked = true;
            cbLoaiNguoiDung.SelectedIndex = 0;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTimKiem.Clear();
            connect.QueryData("SELECT MaNhanVien, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung, MatKhau, Email FROM tblNhanVien;", dgvNhanVien);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Tên nhân viên và mật khẩu không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    string s;
                    if (rbNam.Checked)
                    {
                        s = "Nam";
                    }
                    else if (rbNu.Checked)
                    {
                        s = "Nữ";
                    }
                    else
                    {
                        s = "Khác";
                    }
                    try
                    {
                        connect.ExecuteNonQuery($"INSERT INTO tblNhanVien VALUES(N'{txtHoTen.Text}', '{dtNgaySinh.Value.ToString("yyyy-MM-dd")}', N'{s}', N'{txtEmail.Text}', N'{txtMatKhau.Text}', {cbLoaiNguoiDung.SelectedValue});");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Thêm thành công nhân viên vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Tên nhân viên không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    string s;
                    if (rbNam.Checked)
                    {
                        s = "Nam";
                    }
                    else if (rbNu.Checked)
                    {
                        s = "Nữ";
                    }
                    else
                    {
                        s = "Khác";
                    }
                    try
                    {
                        connect.ExecuteNonQuery($"UPDATE tblNhanVien SET HoTen = N'{txtHoTen.Text}', NgaySinh = '{dtNgaySinh.Value.ToString("yyyy-MM-dd")}', GioiTinh = N'{s}', MatKhau = N'{txtMatKhau.Text}', LoaiNguoiDung = {cbLoaiNguoiDung.SelectedValue}, Email = N'{txtEmail.Text}' WHERE MaNhanVien = {txtMaNhanVien.Text};");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không sửa được nhân viên!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Sửa thành công nhân viên!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblNhanVien WHERE MaNhanVien = {txtMaNhanVien.Text};");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xoá được nhân viên!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sửa thành công nhân viên!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refesh_Form();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refesh_Form();
        }

        private void QLNhanvien_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> LoaiNguoiDungItems = new Dictionary<int, string>();
            LoaiNguoiDungItems.Add(0, "Nhân viên");
            LoaiNguoiDungItems.Add(1, "Quản lý");
            cbLoaiNguoiDung.DataSource = new BindingSource(LoaiNguoiDungItems, null);
            cbLoaiNguoiDung.DisplayMember = "Value";
            cbLoaiNguoiDung.ValueMember = "Key";
            cbLoaiNguoiDung.SelectedIndex = 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT MaNhanVien, HoTen, NgaySinh, GioiTinh, LoaiNguoiDung, MatKhau ,Email FROM tblNhanVien WHERE MaNhanVien LIKE '%{txtTimKiem.Text}%' OR HoTen LIKE N'%{txtTimKiem.Text}%');", dgvNhanVien);

        }

        private void dgvNhanVien_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaNhanVien.Text = dgvNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtHoTen.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
                dtNgaySinh.Value = DateTime.Parse(dgvNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString());
                string s = dgvNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (s == "Nam")
                {
                    rbNam.Checked = true;
                }
                else if (s == "Nữ")
                {
                    rbNu.Checked = true;
                }
                else
                {
                    rbKhac.Checked = true;
                }
                cbLoaiNguoiDung.Text = (bool)dgvNhanVien.Rows[e.RowIndex].Cells[4].Value == false ? "Nhân viên" : "Quản lý";
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dgvNhanVien.Columns[e.ColumnIndex].ValueType == typeof(bool))
                {
                    bool cellValue = (bool)e.Value;
                    e.Value = cellValue ? "Quản lý" : "Nhân viên";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}