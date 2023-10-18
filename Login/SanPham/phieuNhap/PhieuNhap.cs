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
    public partial class PhieuNhap : Form
    {
        public PhieuNhap()
        {
            InitializeComponent();
        }

        connect kn = new connect();

        private void PhieuNhap_Load(object sender, EventArgs e)
        {
            LayMaLoaiNCC();
            LayMaLoaiNV();
            GetData();
        }

        public void LayMaLoaiNCC()
        {
            string truy_van = "select * from NhaCungCap";
            cmbMaNCC.DataSource = kn.LayDuLieu(truy_van);
            cmbMaNCC.DisplayMember = "tenNCC";
            cmbMaNCC.ValueMember = "maNCC";
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
            string truyVan = "SELECT PhieuNhap.maPhieuNhap, PhieuNhap.maNCC, PhieuNhap.ngayNhap, PhieuNhap.maNV,( SELECT SUM(CTPhieuNhap.soLuong * CTPhieuNhap.donGia) FROM CTPhieuNhap WHERE CTPhieuNhap.maPhieuNhap = PhieuNhap.maPhieuNhap) AS tienNhap FROM PhieuNhap;";
            dgvPhieuNhap.DataSource = kn.LayDuLieu(truyVan);
        }

        private void dgvPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaPN.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                txtMaPN.Text = dgvPhieuNhap.Rows[r].Cells["maPhieuNhap"].Value.ToString();
                cmbMaNCC.Text = dgvPhieuNhap.Rows[r].Cells["maNCC"].Value.ToString();
                dtNgayNhap.Text = dgvPhieuNhap.Rows[r].Cells["ngayNhap"].Value.ToString();
                cmbMaNV.Text = dgvPhieuNhap.Rows[r].Cells["maNV"].Value.ToString();
            }
        }

        public void clearText()
        {
            txtMaPN.Text = "";
            dtNgayNhap.Text = "";
            cmbMaNCC.SelectedValue = "";
            cmbMaNV.SelectedValue = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            GetData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("insert into PhieuNhap values('{0}','{1}','{2}',0,'{4}')",
                txtMaPN.Text,
                cmbMaNCC.SelectedValue,
                dtNgayNhap.Text,
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("update PhieuNhap set maNCC='{0}', ngayNhap ='{1}',tienNhap=0, maNV='{2}' where maPhieuNhap='{3}'",
                cmbMaNCC.SelectedValue,
                dtNgayNhap.Text,
                cmbMaNV.SelectedValue,
                txtMaPN.Text
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
            string truy_van = string.Format("delete from PhieuNhap where maPhieuNhap='{0}'",
                txtMaPN.Text
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
            TrangChu frmHT = new TrangChu();
            frmHT.Show();
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string truy_van = string.Format("select * from PhieuNhap where maPhieuNhap like '%{0}%' or maNCC like '%{0}%' or ngayNhap like '%{0}%' or tienNhap like '%{0}%' or maNV like '%{0}%'",
                txtTimKiem.Text
            );
            dgvPhieuNhap.DataSource = kn.LayDuLieu(truy_van);
        }


        private void btnCTPN_Click(object sender, EventArgs e)
        {
            CTPN frmCTHD = new CTPN();
            frmCTHD.Show();
            this.Hide();
        }
    }
}
