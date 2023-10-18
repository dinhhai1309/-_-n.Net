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
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void HoaDon_Load(object sender, EventArgs e)
        {
            LayMaLoaiNV();
            GetData();
        }

        public void LayMaLoaiNV()
        {
            string truy_van = "select * from NhanVien";
            cmbMaNV.DataSource = kn.LayDuLieu(truy_van);
            cmbMaNV.DisplayMember = "tenNV";
            cmbMaNV.ValueMember = "maNV";
        }

        public void GetData()
        {
            string truyVan = "SELECT hoaDon.maHoaDon, hoaDon.ngayBan, hoaDon.maNV, ( SELECT SUM(CTHoaDon.soLuong * SanPham.gia - CTHoaDon.giamGia) FROM CTHoaDon INNER JOIN SanPham ON CTHoaDon.maSP = SanPham.maSP WHERE CTHoaDon.maHoaDon = hoaDon.maHoaDon) AS tongHoaDon FROM hoaDon;";
            dgvHienThi.DataSource = kn.LayDuLieu(truyVan);
        }

        private void dgvHienThi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaHD.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                txtMaHD.Text = dgvHienThi.Rows[r].Cells["maHoaDon"].Value.ToString();
                dtNgayBan.Text = dgvHienThi.Rows[r].Cells["ngayBan"].Value.ToString();
                cmbMaNV.Text = dgvHienThi.Rows[r].Cells["maNV"].Value.ToString();
            }
        }

        public void clearText()
        {
            txtMaHD.Text = "";
            dtNgayBan.Text = "";
            cmbMaNV.SelectedValue = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("insert into hoaDon values('{0}','{1}','{2}',0)",
            txtMaHD.Text,
            dtNgayBan.Text,
            cmbMaNV.SelectedValue
            );
            if (kn.ThucThi(truy_van) == true)
            {
                MessageBox.Show("Thêm thành công!");
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            GetData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("update hoaDon set ngayBan='{0}', maNV='{1}', tongHoaDon='{4}' where maHoaDon='{2}'",
                dtNgayBan.Text,
                cmbMaNV.SelectedValue,
                txtMaHD.Text
            );
            if (kn.ThucThi(truy_van) == true)
            {
                MessageBox.Show("Sửa thành công!");
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("delete from hoaDon where maHoaDon='{0}'",txtMaHD.Text);
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
            string truy_van = string.Format("select * from maHoaDon where maHoaDon like '%{0}%' or ngayBan like '%{0}%' or maNV like '%{0}%'",
                txtTimKiem.Text
            );
            dgvHienThi.DataSource = kn.LayDuLieu(truy_van);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            TrangChu frmHT = new TrangChu();
            frmHT.Show();
            this.Close();
        }

        private void btnCTHoaDon_Click(object sender, EventArgs e)
        {
            ChiTietHoaDon frmCTHD = new ChiTietHoaDon();
            frmCTHD.Show();
            this.Hide();
        }
    }
}
