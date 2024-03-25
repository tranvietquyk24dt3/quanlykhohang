using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanlyKhohang.GUI;

namespace QuanlyKhohang
{
    public partial class QLKhohang : Form
    {
        public QLKhohang()
        {
            InitializeComponent();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ToolStripDropDownItem ts = (ToolStripDropDownItem)sender;
            if (ts.Text == "Nhà cung cấp")
                panel1.Controls.Add(new Nhacungcap());
            else if (ts.Text == "Khách hàng")
                panel1.Controls.Add(new Khachhang());
            else if (ts.Text == "Sản phẩm")
                panel1.Controls.Add(new Sanpham());
            else if (ts.Text == "Phiếu nhập")
                panel1.Controls.Add(new Phieunhap());
            else if (ts.Text == "Phiếu xuất")
                panel1.Controls.Add(new Phieuxuat());
            else if (ts.Text == "Hướng dẫn")
                panel1.Controls.Add(new Huongdan());
            else if (ts.Text == "Đăng xuất")
            {
                Dangnhap dn = new Dangnhap();
                this.Close();
                dn.ShowDialog();
            }
            else if (ts.Text == "Thoát")
                Application.Exit();
        }

        private void QLKhohang_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void QLKhohang_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
