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
    public partial class NhaCungCap : Form
    {
        public NhaCungCap()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            string truyVan = "SELECT * FROM NhaCungCap";
            dgvNhaCungCap.DataSource = kn.LayDuLieu(truyVan);
        }

        private void dgvNhaCungCap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaNCC.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                txtMaNCC.Text = dgvNhaCungCap.Rows[r].Cells["maNCC"].Value.ToString();
                txtTenNCC.Text = dgvNhaCungCap.Rows[r].Cells["tenNCC"].Value.ToString();
                txtSDT.Text = dgvNhaCungCap.Rows[r].Cells["sdt"].Value.ToString();
                txtDiaChi.Text = dgvNhaCungCap.Rows[r].Cells["diaChi"].Value.ToString();
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
            txtMaNCC.Text = string.Empty;
            txtTenNCC.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            GetData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("INSERT INTO NhaCungCap VALUES('{0}', '{1}', '{2}', '{3}')", 
                txtMaNCC.Text,
                txtTenNCC.Text,
                txtSDT.Text,
                txtDiaChi.Text
            );

            if (txtMaNCC.Text != "" && txtTenNCC.Text != "" && txtDiaChi.Text != "" && txtSDT.Text != "")
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
            string truy_van = string.Format("update NhaCungCap set maNCC = N'{0}, tenNCC = {1}, sdt = N'{2}', diaChi =N'{3}' where maNCC = N{0}",
                txtMaNCC.Text,
                txtTenNCC.Text,
                txtSDT.Text,
                txtDiaChi.Text
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
            string truy_van = string.Format("delete from NhaCungCap where maNCC ='{0}'",
                txtMaNCC.Text
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

        private void button5_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("select * from SanPham where tenNCC like '%{0}%'",
                txtTimKiem.Text
            );
            dgvNhaCungCap.DataSource = kn.LayDuLieu(truy_van);
        }
    }
}
