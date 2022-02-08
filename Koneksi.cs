using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//1.
using System.Data.SqlClient;
namespace KoneksiDBSQLServer
{
    internal class Koneksi
    {
        //2.
        public SqlConnection GetConn()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "Data Source=192.168.26.205;Initial Catalog=DB_CONTOH;User Id=sa; Password=BErtho@_2110";
            return Conn;
        }
    }
}
