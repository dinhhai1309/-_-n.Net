using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Luong : Form
    {
        public Luong()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void btnThoat_Click(object sender, EventArgs e)
        {
            TrangChu frmHT = new TrangChu();
            frmHT.Show();
            this.Close();
        }

        private void Luong_Load(object sender, EventArgs e)
        {
            getdata();
        }

        public void getdata()
        {
            string truyVan = @"SELECT * FROM Luong";
            dgvLuong.DataSource = kn.LayDuLieu(truyVan);
        }

        public void refresh_tb()
        {
            txtMaBL.Text = "";
            txtMaNV.Text = "";
            txtNgayCong.Text = "";
            txtHSLuong.Text = "";
            txtNgayCong.Text = "";
            txtTinhLuong.Text = "";
            txtTinhLuong.Text = "";
            cbThang.Text = "";
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            if (txtHSLuong.Text != "" && txtNgayCong.Text != "")
            {
                if (txtThuong.Text != "")
                {
                    float HSLuong = Convert.ToInt32(txtHSLuong.Text);
                    int ngayCong = Convert.ToInt32(txtNgayCong.Text);
                    float thuong = Convert.ToInt32(txtThuong.Text);
                    txtTinhLuong.Text = ((HSLuong * ngayCong) + thuong).ToString();
                }
                else
                {
                    float HSLuong = Convert.ToInt32(txtHSLuong.Text);
                    int ngayCong = Convert.ToInt32(txtNgayCong.Text);
                    txtTinhLuong.Text = (HSLuong * ngayCong).ToString();
                }
            }
            else
            {
                //float HSLuong = Convert.ToInt32(txtHSLuong.Text);
                //int ngayCong = Convert.ToInt32(txtNgayCong.Text);
                if (txtHSLuong.Text == "")
                {
                    MessageBox.Show("Nhập hệ số lương!");
                }
                else
                {
                    if (txtNgayCong.Text == "")
                    {
                        MessageBox.Show("Nhập số ngày Công");
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("INSERT INTO Luong VALUES ('{0}','{1}',{2},{3},{4},{5},{6})",
                                            txtMaBL.Text,
                                            txtMaNV.Text,
                                            txtNgayCong.Text,
                                            txtHSLuong.Text,
                                            txtThuong.Text,
                                            txtTinhLuong.Text,
                                            cbThang.Text);

            if (txtMaNV.Text != "")
            {
                if (kn.ThucThi(truyVan))
                {
                    MessageBox.Show("Thêm thành công!");
                    getdata();
                    refresh_tb();
                }
            }
            else
            {
                MessageBox.Show("Nhập mã nhân viên!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("UPDATE LUONG SET ngayCong ={2}, HSluong = {3}, Thuong = {4}, tongLuong ={5}, thang ='{6}' WHERE maNV ='{1}'AND maBangLuong ='{0}'",
                                            txtMaBL.Text,
                                            txtMaNV.Text,
                                            txtNgayCong.Text,
                                            txtHSLuong.Text,
                                            txtThuong.Text,
                                            txtTinhLuong.Text,
                                            cbThang.Text);

            if (txtMaNV.Text != "")
            {
                if (kn.ThucThi(truyVan))
                {
                    MessageBox.Show("sửa thành công!");
                    getdata();
                    refresh_tb();
                }
            }
            else
            {
                MessageBox.Show("Nhập mã nhân viên!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format("DELETE FROM Luong WHERE maBangLuong = '{0}' AND maNV ='{1}'",
                                            txtMaBL.Text,
                                            txtMaNV.Text);

            if (txtMaNV.Text != "")
            {
                if(txtMaBL.Text != "")
                {
                    if (kn.ThucThi(truyVan))
                    {
                        MessageBox.Show("Xóa thành công!");
                        getdata();
                        refresh_tb();
                    }
                }
                else
                {
                    MessageBox.Show("Nhập mã bảng lương!");
                }
            }
            else
            {
                MessageBox.Show("Nhập mã nhân viên!");
            }
        }

        private void dgvLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;

            txtMaBL.Text = dgvLuong.Rows[r].Cells["maBangLuong"].Value.ToString();
            txtMaNV.Text = dgvLuong.Rows[r].Cells["maNV"].Value.ToString();
            txtNgayCong.Text = dgvLuong.Rows[r].Cells["ngayCong"].Value.ToString();
            txtHSLuong.Text = dgvLuong.Rows[r].Cells["HSLuong"].Value.ToString();
            txtThuong.Text = dgvLuong.Rows[r].Cells["Thuong"].Value.ToString();
            txtTinhLuong.Text = dgvLuong.Rows[r].Cells["tongLuong"].Value.ToString();
            cbThang.Text = dgvLuong.Rows[r].Cells["thang"].Value.ToString();

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string truyVan = string.Format(@"SELECT *  FROM Luong  WHERE maBangLuong LIKE N'%{0}%' OR maNV LIKE N'%{0}%' OR ngayCong LIKE N'%{0}%' OR HSluong LIKE N'%{0}%' OR Thuong LIKE '%{0}%' OR tongLuong LIKE N'%{0}%' OR thang LIKE '%{0}%'", txtTimKiem.Text);
            dgvLuong.DataSource = kn.LayDuLieu(truyVan);
        }
    }
}
