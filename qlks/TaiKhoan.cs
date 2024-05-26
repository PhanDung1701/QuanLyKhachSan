using DevExpress.XtraEditors.Filtering.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlks
{
    internal class TaiKhoan
    {
        private int maNhanVien;
        private string hoten;
        private DateTime ngaysinh;
        private string gioitinh;
        private string matKhau;
        private bool loainguoidung;
        private string email;

        public TaiKhoan()
        {

        }
        public TaiKhoan(int MaNhanVien, string matKhau, bool loainguoidung)
        {
            this.maNhanVien = MaNhanVien;
            this.MatKhau = matKhau;
            this.loainguoidung = loainguoidung;
        }
        public TaiKhoan(int MaNhanVien, string hoten, DateTime ngaysinh, string gioitinh ,string matKhau, bool loainguoidung, string email)
        {
            this.maNhanVien = MaNhanVien;
            this.hoten = hoten;
            this.ngaysinh = ngaysinh;
            this.gioitinh = gioitinh;
            this.matKhau = matKhau;
            this.loainguoidung = loainguoidung;
            this.email = email; 
        }     
  

        public int MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string HoTen { get => hoten; set => hoten = value; }
        public DateTime NgaySinh { get => ngaysinh; set => ngaysinh = value; }
        public string GioiTinh { get => gioitinh; set => gioitinh = value; }
        public string MatKhau { get => matKhau; set => matKhau= value; }

        public bool LoaiNguoiDung { get => loainguoidung; set => loainguoidung = value; }
        public string Email { get => email; set => email = value; }
    }
    
}
