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
    public partial class SanPham : Form
    {
        public SanPham()
        {
            InitializeComponent();
        }

        connect kn = new connect();

        private void SanPham_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            string truyVan = "SELECT * FROM NhaCungCap";
            dgvSanPham.DataSource = kn.LayDuLieu(truyVan);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            GetData();
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaSP.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                txtMaSP.Text = dgvSanPham.Rows[r].Cells["maSP"].Value.ToString();
                txtTenSP.Text = dgvSanPham.Rows[r].Cells["tenSP"].Value.ToString();
                txtGia.Text = dgvSanPham.Rows[r].Cells["gia"].Value.ToString();
                txtSoLuong.Text = dgvSanPham.Rows[r].Cells["soLuong"].Value.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TrangChu frmHT = new TrangChu();
            frmHT.Show();
            this.Close();
        }
        public void clearText()
        {
            txtMaSP.Text = string.Empty;
            txtTenSP.Text = string.Empty;
            txtGia.Text = string.Empty;
            txtSoLuong.Text = string.Empty;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("INSERT INTO SanPham VALUES('{0}', '{1}', '{2}', '{3}')",
                txtMaSP.Text,
                txtTenSP.Text,
                txtGia.Text,
                txtSoLuong.Text
            );

            if (txtMaSP.Text != "" && txtTenSP.Text != "" && txtGia.Text != "" && txtSoLuong.Text != "")
            {
                if (kn.ThucThi(truyVan))
                {
                    MessageBox.Show("Thêm thành công!");
                    btnLamMoi.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Thêm không thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("update SanPham set maSP = N'{0}, tenSP = {1}, gia = N'{3}', soLuong =N'{4}' where maSP = N{0}",
                txtMaSP.Text,
                txtTenSP.Text,
                txtGia.Text,
                txtSoLuong.Text
            );
            if (kn.ThucThi(truy_van) == true)
            {
                MessageBox.Show("Sửa thành công!");
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Sửa không thành công!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("delete from SanPham where maSP ='{0}'",
                txtMaSP.Text
            );
            if (kn.ThucThi(truy_van) == true)
            {
                MessageBox.Show("Xóa thành công!");
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("select * from SanPham where tenSP like '%{0}%'",
                txtTimKiem.Text
            );
            dgvSanPham.DataSource = kn.LayDuLieu(truy_van);
        }

        private void txtMaSP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
