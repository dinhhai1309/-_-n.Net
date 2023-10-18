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
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void button2_Click(object sender, EventArgs e)
        {
            DangNhap frmLogin = new DangNhap();
            frmLogin.Show();
            this.Close();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string taikhoan = txtTaiKhoan.Text;
            string matkhau = txtMatKhau.Text;
            string nhaplai = txtNhapLaiMatKhau.Text;

            try
            {
                if (String.IsNullOrEmpty(taikhoan))
                {
                    MessageBox.Show("Chưa nhập tài khoản!");
                }
                else if (String.IsNullOrEmpty(matkhau))
                {
                    MessageBox.Show("Chưa nhập mật khẩu!");
                }
                else if (String.IsNullOrEmpty(nhaplai))
                {
                    MessageBox.Show("Xác nhận mật khẩu!");
                }
                else if (nhaplai != matkhau)
                {
                    MessageBox.Show("nhập lại mật khẩu không đúng!");
                }
                else
                {
                    string register = string.Format(@"INSERT INTO TaiKhoan VALUES('{0}','{1}')", taikhoan, matkhau);
                    kn.ThucThi(register);
                    MessageBox.Show("Đăng ký thành công");
                    DangNhap frmLogin = new DangNhap();
                    frmLogin.Show();
                    this.Close();
                }
            }
            catch (Exception ex) { }
            {
                MessageBox.Show("Error");
            }
        }
    }
}
