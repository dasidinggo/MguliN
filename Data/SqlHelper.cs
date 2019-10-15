using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class SqlHelper
    {
        string sql = ConfigurationSettings.AppSettings["ConnectionString"].ToString();

        
        public int getNonQuery(string mSql)
        {
            return getNonQuery(mSql, null);
        }

        public int getNonQuery(string mSql,SqlParameter[] mSqlPs)
        {
            SqlConnection conn = new SqlConnection(sql);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format(mSql);
            if (mSqlPs != null)
                cmd.Parameters.AddRange(mSqlPs);
            conn.Open();
            int result = cmd.ExecuteNonQuery();
            //关闭数据库
            conn.Close();
            return result;
        }


        //插入数据

        public string InsertNew(string mSql)
        {
            return InsertNew(mSql, null);
        }

        public string InsertNew(string mSql, SqlParameter[] mSqlPs)
        {
            SqlConnection conn = new SqlConnection(sql);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("{0}; SELECT @@IDENTITY;", mSql);
            if (mSqlPs != null)
                cmd.Parameters.AddRange(mSqlPs);
            conn.Open();
            string result = cmd.ExecuteScalar().ToString();

            //关闭数据库
            conn.Close();
            return result;
        }

        //读取字符串类型
        public string GetString(string mSql)
        {
            return GetString(mSql, null);
        }

        public string GetString(string mSql, SqlParameter[] mSqlPs)
        {
            SqlConnection conn = new SqlConnection(sql);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("{0}", mSql);
            if (mSqlPs != null)
                cmd.Parameters.AddRange(mSqlPs);
            conn.Open();
            string result = cmd.ExecuteScalar().ToString();

            //关闭数据库
            conn.Close();
            return result;
        }

        //读取数据Datatable

        public DataTable getDT(string mSql)
        {
            return getDT(mSql, null);
        }

        public DataTable getDT(string mSql,SqlParameter[] mSqlPs)
        {
            SqlConnection conn = new SqlConnection(sql);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("{0}", mSql);
            if (mSqlPs != null)
                cmd.Parameters.AddRange(mSqlPs);
            conn.Open();
            DataTable reDt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            reDt.Load(dr);
            //关闭数据库
            conn.Close();
            return reDt;
        }

    }
}
