using QuanlyKhohang.DataLayer;
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
    public partial class Form_NV : Form
    {
        public Form_NV()
        {
            InitializeComponent();
        }
        private void Form_NV_Load_1(object sender, EventArgs e)
        {
            DataTable dt = DataAccess.Query("select * from Nhanvien");
            dataGridView1.DataSource = dt;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            lblTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        public delegate void GetData(string id, string name);
        public GetData Getmydata;
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "OK")
            {
                if (lblID.Text == "")
                    MessageBox.Show("Chọn một nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    Getmydata(lblID.Text, lblTen.Text);
                    this.Hide();
                }
            }
            else
                Close();
        }


    }
}
