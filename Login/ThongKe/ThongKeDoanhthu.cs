using Microsoft.SqlServer.Server;
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
    public partial class ThongKeDoanhthu: Form
    {
        public ThongKeDoanhthu()
        {
            InitializeComponent();
        }
        connect kn = new connect();

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string thongKe = String.Format(@"SELECT ngayBan,maHoaDon,maNV, tongHoaDon FROM hoaDon WHERE ngayBan = '{0}-{1}-{2}'",
                dtpThongKe.Value.Year.ToString(),
                dtpThongKe.Value.Month.ToString(),
                dtpThongKe.Value.Day.ToString()
                );

            dgvThongKe.DataSource = kn.LayDuLieu(thongKe);
              
        }
    }
}
