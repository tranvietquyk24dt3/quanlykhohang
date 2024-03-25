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
using QuanlyKhohang.DataLayer;

namespace QuanlyKhohang.GUI
{
    public partial class Phieunhap : UserControl
    {

        Phieunhap_BUS pn = new Phieunhap_BUS();
        public Phieunhap()
        {
            InitializeComponent();
            pn.bangDuLieu = dataGridView1;
        }
        private void Phieunhap_Load(object sender, EventArgs e)
        {
            pn.ViewAll();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtPNID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtNCC.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtNhanvien.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dtPNgaynhap.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtTongsoluong.Text = DataAccess.Query("select * from GetAmount(" + txtPNID.Text + ")").Rows[0][0].ToString();
                txtTongtien.Text = DataAccess.Query("select * from GetTotal(" + txtPNID.Text + ")").Rows[0][0].ToString();
                txtNCCID.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtNVID.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            }
            catch { }

        }
        #region Get Infomation
        public void GetValueNCC(string id, string name)
        {
            txtNCC.Text = name;
            txtNCCID.Text = id;
        }
        private void btnNCC_Click(object sender, EventArgs e)
        {
            Form_NCC ncc = new Form_NCC();
            ncc.Getmydata = new Form_NCC.GetData(GetValueNCC);
            ncc.ShowDialog();
        }
        public void GetValueNV(string id, string name)
        {
            txtNhanvien.Text = name;
            txtNVID.Text = id;
        }
        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            Form_NV nv = new Form_NV();
            nv.Getmydata = new Form_NV.GetData(GetValueNV);
            nv.ShowDialog();
        }
        #endregion

        #region Details
        private void btnChitietPN_Click(object sender, EventArgs e)
        {
            if (txtPNID.Text == "")
                MessageBox.Show("Chọn một phiếu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                Chitietphieunhap ct = new Chitietphieunhap();
                ct.GetValue1(txtPNID.Text, txtNCCID.Text);
                ct.ShowDialog();
            }
        }
        #endregion

        #region Handle button
        int luu = 0;
        public void ResetText1()
        {
            List<TextBox> lst = new List<TextBox>()
            {
                txtPNID,txtNVID,txtNhanvien,txtNCCID,txtNCC,txtTKNhanvien,txtTongsoluong,txtTongtien
            };
            foreach (var item in lst)
                item.ResetText();
            txtNCC.Focus();
        }
        public void Start()
        {
            ResetText1();
            luu = 0;
            btnSua.BackColor = Color.LightGray;
            btnThem.BackColor = Color.LightGray;
            btnNCC.Visible = false;
            btnNhanvien.Visible = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            txtTKNhanvien.Enabled = true;
            txtTKTenNCC.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            luu = 1;
            btnThem.Enabled = false;
            btnThem.BackColor = Color.LightBlue;
            btnNCC.Visible = true;
            btnNhanvien.Visible = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            ResetText1();
            txtTKNhanvien.Enabled = false;
            txtTKTenNCC.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            luu = 2;
            txtNhanvien.Focus();
            btnNCC.Visible = false;
            btnSua.BackColor = Color.LightBlue;
            btnNhanvien.Visible = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            txtTKNhanvien.Enabled = false;
            txtTKTenNCC.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            luu = 0;
            int res = pn.Del(int.Parse(txtPNID.Text));
            if (txtPNID.Text == "")
                MessageBox.Show("Chọn một phiếu nhập để xóa.", "Lỗi");
            else if (res == 1)
                MessageBox.Show("Sản phẩm đã trong kho không thể xóa phiếu nhập.", "Lỗi");
            else
            {
                DialogResult dr;
                dr = MessageBox.Show("Xóa dữ liệu phiếu nhập", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show("Xóa thành công");
                    pn.ViewAll();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (luu == 1)
            {
                if (txtNhanvien.Text == "" | txtNCC.Text == "")
                    MessageBox.Show("Nhập đầy đủ thông tin phiếu nhập.", "Lỗi");
                else
                {
                    pn.Add(int.Parse(txtNCCID.Text), int.Parse(txtNVID.Text), DateTime.Parse(dtPNgaynhap.Text));
                    MessageBox.Show("Thêm thành công");
                    pn.ViewAll();
                    Start();
                }
            }
            else if (luu == 2)
            {
                if (txtNhanvien.Text == "" | txtNCC.Text == "")
                    MessageBox.Show("Nhập đầy đủ thông tin phiếu nhập.", "Lỗi");
                else
                {
                    pn.Update(int.Parse(txtPNID.Text), int.Parse(txtNCCID.Text), int.Parse(txtNVID.Text), DateTime.Parse(dtPNgaynhap.Text));
                    MessageBox.Show("Sửa thành công");
                    pn.ViewAll();
                    Start();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Start();
        }
        #endregion

        private void text_changed(object sender, EventArgs e)
        {
            pn.timKiem(txtTKTenNCC.Text, txtTKNhanvien.Text);
        }

    }
}
