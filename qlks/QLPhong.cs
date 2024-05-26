using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace qlks
{
    public partial class QLPhong : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;

        public QLPhong()
        {
            InitializeComponent();
            connect = new Connect();
            connect.QueryData("SELECT MaPhong, TenPhong, TenLoaiPhong, TinhTrang FROM tblPhong, tblLoaiPhong WHERE tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong;", dgvPhong);
        }

        private void refesh_Form()
        {
            txtMaPhong.Text = "";
            txtTenPhong.Clear();
            cbMaLoaiPhong.SelectedIndex = 0;
            cbTinhTrang.SelectedIndex = 0;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTimKiem.Clear();
            connect.QueryData("SELECT MaPhong, TenPhong, TenLoaiPhong, TinhTrang FROM tblPhong, tblLoaiPhong WHERE tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong;", dgvPhong);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Tên phòng không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    try
                    {
                        connect.ExecuteNonQuery($"INSERT INTO tblPhong VALUES(N'{txtTenPhong.Text}', {cbMaLoaiPhong.SelectedValue}, {cbTinhTrang.SelectedValue});");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Thêm thành công phòng vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Tên phòng không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    try
                    {
                        connect.ExecuteNonQuery($"UPDATE tblPhong SET TenPhong = N'{txtTenPhong.Text}', MaLoaiPhong = {cbMaLoaiPhong.SelectedValue}, TinhTrang = {cbTinhTrang.SelectedValue} WHERE MaPhong = {txtMaPhong.Text};");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không sửa được phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Sửa thành công phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblPhong WHERE MaLoaiPhong = {txtMaPhong.Text};");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xoá được phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sửa thành công phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refesh_Form();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refesh_Form();
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaPhong.Text = dgvPhong.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenPhong.Text = dgvPhong.Rows[e.RowIndex].Cells[1].Value.ToString();
                cbMaLoaiPhong.Text = dgvPhong.Rows[e.RowIndex].Cells[2].Value.ToString();
                cbTinhTrang.Text = (bool)dgvPhong.Rows[e.RowIndex].Cells[3].Value == false ? "Trống" : "Đã thuê";
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT MaPhong, TenPhong, TenLoaiPhong, TinhTrang FROM tblPhong, tblLoaiPhong WHERE tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND (MaPhong LIKE '%{txtTimKiem.Text}%' OR TenPhong LIKE N'%{txtTimKiem.Text}%');", dgvPhong);
        }

        private void QLPhong_Load(object sender, EventArgs e)
        {
            try
            {
                cbMaLoaiPhong.DataSource = connect.DataTable($"SELECT MaLoaiPhong, TenLoaiPhong FROM tblLoaiPhong;");
                cbMaLoaiPhong.DisplayMember = "TenLoaiPhong";
                cbMaLoaiPhong.ValueMember = "MaLoaiPhong";
                cbMaLoaiPhong.DropDownStyle = ComboBoxStyle.DropDown;
                cbMaLoaiPhong.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbMaLoaiPhong.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Chưa có dữ liệu loại phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Dictionary<int, string> tinhTrangItems = new Dictionary<int, string>();
            tinhTrangItems.Add(0, "Trống");
            tinhTrangItems.Add(1, "Đã thuê");
            cbTinhTrang.DataSource = new BindingSource(tinhTrangItems, null);
            cbTinhTrang.DisplayMember = "Value";
            cbTinhTrang.ValueMember = "Key";
            cbTinhTrang.SelectedIndex = 0;
        }

        private void dgvPhong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dgvPhong.Columns[e.ColumnIndex].ValueType == typeof(bool))
                {
                    bool cellValue = (bool)e.Value;
                    e.Value = cellValue ? "Đã thuê" : "Trống";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}