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
    class Phieuxuat_BUS
    {
        public DataGridView bangDuLieu { get; set; }
        public DataView dv { get; set; }
        public void ViewAll()
        {
            DataTable dt = DataAccess.Query("Phieuxuat_view");
            dv = new DataView(dt);
            bangDuLieu.DataSource = dt;
            bangDuLieu.Columns[0].HeaderText = "ID";
            bangDuLieu.Columns[1].HeaderText = "Tên khách hàng";
            bangDuLieu.Columns[2].HeaderText = "Tên nhân viên";
            bangDuLieu.Columns[3].HeaderText = "Ngày xuất";
            bangDuLieu.Columns[4].Visible = false;
            bangDuLieu.Columns[5].Visible = false;
        }
        public void timKiem(string tenkh, string tennv)
        {
            dv.RowFilter = "[TenKH] like '%" + tenkh + "%' and [TenNV] like '%" + tennv + "%'";
            bangDuLieu.DataSource = dv;
        }
        public void Add(int khid, int nvid, DateTime ngayxuat)
        {
            DataAccess.NonQuery("Phieuxuat_insert",
                new SqlParameter("@khid", khid),
                new SqlParameter("@nvid", nvid),
                new SqlParameter("@ngayxuat", ngayxuat));
        }
        public int Del(int id)
        {
            DataTable dt = DataAccess.Query("Phieuxuat_delete",
                new SqlParameter("@id", id));
            int res = int.Parse(dt.Rows[0][0].ToString());
            return res;

        }
        public void Update(int id, int khid, int nvid, DateTime ngayxuat)
        {
            DataAccess.NonQuery("Phieuxuat_update",
                new SqlParameter("@id", id),
                new SqlParameter("@khid", khid),
                new SqlParameter("@nvid", nvid),
                new SqlParameter("@ngayxuat", ngayxuat));
        }
    }
}
