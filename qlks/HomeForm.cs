using DevExpress.XtraBars;
using System;
using System.Windows.Forms;

namespace qlks
{
    public partial class HomeForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        public HomeForm(bool check)
        {
            InitializeComponent();
            btnQLNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            rbBaoCaoThongKe.Visible = check;
        }

        private void OpenForm(Type typeForm)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.GetType() == typeForm)
                {
                    form.Activate();
                    return;
                }
            }
            Form f = (Form)Activator.CreateInstance(typeForm);
            f.MdiParent = this;
            f.Show();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLPhong));
        }

        private void btnQLNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLNhanvien));
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLKhachhang));
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLDichvu));
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLloaiphong));
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLThueohong));
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(TraPhong));
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(typeof(QLHoaDon));
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}