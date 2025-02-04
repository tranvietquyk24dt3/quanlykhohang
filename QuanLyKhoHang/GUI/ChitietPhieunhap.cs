﻿using QuanlyKhohang.BUS;
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
    public partial class Chitietphieunhap : Form
    {
        ChitietPhieunhap_BUS ct = new ChitietPhieunhap_BUS();
        public Chitietphieunhap()
        {
            InitializeComponent();
            ct.sanPham = dataGridView1;
            ct.chiTiet = dataGridView2;
            ct.txtTotal = txtTongtien;
        }
        public void GetValue1(string pnid, string nccid)
        {
            txtPNID1.Text = pnid;
            txtNCCID.Text = nccid;
        }

        private void Chitietphieunhap_Load(object sender, EventArgs e)
        {
            ct.ViewAll(int.Parse(txtPNID1.Text), int.Parse(txtNCCID.Text));
            txtID.Focus();
        }
        #region CellContentClick
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
        #endregion
        #region ButtonHandle
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtSoluong.Text == "" || int.Parse(txtSoluong.Text) <= 0)
                MessageBox.Show("Nhập số lượng lớn hơn 0.");
            else
            {
                if (trangThai == 0)
                    ct.Insert_Update(int.Parse(txtPNID1.Text), int.Parse(txtID.Text), int.Parse(txtSoluong.Text));
                else
                    ct.Update(int.Parse(txtPNID1.Text), int.Parse(txtID.Text), int.Parse(txtSoluong.Text));

                ct.ViewAll(int.Parse(txtPNID1.Text), int.Parse(txtNCCID.Text));
            }
            btnXoa.Enabled = false;
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            ct.Delete(int.Parse(txtPNID1.Text), int.Parse(txtID.Text));
            ct.ViewAll(int.Parse(txtPNID1.Text), int.Parse(txtNCCID.Text));
            btnXoa.Enabled = false;
            txtID.ResetText(); txtSanpham.ResetText(); txtSoluong.ResetText();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        #endregion

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
