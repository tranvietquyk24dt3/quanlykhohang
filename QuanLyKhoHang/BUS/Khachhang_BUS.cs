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
    class Khachhang_BUS
    {
        #region Control
        public DataGridView bangDuLieu { get; set; }
        public DataView dv { get; set; }
        #endregion

        //hien thi thong tin chi tiet cua kh
        public void ViewAll()
        {
            string sql = string.Format("select * from Khachhang");
            DataTable dt = DataAccess.Query(sql);
            dv = new DataView(dt);
            bangDuLieu.DataSource = dt;
            bangDuLieu.Columns[0].HeaderText = "KHID";
            bangDuLieu.Columns[1].HeaderText = "Tên Khách hàng";
            bangDuLieu.Columns[2].HeaderText = "Địa chỉ";
            bangDuLieu.Columns[3].HeaderText = "Điện thoại";
            bangDuLieu.Columns[4].HeaderText = "Email";
        }

        //Loc ket qua tim kiem trong csdl
        public void timKiem(string tenkh, string diachi)
        {
            dv.RowFilter = "[TenKH] like '%" + tenkh + "%' and [Diachi] like '%" + diachi + "%'";
            bangDuLieu.DataSource = dv;
        }

        //Them kh moi
        public void Add(string ten,string diachi,string dienthoai,string email)
        {
            DataAccess.NonQuery("Khachhang_insert",
                new SqlParameter("@ten", ten),
                new SqlParameter("@diachi", diachi),
                new SqlParameter("@dienthoai", dienthoai),
                new SqlParameter("@email", email));
        }

        //Sua thong tin kh
        public void Update(int id, string ten, string diachi, string dienthoai, string email)
        {
            DataAccess.NonQuery("Khachhang_update",
                new SqlParameter("@khid", id),
                new SqlParameter("@ten", ten),
                new SqlParameter("@diachi", diachi),
                new SqlParameter("@dienthoai", dienthoai),
                new SqlParameter("@email", email));
        }

        //Xoa kh da co theo id
        public int Delete(int id)
        {
            DataTable dt = DataAccess.Query("Khachhang_delete",
                new SqlParameter("@khid", id));
            int res = int.Parse(dt.Rows[0][0].ToString());
            return res;
        }
    }
}
