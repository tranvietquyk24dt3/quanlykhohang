using QuanlyKhohang;
using QuanlyKhohang.BUS;
using QuanlyKhohang.DataLayer;
using QuanlyKhohang.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form_NCC : Form
    {
        public Form_NCC()
        {
            InitializeComponent();
        }
        #region Push Information
        public delegate void GetData(string id, string name);
        public GetData Getmydata;

        private void Form_NCC_Load(object sender, EventArgs e)
        {
            DataTable dt = DataAccess.Query("select * from Nhacungcap");
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            lblTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        //private void btn_Click(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    if (btn.Text == "OK")
        //    {
        //        if (lblID.Text == "")
        //            MessageBox.Show("Chọn một nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        else
        //        {
        //            Getmydata(lblID.Text, lblTen.Text);
        //            this.Hide();
        //        }
        //    }
        //    else
        //        Close();
        //}

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
                MessageBox.Show("Chọn một nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                Getmydata(lblID.Text, lblTen.Text);
                this.Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
