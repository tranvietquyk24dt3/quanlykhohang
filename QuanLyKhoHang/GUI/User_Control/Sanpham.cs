using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanlyKhohang.BUS;
using GUI;

namespace QuanlyKhohang.GUI
{
    public partial class Sanpham : UserControl
    {
        Sanpham_BUS sp = new Sanpham_BUS();
        public Sanpham()
        {
            InitializeComponent();
            sp.bangDuLieu = dataGridView1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtSPID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtNCC.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtGia.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtSoluong.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtNCCID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
        public void GetValue(string id, string name)
        {
            txtNCCID.Text = id;
            txtNCC.Text = name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Form_NCC fncc = new Form_NCC();
            fncc.Getmydata = new Form_NCC.GetData(GetValue);
            fncc.ShowDialog();
        }
        #region Handle button
        int luu = 0;
        public void ResetText1()
        {
            List<TextBox> lst = new List<TextBox>()
            {
                txtGia,txtNCC,txtSoluong,txtNCCID,txtSPID,txtTenSP,txtTKNCC,txtTKTenSP
            };
            foreach (var item in lst)
                item.ResetText();
            txtTenSP.Focus();
        }
        public void Start()
        {
            ResetText1();
            luu = 0;
            btnSua.BackColor = Color.LightGray;
            btnThem.BackColor = Color.LightGray;
            btnHuy.BackColor = Color.LightGray;
            btnLuu.BackColor = Color.LightGray;
            btnOk.Visible = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            txtTKNCC.Enabled = true;
            txtTKTenSP.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            luu = 1;
            btnThem.Enabled = false;
            btnThem.BackColor = Color.LightBlue;
            btnOk.Visible = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtTKNCC.Enabled = false;
            txtTKTenSP.Enabled = false;
            ResetText1();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            if (txtSPID.Text == "")
                MessageBox.Show("Chọn một sản phẩm để sửa.", "Lỗi");
            else
            {
                luu = 2;
                txtTenSP.Focus();
                btnOk.Visible = false;
                btnSua.BackColor = Color.LightBlue;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                txtTKNCC.Enabled = false;
                txtTKTenSP.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            luu = 0;
            int res = sp.Delete(int.Parse(txtSPID.Text));
            if (txtSPID.Text == "")
                MessageBox.Show("Chọn một sản phẩm để xóa.", "Lỗi");
            else if (res == 1 || res == 2)
                MessageBox.Show("Sản phẩm đã tồn tại trong Phiếu nhập hoặc Phiếu xuất.", "Lỗi");
            else
            {
                DialogResult dr;
                dr = MessageBox.Show("Xóa dữ liệu sản phẩm", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    sp.Delete(int.Parse(txtSPID.Text));
                    MessageBox.Show("Xóa thành công");
                    sp.ViewAll();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (luu == 1)
            {
                if (txtTenSP.Text == "" | txtGia.Text == "" | txtNCCID.Text == "")
                    MessageBox.Show("Nhập đầy đủ thông tin sản phẩm.", "Lỗi");
                else
                {
                    sp.Add(txtTenSP.Text, int.Parse(txtNCCID.Text), float.Parse(txtGia.Text), 0);
                    MessageBox.Show("Thêm thành công");
                    sp.ViewAll();
                    Start();
                }
            }
            else if (luu == 2)
            {
                if (txtTenSP.Text == "" | txtGia.Text == "")
                    MessageBox.Show("Nhập đầy đủ thông tin sản phẩm.", "Lỗi");
                else
                {
                    sp.Update(int.Parse(txtSPID.Text), txtTenSP.Text, int.Parse(txtNCCID.Text), float.Parse(txtGia.Text));
                    MessageBox.Show("Sửa thành công");
                    sp.ViewAll();
                    Start();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Start();
        }
        #endregion

        //private void timKiem(object sender, EventArgs e)
        //{
        //    sp.timKiem(txtTKTenSP.Text, txtTKNCC.Text);
        //}

        private void Sanpham_Load_1(object sender, EventArgs e)
        {
            sp.ViewAll();
        }

        private void txtTKTenSP_TextChanged(object sender, EventArgs e)
        {
            sp.timKiem(txtTKTenSP.Text, txtTKNCC.Text);
        }


    }
}
