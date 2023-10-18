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
    public partial class ChiTietHoaDon : Form
    {
        public ChiTietHoaDon()
        {
            InitializeComponent();
        }
        connect kn = new connect();
        private void ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LayMaLoaiHD();
            LayMaLoaiSP();
            GetData();
        }

        public void LayMaLoaiHD()
        {
            string truy_van = "select * from hoaDon";
            cmbMaHD.DataSource = kn.LayDuLieu(truy_van);
            cmbMaHD.DisplayMember = "maHoaDon";
            cmbMaHD.ValueMember = "maHoaDon";
        }
        public void LayMaLoaiSP()
        {
            string truy_van = "select * from sanPham";
            cmbMaSP.DataSource = kn.LayDuLieu(truy_van);
            cmbMaSP.DisplayMember = "tenSP";
            cmbMaSP.ValueMember = "maSP";
        }


        public void GetData()
        {
            string truyVan = "SELECT CTHoaDon.maHoaDon, CTHoaDon.maSP, CTHoaDon.soLuong, CTHoaDon.giamGia,(CTHoaDon.soLuong * SanPham.gia - CTHoaDon.giamGia) AS tongGia FROM CTHoaDon INNER JOIN SanPham ON CTHoaDon.maSP = SanPham.maSP;";
            dgvChiTietHoaDon.DataSource = kn.LayDuLieu(truyVan);
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("select * from CTHoaDon where maHoaDon like '%{0}%' or maSP like '%{0}%'",
                txtTimKiem.Text
            );
            dgvChiTietHoaDon.DataSource = kn.LayDuLieu(truy_van);
        }
        private void btnTongTien_Click(object sender, EventArgs e)
        {

        }
        public void clearText()
        {
            cmbMaHD.SelectedValue = "";
            cmbMaSP.SelectedValue = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("insert into CTHoaDon values('{0}','{1}','{2}','{3}',0)",
                cmbMaHD.SelectedValue,
                cmbMaSP.SelectedValue,
                txtSoLuong.Text,
                txtGiamGia.Text
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
            string truy_van = string.Format("update CTHoaDon set  soLuong='{0}', giamGia='{1}', tongGia = 0 where maHoaDon='{2}' and maSP='{3}'",
                txtSoLuong.Text,
                txtGiamGia.Text,
                cmbMaHD.SelectedValue,
                cmbMaSP.SelectedValue
                
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
            string truy_van = string.Format("delete from CTHoaDon where maHoaDon='{0}'", cmbMaHD.SelectedValue);
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            HoaDon frmHT = new HoaDon();
            frmHT.Show();
            this.Close();

        }

        private void dgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                cmbMaHD.Text = dgvChiTietHoaDon.Rows[r].Cells["maHoaDon"].Value.ToString();
                cmbMaSP.Text = dgvChiTietHoaDon.Rows[r].Cells["maSP"].Value.ToString();
                txtSoLuong.Text = dgvChiTietHoaDon.Rows[r].Cells["soLuong"].Value.ToString();
                txtGiamGia.Text = dgvChiTietHoaDon.Rows[r].Cells["giamGia"].Value.ToString();
            }
        }

    }
}
