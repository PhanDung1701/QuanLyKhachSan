using System;
using System.Windows.Forms;

namespace qlks
{
    public partial class TraPhong : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;

        public TraPhong()
        {
            InitializeComponent();
            connect = new Connect();
            connect.QueryData($"SELECT MaHoaDon, tblHoaDon.MaKhachHang, TenKhachHang, tblPhong.MaPhong, TenPhong FROM tblHoaDon, tblKhachHang, tblPhong, tblLoaiPhong WHERE tblHoaDon.MaKhachHang = tblKhachHang.MaKhachHang AND tblHoaDon.MaPhong = tblPhong.MaPhong AND tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND DaThanhToan = 0;", dgvDSThuePhong);
        }

        private void txtTimKiemPhong_TextChanged(object sender, EventArgs e)
        {
            connect.QueryData($"SELECT MaHoaDon, tblHoaDon.MaKhachHang, TenKhachHang, tblPhong.MaPhong, TenPhong FROM tblHoaDon, tblKhachHang, tblPhong, tblLoaiPhong WHERE tblHoaDon.MaKhachHang = tblKhachHang.MaKhachHang AND tblHoaDon.MaPhong = tblPhong.MaPhong AND tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND DaThanhToan = 0 AND TenPhong LIKE N'%{txtTimKiemPhong.Text}%';", dgvDSThuePhong);
        }

        private void dgvDSThuePhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvDSThuePhong.Columns["XacNhan"].Index)
            {
                DialogResult dialogResult = MessageBox.Show("Xác nhận thanh toán hoá đơn này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    new Laphoadon(dgvDSThuePhong.Rows[e.RowIndex].Cells["MaHoaDon"].Value.ToString()).ShowDialog();
                    connect.QueryData($"SELECT MaHoaDon, tblHoaDon.MaKhachHang, TenKhachHang, tblPhong.MaPhong, TenPhong FROM tblHoaDon, tblKhachHang, tblPhong, tblLoaiPhong WHERE tblHoaDon.MaKhachHang = tblKhachHang.MaKhachHang AND tblHoaDon.MaPhong = tblPhong.MaPhong AND tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND DaThanhToan = 0;", dgvDSThuePhong);
                }
            }
        }
    }
}