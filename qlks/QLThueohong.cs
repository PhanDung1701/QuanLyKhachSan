using System;
using System.Data;
using System.Windows.Forms;

namespace qlks
{
    public partial class QLThueohong : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;
        private HoaDon hoaDon = new HoaDon();

        public QLThueohong()
        {
            InitializeComponent();
            connect = new Connect();
        }

        private void QLThueohong_Load(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            connect.QueryData($"SELECT MaHoaDon, tblHoaDon.MaKhachHang, TenKhachHang, tblPhong.MaPhong, TenPhong,TenLoaiPhong, GiaPhongMotNgay, NgayThuePhong, SoChungMinhThu FROM tblHoaDon, tblKhachHang, tblPhong, tblLoaiPhong WHERE tblHoaDon.MaKhachHang = tblKhachHang.MaKhachHang AND tblHoaDon.MaPhong = tblPhong.MaPhong AND tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND DaThanhToan = 0;", dgvDSThuePhong);
            connect.QueryData($"SELECT tblPhong.MaPhong, TenPhong, TenLoaiPhong, GiaPhongMotNgay FROM tblPhong, tblLoaiPhong WHERE tblLoaiPhong.MaLoaiPhong = tblPhong.MaLoaiPhong AND TinhTrang = 0", dgvDSPhongTrong);
        }

        private void txtTimKiemPhong_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT MaHoaDon, tblHoaDon.MaKhachHang, TenKhachHang, tblPhong.MaPhong, TenPhong,TenLoaiPhong, GiaPhongMotNgay, NgayThuePhong FROM tblHoaDon, tblKhachHang, tblPhong, tblLoaiPhong WHERE tblHoaDon.MaKhachHang = tblKhachHang.MaKhachHang AND tblHoaDon.MaPhong = tblPhong.MaPhong AND tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND DaThanhToan = 0 AND TenPhong LIKE N'%{txtTimKiemPhong.Text}%';", dgvDSThuePhong);
            connect.QueryData($"SELECT tblPhong.MaPhong, TenPhong, TenLoaiPhong, GiaPhongMotNgay FROM tblPhong, tblLoaiPhong WHERE tblLoaiPhong.MaLoaiPhong = tblPhong.MaLoaiPhong AND TinhTrang = 0 AND TenPhong LIKE N'%{txtTimKiemPhong.Text}%';", dgvDSPhongTrong);
        }

        private void txtMaCCCD_TextChanged(object sender, EventArgs e)
        {
            DataTable data = connect.DataTable($"SELECT MaKhachHang, TenKhachHang FROM tblKhachHang WHERE SoChungMinhThu LIKE N'{txtMaCCCD.Text}';");

            if (data.Rows.Count > 0)
            {
                txtMaKhachHang.Text = data.Rows[0]["MaKhachHang"].ToString();
                txtTenKhachHang.Text = data.Rows[0]["TenKhachHang"].ToString();
            }
            else
            {
                txtMaKhachHang.Text = "";
                txtTenKhachHang.Text = "";
            }

        }

        private void dgvDSPhongTrong_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaPhong.Text = dgvDSPhongTrong.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();
                txtTenPhong.Text = dgvDSPhongTrong.Rows[e.RowIndex].Cells["TenPhong"].Value.ToString();
                txtLoaiPhong.Text = dgvDSPhongTrong.Rows[e.RowIndex].Cells["TenLoaiPhong"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn phòng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtMaKhachHang.Text == "")
            {
                MessageBox.Show("Chưa tìm thấy khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thuê phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                hoaDon.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                hoaDon.MaNhanVienLapHoaDon = MaNhanVien.manhanvien;
                hoaDon.MaPhong = int.Parse(txtMaPhong.Text);
                ChonDichVu chonDichVu = new ChonDichVu(hoaDon);
                chonDichVu.ShowDialog();
                btnLamMoi_Click(sender, e);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            QLThueohong_Load(sender, e);
            txtMaCCCD.Text = "";
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtLoaiPhong.Text = "";
        }

        private void dgvDSThuePhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDon.MaHoaDon = (int)dgvDSThuePhong.Rows[e.RowIndex].Cells["MaHoaDon"].Value;
                txtMaKhachHang.Text = dgvDSThuePhong.Rows[e.RowIndex].Cells["MaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = dgvDSThuePhong.Rows[e.RowIndex].Cells["TenKhachHang"].Value.ToString();
                txtMaPhong.Text = dgvDSThuePhong.Rows[e.RowIndex].Cells["MaPhong1"].Value.ToString();
                txtTenPhong.Text = dgvDSThuePhong.Rows[e.RowIndex].Cells["TenPhong1"].Value.ToString();
                txtLoaiPhong.Text = dgvDSThuePhong.Rows[e.RowIndex].Cells["TenLoaiPhong1"].Value.ToString();
                txtMaCCCD.Text = dgvDSThuePhong.Rows[e.RowIndex].Cells["SoChungMinhThu"].Value.ToString();
                btnXoa.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xoá phòng thuê này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblHoaDon WHERE MaHoaDon = {hoaDon.MaHoaDon};");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xoá được phòng thuê!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Sửa thành công phòng thuê!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(sender, e);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKhachHang.Text == "")
            {
                MessageBox.Show("Chưa tìm thấy khách hàng!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa phòng thuê này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                hoaDon.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                hoaDon.MaNhanVienLapHoaDon = MaNhanVien.manhanvien;
                hoaDon.MaPhong = int.Parse(txtMaPhong.Text);
                ChonDichVu chonDichVu = new ChonDichVu(hoaDon, true);
                chonDichVu.ShowDialog();
                btnLamMoi_Click(sender, e);
            }
        }
    }
}