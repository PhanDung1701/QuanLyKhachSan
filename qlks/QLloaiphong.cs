using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace qlks
{
    public partial class QLloaiphong : XtraForm
    {
        private Connect connect;

        public QLloaiphong()
        {
            InitializeComponent();
            connect = new Connect();
            connect.QueryData("SELECT * FROM tblLoaiPhong;", dgvLoaiPhong);
        }

        private void refesh_Form()
        {
            txtMaLoaiPhong.Text = "";
            txtTenLoaiPhong.Clear();
            numGiaPhongMotNgay.Value = 0;
            txtMoTa.Clear();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTimKiem.Clear();
            connect.QueryData("SELECT * FROM tblLoaiPhong;", dgvLoaiPhong);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenLoaiPhong.Text) || string.IsNullOrEmpty(txtMoTa.Text))
            {
                MessageBox.Show("Tên loại phòng và mô tả phòng không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        connect.ExecuteNonQuery($"INSERT INTO tblLoaiPhong VALUES(N'{txtTenLoaiPhong.Text}', {numGiaPhongMotNgay.Value}, N'{txtMoTa.Text}');");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Thêm thành công loại phòng vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenLoaiPhong.Text) || string.IsNullOrEmpty(txtMoTa.Text))
            {
                MessageBox.Show("Tên loại phòng và mô tả phòng không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa loại phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    try
                    {
                        connect.ExecuteNonQuery($"UPDATE tblLoaiPhong SET TenLoaiPhong = N'{txtTenLoaiPhong.Text}', GiaPhongMotNgay = {numGiaPhongMotNgay.Value}, MoTa = N'{txtMoTa.Text}' WHERE MaLoaiPhong = {txtMaLoaiPhong.Text};");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không sửa được loại phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Sửa thành công loại phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void dgvLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaLoaiPhong.Text = dgvLoaiPhong.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenLoaiPhong.Text = dgvLoaiPhong.Rows[e.RowIndex].Cells[1].Value.ToString();
                numGiaPhongMotNgay.Value = decimal.Parse(dgvLoaiPhong.Rows[e.RowIndex].Cells[2].Value.ToString());
                txtMoTa.Text = dgvLoaiPhong.Rows[e.RowIndex].Cells[3].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refesh_Form();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá loại phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblLoaiPhong WHERE MaLoaiPhong = {txtMaLoaiPhong.Text};");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xoá được loại phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sửa thành công loại phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refesh_Form();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT * FROM tblLoaiPhong WHERE MaLoaiPhong LIKE '%{txtTimKiem.Text}%' OR TenLoaiPhong LIKE N'%{txtTimKiem.Text}%' OR GiaPhongMotNgay LIKE '%{txtTimKiem.Text}%' OR MoTa LIKE N'%{txtTimKiem.Text}%';", dgvLoaiPhong);
        }

    }
}