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
using QuanlyKhohang.DataLayer;
using QuanlyKhohang.GUI;

namespace QuanlyKhohang.GUI
{
    public partial class Phieuxuat : UserControl
    {
        Phieuxuat_BUS px = new Phieuxuat_BUS();
        public Phieuxuat()
        {
            InitializeComponent();
            px.bangDuLieu = dataGridView1;
        }
        private void Phieuxuat_Load_1(object sender, EventArgs e)
        {
            px.ViewAll();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPXID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtKH.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtNhanvien.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtPNgayxuat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtTongsoluong.Text = DataAccess.Query("select * from GetAmount1(" + txtPXID.Text + ")").Rows[0][0].ToString();
            txtTongtien.Text = DataAccess.Query("select * from GetTotal1(" + txtPXID.Text + ")").Rows[0][0].ToString();
            txtKHID.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtNVID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        #region Get Infomation
        public void GetValueKH(string id, string name)
        {
            txtKH.Text = name;
            txtKHID.Text = id;
        }
        private void btnKH_Click_1(object sender, EventArgs e)
        {
            Form_KH KH = new Form_KH();
            KH.Getmydata = new Form_KH.GetData(GetValueKH);
            KH.ShowDialog();
        }
        public void GetValueNV(string id, string name)
        {
            txtNhanvien.Text = name;
            txtNVID.Text = id;
        }
        private void btnNhanvien_Click_1(object sender, EventArgs e)
        {
            Form_NV nv = new Form_NV();
            nv.Getmydata = new Form_NV.GetData(GetValueNV);
            nv.ShowDialog();
        }
        #endregion

        #region Details
        private void btnChitietPN_Click(object sender, EventArgs e)
        {
            if (txtPXID.Text == "")
                MessageBox.Show("Chọn một phiếu xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                Chitietphieuxuat ctx = new Chitietphieuxuat();
                ctx.GetValue1(txtPXID.Text);
                ctx.ShowDialog();
            }
        }
        #endregion

        #region Handle button
        int luu = 0;
        public void ResetText1()
        {
            List<TextBox> lst = new List<TextBox>()
            {
                txtPXID,txtNVID,txtNhanvien,txtKHID,txtKH,txtTKNhanvien,txtTongsoluong,txtTongtien
            };
            foreach (var item in lst)
                item.ResetText();
            txtKH.Focus();
        }
        public void Start()
        {
            ResetText1();
            luu = 0;
            btnSua.BackColor = Color.LightGray;
            btnThem.BackColor = Color.LightGray;
            btnKH.Visible = false;
            btnNhanvien.Visible = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            txtTKNhanvien.Enabled = true;
            txtTKTenKH.Enabled = true;
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            luu = 1;
            btnThem.Enabled = false;
            btnThem.BackColor = Color.LightBlue;
            btnKH.Visible = true;
            btnNhanvien.Visible = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            ResetText1();
            txtTKNhanvien.Enabled = false;
            txtTKTenKH.Enabled = false;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            luu = 2;
            txtNhanvien.Focus();
            btnKH.Visible = false;
            btnSua.BackColor = Color.LightBlue;
            btnNhanvien.Visible = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            txtTKNhanvien.Enabled = false;
            txtTKTenKH.Enabled = false;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            luu = 0;
            int res = px.Del(int.Parse(txtPXID.Text));
            if (txtPXID.Text == "")
                MessageBox.Show("Chọn một phiếu xuất để xóa.", "Lỗi");
            else if (res == 1)
                MessageBox.Show("Sản phẩm đã trong kho không thể xóa phiếu xuất.", "Lỗi");
            else
            {
                DialogResult dr;
                dr = MessageBox.Show("Xóa dữ liệu phiếu xuất", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show("Xóa thành công");
                    px.ViewAll();
                }
            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            if (luu == 1)
            {
                if (txtNhanvien.Text == "" | txtKH.Text == "")
                    MessageBox.Show("xuất đầy đủ thông tin phiếu xuất.", "Lỗi");
                else
                {
                    px.Add(int.Parse(txtKHID.Text), int.Parse(txtNVID.Text), DateTime.Parse(dtPNgayxuat.Text));
                    MessageBox.Show("Thêm thành công");
                    px.ViewAll();
                    Start();
                }
            }
            else if (luu == 2)
            {
                if (txtNhanvien.Text == "" | txtKH.Text == "")
                    MessageBox.Show("xuất đầy đủ thông tin phiếu xuất.", "Lỗi");
                else
                {
                    px.Update(int.Parse(txtPXID.Text), int.Parse(txtKHID.Text), int.Parse(txtNVID.Text), DateTime.Parse(dtPNgayxuat.Text));
                    MessageBox.Show("Sửa thành công");
                    px.ViewAll();
                    Start();
                }
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            Start();
        }

        #endregion
        private void txtTKTenKH_TextChanged(object sender, EventArgs e)
        {
            px.timKiem(txtTKTenKH.Text, txtTKNhanvien.Text);
        }




        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPXID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtKH.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtNhanvien.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtPNgayxuat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtTongsoluong.Text = DataAccess.Query("select * from GetAmount1(" + txtPXID.Text + ")").Rows[0][0].ToString();
            txtTongtien.Text = DataAccess.Query("select * from GetTotal1(" + txtPXID.Text + ")").Rows[0][0].ToString();
            txtKHID.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtNVID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
