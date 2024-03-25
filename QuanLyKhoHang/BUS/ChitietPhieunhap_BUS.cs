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
    class ChitietPhieunhap_BUS
    {
        public TextBox txtTotal { get; set; }
        public DataGridView sanPham { get; set; }
        public DataGridView chiTiet { get; set; }
        public void ViewAll(int pnid, int nccid)
        {
            txtTotal.Text = DataAccess.Query("select * from GetTotal(" + pnid.ToString() + ")").Rows[0][0].ToString();
            DataTable dt1 = DataAccess.Query("select * from Sanpham where NCCID = " + nccid.ToString());
            sanPham.DataSource = dt1;
            sanPham.Columns[0].HeaderText = "ID";
            sanPham.Columns[1].HeaderText = "Tên SP";
            sanPham.Columns[2].Visible = false;
            sanPham.Columns[3].HeaderText = "Giá";
            sanPham.Columns[4].HeaderText = "SL";

            DataTable dt2 = DataAccess.Query("select * from Chitietphieunhap_view(" + pnid.ToString() + ")");
            chiTiet.DataSource = dt2;
            chiTiet.Columns[0].HeaderText = "ID";
            chiTiet.Columns[1].HeaderText = "Tên SP";
            chiTiet.Columns[2].HeaderText = "Giá";
            chiTiet.Columns[3].HeaderText = "Số lượng";
            chiTiet.Columns[4].HeaderText = "Đơn giá SP";
        }
        public void Insert_Update(int pnid, int spid, int soluong)
        {
            DataAccess.NonQuery("Chitietphieunhap_insert_update",
                new SqlParameter("@pnid", pnid),
                new SqlParameter("@spid", spid),
                new SqlParameter("@soluong", soluong));
        }
        public void Update(int pnid, int spid, int soluong)
        {
            DataAccess.NonQuery("Chitietphieunhap_update",
                new SqlParameter("@pnid", pnid),
                new SqlParameter("@spid", spid),
                new SqlParameter("@soluong", soluong));
        }
        public void Delete(int pnid, int spid)
        {
            DataAccess.NonQuery("Chitietphieunhap_delete",
                new SqlParameter("@pnid", pnid),
                new SqlParameter("@spid", spid));
        }
    }

}
