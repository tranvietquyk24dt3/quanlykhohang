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

namespace QuanlyKhohang.GUI
{
    public partial class Khachhang : UserControl
    {
        Khachhang_BUS kh = new Khachhang_BUS();

        public Khachhang()
        {
            InitializeComponent();
            kh.bangDuLieu = dataGridView1;
        }

        private void Khachhang_Load(object sender, EventArgs e)
        {
            kh.ViewAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKHID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDiachi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDienthoai.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        #region Handle button
        int luu = 0;        //luu = 1 them, luu = 2 sua, luu = 0 bat dau va xoa
        public void ResetText1()
        {
            List<TextBox> lst = new List<TextBox>()
            {
                txtKHID,txtTenKH,txtEmail,txtDienthoai,txtDiachi,txtTKDiachi,txtTKTenKH
            };
            foreach (var item in lst)
                item.ResetText();
            txtTenKH.Focus();
        }
        public void Start()
        {
            ResetText1();
            luu = 0;
            btnSua.BackColor = Color.LightGray;
            btnThem.BackColor = Color.LightGray;
            btnHuy.BackColor = Color.LightGray;
            btnLuu.BackColor = Color.LightGray;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            txtTKDiachi.Enabled = true;
            txtTKTenKH.Enabled = true;
        }

        #endregion

        private void btnThem_Click(object sender, EventArgs e)
        {
            luu = 1;
            btnThem.Enabled = false;
            btnThem.BackColor = Color.LightBlue;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            ResetText1();
            txtTKDiachi.Enabled = false;
            txtTKTenKH.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtKHID.Text == "")
                MessageBox.Show("Chọn một sản phẩm để sửa.", "Lỗi");
            else
            {
                luu = 2;
                txtTenKH.Focus();
                btnSua.BackColor = Color.LightBlue;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                txtTKDiachi.Enabled = false;
                txtTKTenKH.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            luu = 0;
            
            if (txtKHID.Text == "")
                MessageBox.Show("Chọn một khách hàng để xóa.", "Lỗi");
            else
            {
                int res = kh.Delete(int.Parse(txtKHID.Text));
                if (res == 1)
                    MessageBox.Show("Khách hàng đã từng thực hiện giao dịch không thể xóa.", "Lỗi");
                else
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Xóa dữ liệu khách hàng", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.OK)
                    {
                        kh.Delete(int.Parse(txtKHID.Text));
                        MessageBox.Show("Xóa thành công");
                        kh.ViewAll();
                    }
                }
            }
            
        }

        private void txtDienthoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (luu == 1)
            {
                txtKHID.ResetText();
                if (txtTenKH.Text == "" | txtDiachi.Text == "" | txtDienthoai.Text == "" | txtEmail.Text == "")
                    MessageBox.Show("Nhập đầy đủ thông tin khách hàng.", "Lỗi");
                else
                {
                    kh.Add(txtTenKH.Text, txtDiachi.Text, txtDienthoai.Text, txtEmail.Text);
                    MessageBox.Show("Thêm thành công");
                    kh.ViewAll();
                    Start();
                }
            }
            else if (luu == 2)
            {
                if (txtTenKH.Text == "" | txtDiachi.Text == "" | txtDienthoai.Text == "" | txtEmail.Text == "")
                    MessageBox.Show("Nhập đầy đủ thông tin sản phẩm.", "Lỗi");
                else
                {
                    kh.Update(int.Parse(txtKHID.Text), txtTenKH.Text, txtDiachi.Text, txtDienthoai.Text, txtEmail.Text);
                    MessageBox.Show("Sửa thành công");
                    kh.ViewAll();
                    Start();
                }
            }
        }

        private void txtTKTenKH_TextChanged(object sender, EventArgs e)
        {
            kh.timKiem(txtTKTenKH.Text, txtTKDiachi.Text);
        }
    }
}
