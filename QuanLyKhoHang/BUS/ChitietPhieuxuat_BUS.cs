using QuanlyKhohang.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlyKhohang.BUS
{
    class ChitietPhieuxuat_BUS
    {
        public TextBox txtTotal { get; set; }
        public DataGridView sanPham { get; set; }
        public DataGridView chiTiet { get; set; }
        public void ViewAll(int pxid)
        {
            txtTotal.Text = DataAccess.Query("select * from GetTotal1(" + pxid.ToString() + ")").Rows[0][0].ToString();
            DataTable dt1 = DataAccess.Query("select * from Sanpham");
            sanPham.DataSource = dt1;
            sanPham.Columns[0].HeaderText = "ID";
            sanPham.Columns[1].HeaderText = "Tên SP";
            sanPham.Columns[2].Visible = false;
            sanPham.Columns[3].HeaderText = "Giá";
            sanPham.Columns[4].HeaderText = "SL";

            DataTable dt2 = DataAccess.Query("select * from Chitietphieuxuat_view(" + pxid.ToString() + ")");
            chiTiet.DataSource = dt2;
            chiTiet.Columns[0].HeaderText = "ID";
            chiTiet.Columns[1].HeaderText = "Tên SP";
            chiTiet.Columns[2].HeaderText = "Giá";
            chiTiet.Columns[3].HeaderText = "Số lượng";
            chiTiet.Columns[4].HeaderText = "Đơn giá SP";
        }
        public void Insert_Update(int pxid, int spid, int soluong)
        {
            DataAccess.NonQuery("Chitietphieuxuat_insert_update",
                new SqlParameter("@pxid", pxid),
                new SqlParameter("@spid", spid),
                new SqlParameter("@soluong", soluong));
        }
        public void Update(int pxid, int spid, int soluong)
        {
            DataAccess.NonQuery("Chitietphieuxuat_update",
                new SqlParameter("@pxid", pxid),
                new SqlParameter("@spid", spid),
                new SqlParameter("@soluong", soluong));
        }
        public void Delete(int pxid, int spid)
        {
            DataAccess.NonQuery("Chitietphieuxuat_delete",
                new SqlParameter("@pxid", pxid),
                new SqlParameter("@spid", spid));
        }
    }
}
