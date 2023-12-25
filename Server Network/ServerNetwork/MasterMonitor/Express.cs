using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQL_Test
{
    class Express
    {
        public string connString = "Data Source=10.175.12.91;Initial Catalog=tetest;User ID = battery;Pwd = batterysql";
        //创建连接对象的变量  
        public SqlConnection conn;
        // 执行对数据表中数据的增加、删除、修改操作  
        public int NonQuery(string sql)
        {
            conn = new SqlConnection(connString);
            int a = -1;
            try
            {
                conn.Open();  //打开数据库  
                SqlCommand cmd = new SqlCommand(sql, conn);
                a = cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Connect Fail!");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();    //关闭数据库  
                }
            }
            return a;

        }
        // 执行对数据表中数据的查询操作  
        public DataSet Query(string sql)
        {
            conn = new SqlConnection(connString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();      //打开数据库  
                SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
                adp.Fill(ds);
            }
            catch
            {
                //MessageBox.Show("Connect Fail!");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();      //关闭数据库  
            }
            return ds;
        }
    }
}
