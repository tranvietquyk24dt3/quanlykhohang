using QuanlyKhohang.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyKhohang.BUS
{
    class Dangnhap_BUS
    {
        public int CheckUser(string user, string pass)
        {
            DataTable dt = DataAccess.Query("Dangnhap_check",
                new SqlParameter("@tk", user),
                new SqlParameter("@mk", pass));
            int res = int.Parse(dt.Rows[0][0].ToString());
            return res;
        }
    }
}
