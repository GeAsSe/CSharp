using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{

    public class MysqlHelper
    {
        public static string connString = "server = cdb-ezjh4cma.bj.tencentcdb.com;port = 10210; user id = root;password=abcd1234;persistsecurityinfo = True; database = hzx.NET";
        public static MySqlConnection conn = null;
        public static MySqlCommand cmd = null;
        public static MySqlDataReader dr = null;

        public int LoginCheck(int uid, string pwd)
        {
            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();
                cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select * from user where uid = @uid";
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));
                dr = cmd.ExecuteReader();
                if(dr.Read() == false)
                {
                    //没有此用户
                    return 0;
                }
                if (!dr["password"].Equals(pwd))
                {
                    //密码不正确
                    return 1;
                }
                if (dr["admin"].Equals(1))
                {
                    //是管理员
                    return 4;
                }
                else
                {
                    //是普通用户
                    Console.WriteLine(dr["admin"]);
                    return 5;
                }
                
            }
            catch(Exception e)
            {
                //出错
                Console.WriteLine(e);
                return 3;
            }
            finally
            {
                if(dr != null)
                {
                    dr.Close();
                }
                if(conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static MySqlDataReader getReader(string cText)
        {
            conn = new MySqlConnection(connString);
            conn.Open();
            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = cText;
            return cmd.ExecuteReader();
            
        }

    }
}
