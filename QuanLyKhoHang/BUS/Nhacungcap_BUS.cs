using System.Windows.Forms;
using System.Data;
using QuanlyKhohang.DataLayer;
using System.Data.SqlClient;
using System;

namespace QuanlyKhohang.BUS
{
    class Nhacungcap_BUS
    {
        #region Control
        public DataGridView bangDuLieu { get; set; }
        public DataView dv { get; set; }
        #endregion
        public void ViewAll()
        {
            string sql = string.Format("select * from Nhacungcap");
            DataTable dt = DataAccess.Query(sql);
            dv = new DataView(dt);
            bangDuLieu.DataSource = dt;
            bangDuLieu.Columns[0].HeaderText = "ID";
            bangDuLieu.Columns[1].HeaderText = "Tên NCC";
            bangDuLieu.Columns[2].HeaderText = "Địa chỉ";
            bangDuLieu.Columns[3].HeaderText = "Điện thoại";
            bangDuLieu.Columns[4].HeaderText = "Email";
        }
        public void timKiem(string tenncc, string diachi)
        {
            dv.RowFilter = "[TenNCC] like '%" + tenncc + "%' and [Diachi] like '%" + diachi + "%'";
            bangDuLieu.DataSource = dv;
        }
        public void Add(string ten, string diachi, string dienthoai, string email)
        {
            DataAccess.NonQuery("Nhacungcap_insert",
               new SqlParameter("@ten", ten),
               new SqlParameter("@diachi", diachi),
               new SqlParameter("@dienthoai", dienthoai),
               new SqlParameter("@email", email));
        }
        public void Update(int id,string ten,string diachi,string dienthoai,string email)
        {
            DataAccess.NonQuery("Nhacungcap_update",
                new SqlParameter("@id", id),
                new SqlParameter("@ten", ten),
                new SqlParameter("diachi", diachi),
                new SqlParameter("@dienthoai", dienthoai),
                new SqlParameter("@email", email));
        }
        public int Delete(int id)
        {
            DataTable dt = DataAccess.Query("Nhacungcap_delete",
                new SqlParameter("@id", id));
            int res = int.Parse(dt.Rows[0][0].ToString());
            return res;
        }
    }
}
