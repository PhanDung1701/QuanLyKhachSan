using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace qlks
{
    public partial class Laphoadon : XtraForm
    {
        private Connect connect;
        private DataTable data;
        public Laphoadon()
        {
            InitializeComponent();
            connect = new Connect();
        }

        public Laphoadon(string id)
        {
            InitializeComponent();
            connect = new Connect();
            txtTienDichVu.Text = connect.DataTable($"SELECT SUM(SoLuongSuDungDich * GiaDichVu) FROM tblChiTietHoaDon, tblDichVu WHERE tblChiTietHoaDon.MaDichVu = tblDichVu.MaDichVu AND MaHoaDon = {id};").Rows[0][0].ToString();
            data = connect.DataTable($"SELECT MaHoaDon, TenKhachHang, NgayThuePhong, HoTen, TenPhong, GiaPhongMotNgay FROM tblHoaDon, tblNhanVien, tblKhachHang, tblPhong, tblLoaiPhong WHERE tblHoaDon.MaNhanVienLapHoaDon = tblNhanVien.MaNhanVien AND tblHoaDon.MaKhachHang = tblKhachHang.MaKhachHang AND tblHoaDon.MaPhong = tblPhong.MaPhong AND tblPhong.MaLoaiPhong = tblLoaiPhong.MaLoaiPhong AND DaThanhToan = 0 AND MaHoaDon = {id};");
            txtMaHoaDon.Text = data.Rows[0][0].ToString();
            txtTenKhachHang.Text = data.Rows[0][1].ToString();
            DateTime ngayThuePhong = DateTime.Parse(data.Rows[0][2].ToString());
            txtNgayThue.Text = ngayThuePhong.ToString("dd/MM/yyyy h:mm:ss tt");
            txtTenNhanVien.Text = data.Rows[0][3].ToString();
            txtTenPhong.Text = data.Rows[0][4].ToString();
            txtGiaPhong.Text = data.Rows[0][5].ToString();
            dtpNgayTra.Value = DateTime.Now;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Xác nhận thanh toán hoá đơn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                connect.ExecuteNonQuery($"UPDATE tblHoaDon SET ThanhTien = {txtTienPhaiTra.Text}, NgayTraPhong = '{dtpNgayTra.Value.ToString()}', DaThanhToan = 1 WHERE MaHoaDon = {txtMaHoaDon.Text};");
                this.Close();
            }
        }

        private void dtpNgayTra_ValueChanged(object sender, EventArgs e)
        {
            // Chuyển đổi chuỗi ngày thuê thành đối tượng DateTime
            DateTime NgayThue;
            if (DateTime.TryParseExact(txtNgayThue.Text, "dd/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out NgayThue))
            {
                // So sánh ngày trả với ngày thuê
                if (dtpNgayTra.Value > NgayThue)
                {
                    // Tính số ngày đã thuê
                    TimeSpan soNgayThue = dtpNgayTra.Value.Date - NgayThue.Date;

                    // Chuyển đổi giá phòng từ kiểu string sang kiểu số thực
                    double giaPhong;
                    if (double.TryParse(txtGiaPhong.Text, out giaPhong))
                    {
                        // Tính tiền thuê phòng
                        double tienThuePhong = soNgayThue.TotalDays * giaPhong;

                        // Hiển thị kết quả trên TextBox
                        txtTienThuePhong.Text = tienThuePhong.ToString();
                    }
                    else
                    {
                        // Xử lý nếu không thể chuyển đổi giá phòng thành công
                        MessageBox.Show("Giá phòng không hợp lệ");
                    }
                }
                else
                {
                    // Xử lý nếu ngày trả không hợp lệ
                    MessageBox.Show("Ngày trả phòng phải sau ngày thuê");
                }
            }
            else
            {
                // Xử lý nếu không thể chuyển đổi ngày thuê thành công
                MessageBox.Show("Ngày thuê không hợp lệ");
            }
            txtTienPhaiTra.Text = (double.Parse(txtTienDichVu.Text) + double.Parse(txtTienThuePhong.Text)).ToString();
        }
    }
}
