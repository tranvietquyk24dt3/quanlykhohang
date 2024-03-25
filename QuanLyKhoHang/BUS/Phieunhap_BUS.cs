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
    class Phieunhap_BUS
    {
        public DataGridView bangDuLieu { get; set; }
        public DataView dv { get; set; }

        public void ViewAll()
        {
            DataTable dt = DataAccess.Query("Phieunhap_view");
            bangDuLieu.DataSource = dt;
            dv = new DataView(dt);
            bangDuLieu.Columns[0].HeaderText = "ID";
            bangDuLieu.Columns[1].HeaderText = "Tên nhà cung cấp";
            bangDuLieu.Columns[2].HeaderText = "Tên nhân viên";
            bangDuLieu.Columns[3].HeaderText = "Ngày nhập";
            bangDuLieu.Columns[4].Visible = false;
            bangDuLieu.Columns[5].Visible = false;
        }
        public void timKiem(string tenncc, string nv)
        {
            dv.RowFilter = "[TenNCC] like '%" + tenncc + "%' and [TenNV] like '%" + nv + "%'";
            bangDuLieu.DataSource = dv;
        }
        public void Add(int nccid, int nvid, DateTime ngaynhap)
        {
            DataAccess.NonQuery("Phieunhap_insert",
                new SqlParameter("@nccid", nccid),
                new SqlParameter("@nvid", nvid),
                new SqlParameter("@ngaynhap", ngaynhap));
        }
        public int Del(int id)
        {
            DataTable dt = DataAccess.Query("Phieunhap_delete",
                new SqlParameter("@id", id));
            int res = int.Parse(dt.Rows[0][0].ToString());
            return res;

        }
        public void Update(int id, int nccid, int nvid, DateTime ngaynhap)
        {
            DataAccess.NonQuery("Phieunhap_update",
                new SqlParameter("@id", id),
                new SqlParameter("@nccid", nccid),
                new SqlParameter("@nvid", nvid),
                new SqlParameter("@ngaynhap", ngaynhap));
        }
    }
}
