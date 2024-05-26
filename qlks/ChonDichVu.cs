using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace qlks
{
    public partial class ChonDichVu : DevExpress.XtraEditors.XtraForm
    {
        private Connect connect;
        private HoaDon hoaDon;
        static BindingList<DichVu> dichVus;
        DichVu rowSourcePhai = new DichVu();
        DichVu rowSourceTrai = new DichVu();
        private bool check;

        public ChonDichVu()
        {
            InitializeComponent();
            connect = new Connect();
        }

        public ChonDichVu(HoaDon hoaDon)
        {
            InitializeComponent();
            connect = new Connect();
            this.hoaDon = hoaDon;
            dichVus = new BindingList<DichVu>();
        }

        static BindingList<DichVu> ConvertDataTableToBindingList(DataTable dataTable)
        {
            BindingList<DichVu> dichVuBindingList = new BindingList<DichVu>(
                dataTable.AsEnumerable().Select(row => new DichVu
                {
                    MaDichVu = row.Field<int>("MaDichVu"),
                    TenDichVu = row.Field<string>("TenDichVu"),
                    GiaDichVu = row.Field<double>("GiaDichVu"), // Chuyển đổi sang kiểu double
                    SoLuongSuDungDich = row.Field<int>("SoLuongSuDungDich"),
                }).ToList()
            );

            return dichVuBindingList;
        }

        public ChonDichVu(HoaDon hoaDon, bool check)
        {
            InitializeComponent();
            connect = new Connect();
            this.hoaDon = hoaDon;
            this.check = check;
            dichVus = ConvertDataTableToBindingList(connect.DataTable($"SELECT MaDichVu, TenDichVu, GiaDichVu, SoLuongSuDungDich FROM tblChiTietHoaDon, tblDichVu WHERE tblDichVu.MaDichVu = tblChiTietHoaDon.MaDichVu AND MaHoaDon = {hoaDon.MaHoaDon}"));
        }

        private void ChonDichVu_Load(object sender, EventArgs e)
        {
            if (check)
            {
                connect.QueryData("SELECT * FROM tblDichVu;", dgvDSDichVu);
                dgvDichVuDaChon.DataSource = dichVus;
                //connect.QueryData($"SELECT tblDichVu.MaDichVu, TenDichVu, GiaDichVu, SoLuongSuDungDich FROM tblChiTietHoaDon, tblDichVu WHERE tblDichVu.MaDichVu = tblChiTietHoaDon.MaDichVu AND MaHoaDon = {hoaDon.MaHoaDon};", dgvDichVuDaChon);
            }
            else
            {
                connect.QueryData("SELECT * FROM tblDichVu;", dgvDSDichVu);
                btnXacNhan.Enabled = false;
            }
            btnPhai.Enabled = false;
            btnTrai.Enabled = false;
        }

        private void dgvDSDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowSourcePhai.MaDichVu = Convert.ToInt32(dgvDSDichVu.Rows[e.RowIndex].Cells[0].Value);
                rowSourcePhai.TenDichVu = dgvDSDichVu.Rows[e.RowIndex].Cells[1].Value.ToString();
                rowSourcePhai.GiaDichVu = Convert.ToInt32(dgvDSDichVu.Rows[e.RowIndex].Cells[2].Value);
                btnPhai.Enabled = true;
            }
        }

        private void btnPhai_Click(object sender, EventArgs e)
        {
            bool found = false;
            foreach (DichVu dichVu in dichVus)
            {
                if (dichVu.MaDichVu == rowSourcePhai.MaDichVu)
                {
                    dichVu.SoLuongSuDungDich = dichVu.SoLuongSuDungDich + 1;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                dichVus.Add(new DichVu
                {
                    MaDichVu = rowSourcePhai.MaDichVu,
                    TenDichVu = rowSourcePhai.TenDichVu,
                    GiaDichVu = rowSourcePhai.GiaDichVu,
                    SoLuongSuDungDich = 1
                });
                //dgvDichVuDaChon.Rows.Add(rowSelected.Cells[0].Value, rowSelected.Cells[1].Value, rowSelected.Cells[2].Value, 1);
            }
            dgvDichVuDaChon.DataSource = null;
            dgvDichVuDaChon.DataSource = dichVus;
        }


        private void btnTrai_Click(object sender, EventArgs e)
        {
            foreach (DichVu dichVu in dichVus)
            {
                if (dichVu.MaDichVu == rowSourceTrai.MaDichVu)
                {
                    dichVu.SoLuongSuDungDich--;

                    if (dichVu.SoLuongSuDungDich == 0)
                    {
                        dichVus.Remove(dichVu);
                    }
                    break;
                }
            }
            dgvDichVuDaChon.DataSource = null;
            dgvDichVuDaChon.DataSource = dichVus;
        }

        private void dgvDichVuDaChon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowSourceTrai.MaDichVu = Convert.ToInt32(dgvDichVuDaChon.Rows[e.RowIndex].Cells[0].Value);
                rowSourceTrai.TenDichVu = dgvDichVuDaChon.Rows[e.RowIndex].Cells[1].Value.ToString();
                rowSourcePhai.GiaDichVu = Convert.ToInt32(dgvDichVuDaChon.Rows[e.RowIndex].Cells[2].Value);
                rowSourceTrai.SoLuongSuDungDich = Convert.ToInt32(dgvDichVuDaChon.Rows[e.RowIndex].Cells[3].Value);
                btnTrai.Enabled = true;
            }
        }

        private void dgvDichVuDaChon_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvDichVuDaChon.Rows.Count > 0)
            {
                btnXacNhan.Enabled = true;
            }
            else
            {
                btnXacNhan.Enabled = false;
            }
        }

        private void dgvDichVuDaChon_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgvDichVuDaChon.Rows.Count > 0)
            {
                btnXacNhan.Enabled = true;
            }
            else
            {
                btnXacNhan.Enabled = false;
            }
        }

        private void btnNoXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (check)
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblChiTietHoaDon WHERE MaHoaDon = {hoaDon.MaHoaDon};");
                }
                else
                {
                    connect.ExecuteNonQuery($"INSERT INTO tblHoaDon (MaKhachHang, MaNhanVienLapHoaDon, MaPhong) VALUES({hoaDon.MaKhachHang}, {hoaDon.MaNhanVienLapHoaDon}, {hoaDon.MaPhong});");
                    connect.ExecuteNonQuery($"UPDATE tblPhong SET TinhTrang = 1 WHERE MaPhong = {hoaDon.MaPhong}");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Thêm thành công phòng vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (check)
                {
                    connect.ExecuteNonQuery($"DELETE FROM tblChiTietHoaDon WHERE MaHoaDon = {hoaDon.MaHoaDon};");
                }
                else
                {
                    connect.ExecuteNonQuery($"INSERT INTO tblHoaDon (MaKhachHang, MaNhanVienLapHoaDon, MaPhong) VALUES ({hoaDon.MaKhachHang}, {hoaDon.MaNhanVienLapHoaDon}, {hoaDon.MaPhong});");
                    hoaDon.MaHoaDon = connect.ExecuteScalar($"SELECT TOP 1 MaHoaDon FROM tblHoaDon ORDER BY MaHoaDon DESC;");
                }
                for (int i = 0; i < dgvDichVuDaChon.Rows.Count; i++)
                {
                    connect.ExecuteNonQuery($"INSERT INTO tblChiTietHoaDon VALUES ({hoaDon.MaHoaDon}, {dgvDichVuDaChon.Rows[i].Cells[0].Value.ToString()}, {dgvDichVuDaChon.Rows[i].Cells[3].Value.ToString()});");
                }
                connect.ExecuteNonQuery($"UPDATE tblPhong SET TinhTrang = 1 WHERE MaPhong = {hoaDon.MaPhong}");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không thêm được vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Thêm thành công phòng vào Database!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}