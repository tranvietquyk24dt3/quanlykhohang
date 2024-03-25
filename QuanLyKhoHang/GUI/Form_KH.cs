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
    public partial class Form_KH : Form
    {
        public Form_KH()
        {
            InitializeComponent();
        }

        private void Form_KH_Load(object sender, EventArgs e)
        {
            DataTable dt = DataAccess.Query("select * from Khachhang");
            dataGridView1.DataSource = dt;

        }

        //load tu csdl hien thi len datagridview
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            lblTen.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        #region Push Information

        public delegate void GetData(string id, string name);

        public GetData Getmydata;
        
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "OK")
            {
                if (lblID.Text == "")
                    MessageBox.Show("Chọn một nhà khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    Getmydata(lblID.Text, lblTen.Text);
                    this.Hide();
                }
            }
            else
                Close();
        }
        #endregion

    }
}
