using QuanlyKhohang.BUS;
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
    public partial class Chitietphieuxuat : Form
    {
        ChitietPhieuxuat_BUS ct = new ChitietPhieuxuat_BUS();

        public Chitietphieuxuat()
        {
            InitializeComponent();
            ct.sanPham = dataGridView1;
            ct.chiTiet = dataGridView2;
            ct.txtTotal = txtTongtien;
        }

        private void Chitietphieuxuat_Load(object sender, EventArgs e)
        {
            ct.ViewAll(int.Parse(txtPXID1.Text));
            txtID.Focus();
        }
        public void GetValue1(string pxid)
        {
            txtPXID1.Text = pxid;
        }

        int trangThai = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            trangThai = 0;
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtSanpham.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            btnXoa.Enabled = false;
            txtSoluong.Focus();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            trangThai = 1;
            txtID.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtSanpham.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtSoluong.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            btnXoa.Enabled = true;
            txtSoluong.Focus();
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtSoluong.Text == "" || int.Parse(txtSoluong.Text) <= 0)
                MessageBox.Show("Nhập số lượng lớn hơn 0.");
            else
            {
                if (trangThai == 0)
                    ct.Insert_Update(int.Parse(txtPXID1.Text), int.Parse(txtID.Text), int.Parse(txtSoluong.Text));
                else
                    ct.Update(int.Parse(txtPXID1.Text), int.Parse(txtID.Text), int.Parse(txtSoluong.Text));

                ct.ViewAll(int.Parse(txtPXID1.Text));
            }
            btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ct.Delete(int.Parse(txtPXID1.Text), int.Parse(txtID.Text));
            ct.ViewAll(int.Parse(txtPXID1.Text));
            btnXoa.Enabled = false;
            txtID.ResetText(); txtSanpham.ResetText(); txtSoluong.ResetText();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
