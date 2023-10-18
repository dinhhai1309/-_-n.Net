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
    public partial class CTPN : Form
    {
        public CTPN()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void CTPN_Load(object sender, EventArgs e)
        {
            LayMaLoaiPN();
            LayMaLoaiSP();
            GetData();
        }

        public void LayMaLoaiSP()
        {
            string truy_van = "select * from SanPham";
            cmbMaSP.DataSource = kn.LayDuLieu(truy_van);
            cmbMaSP.DisplayMember = "tenSP";
            cmbMaSP.ValueMember = "maSP";
        }
        public void LayMaLoaiPN()
        {
            string truy_van = "select * from PhieuNhap";
            cmbMaPN.DataSource = kn.LayDuLieu(truy_van);
            cmbMaPN.DisplayMember = "maPhieuNhap";
            cmbMaPN.ValueMember = "maPhieuNhap";
        }

        public void GetData()
        {
            string truyVan = "SELECT * FROM CTPN";
            dgvChiTietHoaDon.DataSource = kn.LayDuLieu(truyVan);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            GetData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("INSERT INTO CTPN VALUES('{0}', '{1}', '{2}', '{3}')",
                cmbMaPN.SelectedValue,
                txtSoLuong.Text,
                txtDonGia.Text,
                cmbMaSP.SelectedValue
            );


            if (kn.ThucThi(truyVan))
                {
                    MessageBox.Show("Thêm thành công!");
                    btnLamMoi.PerformClick();
                }
            else
            {
                MessageBox.Show("Thêm không thành công!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("update CTPN set  soLuong='{0}', donGia='{1}' where maPN='{2}' and maSP='{3}'",
                
                txtSoLuong.Text,
                txtDonGia.Text,
                cmbMaPN.SelectedValue,
                cmbMaSP.SelectedValue
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
            string truy_van = string.Format("delete from SanPham where maPN ='{0}'",
                cmbMaPN.SelectedValue
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            PhieuNhap frmHT = new PhieuNhap();
            frmHT.Show();
            this.Close();
        }

        public void clearText()
        {
            cmbMaPN.SelectedValue = string.Empty;
            txtSoLuong.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            cmbMaSP.SelectedValue = string.Empty;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("select * from CTPN where maPN like '%{0}%' or maSP like '%{0}%'",
                txtTimKiem.Text
            );
            dgvChiTietHoaDon.DataSource = kn.LayDuLieu(truy_van);
        }

        private void dgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                cmbMaPN.Text = dgvChiTietHoaDon.Rows[r].Cells["maPhieuNhap"].Value.ToString();
                txtSoLuong.Text = dgvChiTietHoaDon.Rows[r].Cells["soLuong"].Value.ToString();
                txtDonGia.Text = dgvChiTietHoaDon.Rows[r].Cells["donGia"].Value.ToString();
                cmbMaSP.Text = dgvChiTietHoaDon.Rows[r].Cells["maSP"].Value.ToString();
            }
        }
    }
}
