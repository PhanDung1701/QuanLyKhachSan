using System;
using System.Windows.Forms;

namespace qlks
{
    public partial class QLDichvu : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;

        public QLDichvu()
        {
            InitializeComponent();
            connect = new Connect();
            connect.QueryData("SELECT * FROM tblDichVu;", dgvDichVu);
        }

        private void refesh_Form()
        {
            txtMaDichVu.Text = "";
            txtTenDichVu.Clear();
            numGiaDichVu.Value = 0;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTimKiem.Clear();
            connect.QueryData("SELECT * FROM tblDichVu;", dgvDichVu);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDichVu.Text))
            {
                MessageBox.Show("Tên dịch vụ không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        connect.ExecuteNonQuery($"INSERT INTO tblDichVu VALUES(N'{txtTenDichVu.Text}', {numGiaDichVu.Value});");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Thêm thành công dịch vụ vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDichVu.Text))
            {
                MessageBox.Show("Tên dịch vụ không được để trống!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa dịch vụ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    try
                    {
                        connect.ExecuteNonQuery($"UPDATE tblDichVu SET TenDichVu = N'{txtTenDichVu.Text}', GiaDichVu = {numGiaDichVu.Value} WHERE MaDichVu = {txtMaDichVu.Text};");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi không sửa được dịch vụ!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Sửa thành công dịch vụ!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refesh_Form();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá dịch vụ này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            else
            {
                try
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblDichVu WHERE MaDichVu = {txtMaDichVu.Text};");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xoá được dịch vụ!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sửa thành công dịch vụ!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refesh_Form();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refesh_Form();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT * FROM tblDichVu WHERE MaDichVu LIKE '%{txtTimKiem.Text}%' OR TenDichVu LIKE N'%{txtTimKiem.Text}%' OR GiaDichVu LIKE '%{txtTimKiem.Text}%';", dgvDichVu);
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaDichVu.Text = dgvDichVu.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenDichVu.Text = dgvDichVu.Rows[e.RowIndex].Cells[1].Value.ToString();
                numGiaDichVu.Value = decimal.Parse(dgvDichVu.Rows[e.RowIndex].Cells[2].Value.ToString());
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void QLDichvu_Load(object sender, EventArgs e)
        {

        }
    }
}