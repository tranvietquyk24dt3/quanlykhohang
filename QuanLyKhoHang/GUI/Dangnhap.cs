using QuanlyKhohang.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlyKhohang.GUI
{
    public partial class Dangnhap : Form
    {
        Dangnhap_BUS dn = new Dangnhap_BUS();

        public Dangnhap()
        {
            InitializeComponent();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            int res = dn.CheckUser(txtTaikhoan.Text, txtMatkhau.Text);
            if (res == 1)
            {
                lbl1.Visible = true;
                lbl2.Visible = false;
            }
            else if (res == 2)
            {
                lbl2.Visible = true;
                lbl1.Visible = false;
            }
            else
            {
                QLKhohang f = new QLKhohang();
                this.Hide();
                f.Show();

            }
        }
    }
}
