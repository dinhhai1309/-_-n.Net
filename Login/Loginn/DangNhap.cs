using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("SELECT * FROM TaiKhoan WHERE taiKhoan = '{0}' AND matKhau = '{1}'", txtTaiKhoan.Text, txtMatKhau.Text);
            DataTable tb = kn.LayDuLieu(truyVan);
            if(tb.Rows.Count >= 1 )
            {
                MessageBox.Show("Đăng nhập thành công!");
                TrangChu frmHeThong = new TrangChu();
                frmHeThong.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy frmRegister = new DangKy();
            frmRegister.Show();
            this.Hide();
        }
    }
}
