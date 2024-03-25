using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace QuanlyKhohang.GUI
{
    public partial class Huongdan : UserControl
    {
        public Huongdan()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            StreamReader doc=null;
            if (e.Node.Text == "Nhà cung cấp")
                doc = File.OpenText("../../Guide/" + "Khachhang.html");
            if (e.Node.Text == "Khách hàng")
                doc = File.OpenText("../../Guide/" + "Khachhang.html");
            if (e.Node.Text == "Sản phẩm")
                doc = File.OpenText("../../Guide/" + "Sanpham.html");
            if (e.Node.Text == "Phiếu xuất")
                doc = File.OpenText("../../Guide/" + "Phieuxuat.html");
            if (e.Node.Text == "Phiếu nhập")
                doc = File.OpenText("../../Guide/" + "Phieunhap.html");
            webBrowser1.DocumentText = doc.ReadToEnd();
        }
    }
}
