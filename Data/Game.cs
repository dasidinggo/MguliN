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
    public class Game
    {
        SqlHelper sh = new SqlHelper();

        //365存入金额
        //public string SaveTSFIn(string mJE, string mDate)
        //{
        //    SqlConnection conn = new SqlConnection(sql);
        //    SqlCommand cmd = conn.CreateCommand();
        //    cmd.CommandText = string.Format("insert into tb_plan_detail (je,Svdate) values ({0},'{1}')",je,DateTime.Now.ToString("yyyy-MM-dd"));
        //    conn.Open();
        //    int result = cmd.ExecuteNonQuery();

        //    //关闭数据库
        //    conn.Close();
        //    return "";
        //}

        //存入金额
        public string SaveIn(string je)
        {
            //SqlConnection conn = new SqlConnection(sql);
            //SqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = string.Format("insert into tb_plan_detail (je,Svdate) values (@je,@Svdate)", je, );
            //conn.Open();
            //int result = cmd.ExecuteNonQuery();
            string mSql = "insert into tb_plan_detail (je,Svdate,UserID) values (@je,@Svdate,@UserID)";
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@je", je);
            sp[1] = new SqlParameter("@Svdate", DateTime.Now.ToString("yyyy-MM-dd"));
            sp[2] = new SqlParameter("@UserID", "MguliN");
            return sh.InsertNew(mSql, sp);
        }

        //新增信息
        public string AddGameInfo(string svstr)
        {
            string[] svarr = svstr.Split('@');
            string mSql = string.Format("insert into tb_game (userid,console,name,price,buydate,remark) values (@userid,@console,@name,@price,@buydate,@remark); SELECT @@IDENTITY;");

            SqlParameter[] sp = new SqlParameter[svarr.Length + 2];
            for (int i = 0; i < svarr.Length; i++)
            {
                sp[i] = new SqlParameter("@" + svarr[i].Split('|')[0], svarr[i].Split('|')[1]);
            }

            sp[svarr.Length] = new SqlParameter("@userid", "MguliN");
            sp[svarr.Length + 1] = new SqlParameter("@remark", "");

            return sh.InsertNew(mSql, sp);
        }

        //更新信息
        public string UpdGameInfo(string svstr, string id)
        {
            string[] svarr = svstr.Split('@');
            string mSql = string.Format("update tb_game set console = @console,name=@name,price=@price,buydate=@buydate,remark=@remark where id=@id;");

            SqlParameter[] sp = new SqlParameter[svarr.Length + 3];
            for (int i = 0; i < svarr.Length; i++)
            {
                sp[i] = new SqlParameter("@" + svarr[i].Split('|')[0], svarr[i].Split('|')[1]);
            }

            sp[svarr.Length] = new SqlParameter("@userid", "MguliN");
            sp[svarr.Length + 1] = new SqlParameter("@remark", "");
            sp[svarr.Length + 2] = new SqlParameter("@id", id);

            return sh.getNonQuery(mSql, sp).ToString();
        }

        //更新信息
        public string DelGameInfo(string id)
        {
            string mSql = string.Format("delete tb_game where id=@id;");
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@id", id);
            return sh.getNonQuery(mSql, sp).ToString();
        }

        public string GetNewGameListstr(string mWhereStr)
        {
            string mSql = "";
            //string mCSql = "";
            string mWhere = "";
            mSql = string.Format("SELECT * FROM tb_game ");
            //mCSql = string.Format("SELECT count(0) FROM tb_game where 1=1");
            if (mWhereStr != "")
            {
                mWhereStr = mWhereStr.Replace("year", "Year([buydate])");
                string[] mWhereArr = mWhereStr.Split(';');
                foreach (string War in mWhereArr)
                {
                    if (War.Split(':')[0] == "name")
                        mWhere += " and " + War.Split(':')[0] + " like '%" + War.Split(':')[1] + "%'";
                    else
                        mWhere += " and " + War.Split(':')[0] + " = '" + War.Split(':')[1] + "'";
                }
            }
            //mWhere = string.Format(" and console = '{0}'", console);
            mSql += mWhere + " order by tb_game.buydate desc";
            //mCSql += mWhere;
            //cmd.CommandText = mSql;
            //conn.Open();
            //connCount.Open();
            DataTable dt = new DataTable();
            dt = sh.getDT(mSql);
            string result = "";
            StringBuilder sb = new StringBuilder();
            result = sb.ToString();

            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    sb.Append("\"id\": \"" + dr["id"].ToString() + "\",");
                    sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
                    sb.Append("\"name\": \"" + dr["name"].ToString() + "\",");
                    sb.Append("\"price\": \"" + dr["price"].ToString() + "\",");
                    sb.Append("\"buydate\": \"" + Convert.ToDateTime(dr["buydate"]).ToString("yyyy-MM-dd") + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            //cmdCount.CommandText = mCSql;
            //string DCounts = sh.GetString(mCSql); // cmdCount.ExecuteScalar().ToString();


            result = sb.ToString();// + "&" + DCounts;

            return result;
        }

        //获取列表
        public string GetGameListstr(string mWhereStr, string pageindex, string pagesize)
        {
            if (pageindex == "")
                pageindex = "1";
            string mSql = "";
            string mCSql = "";
            string mWhere = "";
            mSql = string.Format("SELECT top {1} * FROM tb_game where id not in (SELECT top ({1}*({0}-1)) id FROM tb_game order by buydate desc)", pageindex, pagesize);
            mCSql = string.Format("SELECT count(0) FROM tb_game where 1=1");
            if (mWhereStr != "")
            {
                mWhereStr = mWhereStr.Replace("year", "Year([buydate])");
                string[] mWhereArr = mWhereStr.Split(';');
                foreach (string War in mWhereArr)
                {
                    if (War.Split(':')[0] == "name")
                        mWhere += " and " + War.Split(':')[0] + " like '%" + War.Split(':')[1] + "%'";
                    else
                        mWhere += " and " + War.Split(':')[0] + " = '" + War.Split(':')[1] + "'";
                }
            }
                //mWhere = string.Format(" and console = '{0}'", console);
            mSql += mWhere + " order by tb_game.buydate desc";
            mCSql += mWhere;
            //cmd.CommandText = mSql;
            //conn.Open();
            //connCount.Open();
            DataTable dt = new DataTable();
            dt = sh.getDT(mSql);
            string result = "";
            StringBuilder sb = new StringBuilder();
            result = sb.ToString();

            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    sb.Append("\"id\": \"" + dr["id"].ToString() + "\",");
                    sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
                    sb.Append("\"name\": \"" + dr["name"].ToString() + "\",");
                    sb.Append("\"price\": \"" + dr["price"].ToString() + "\",");
                    sb.Append("\"buydate\": \"" + Convert.ToDateTime(dr["buydate"]).ToString("yyyy-MM-dd") + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            //cmdCount.CommandText = mCSql;
            string DCounts = sh.GetString(mCSql); // cmdCount.ExecuteScalar().ToString();


            result = sb.ToString();// + "&" + DCounts;

            return result;
        }

        //获取详细信息
        public string GetGameInfostr(string id)
        {
            string mSql = "";
            mSql = string.Format("SELECT top 1 * FROM tb_game ");
            if (id != "")
                mSql += string.Format(" where id = '{0}'", id);
            mSql += " order by buydate desc";

            string result = "";
            StringBuilder sb = new StringBuilder();
            result = sb.ToString();

            DataTable dt = sh.getDT(mSql);
            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    sb.Append("\"id\": \"" + dr["id"].ToString() + "\",");
                    sb.Append("\"name\": \"" + dr["name"].ToString() + "\",");
                    sb.Append("\"price\": \"" + dr["price"].ToString() + "\",");
                    sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
                    sb.Append("\"buydate\": \"" + Convert.ToDateTime(dr["buydate"]).ToString("yyyy-MM-dd") + "\"");
                    sb.Append("}");
                }
                sb.Append("]");
            }
            result = sb.ToString();
            return result;
        }

        //获取总额
        public string GetSvstr(string nf)
        {
            int sumje = 0;
            //SqlConnection conn = new SqlConnection(sql);
            //SqlCommand cmd = conn.CreateCommand();
            string mSql = string.Format("select je from tb_plan_detail where YEAR(Svdate) = {0} ", nf);
            //conn.Open();
            DataTable dt = new DataTable();
            string result = "";
            //SqlDataReader dr = cmd.ExecuteReader();
            dt = sh.getDT(mSql);
            //cmd.ExecuteNonQuery();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sumje += int.Parse(dr[0].ToString());
                    result += dr[0].ToString() + ",";
                }
            }
            
            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1) + "@" + sumje.ToString();

            return result;
        }

        //按年份统计产品金额
        public string GameCount(string nf)
        {
            string mSql = "";
            mSql = string.Format("select SUM([price]) as ze ,a.[console],b.color from [tb_game] a INNER JOIN tb_SetConsoleCR b on a.[console] = b.console ");
            if (nf != "")
                mSql += string.Format(" where year([buydate]) = '{0}'", nf);
            mSql += " GROUP BY a.[console] ,b.color";

            string result = "";
            StringBuilder sb = new StringBuilder();
            result = sb.ToString();

            DataTable dt = sh.getDT(mSql);
            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
                    sb.Append("\"color\": \"" + dr["color"].ToString() + "\",");
                    sb.Append("\"ze\": \"" + dr["ze"].ToString() + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            result = sb.ToString();
            return result;
        }

        //统计各个平台的数量和价格
        public string GameStatForChart()
        {
            string mSql = "";
            mSql = string.Format("SELECT COUNT(0) AS sl, SUM(price) AS sje, console FROM dbo.tb_game GROUP BY console");

            string result = "";
            StringBuilder sb = new StringBuilder();
            result = sb.ToString();

            DataTable dt = sh.getDT(mSql);
            if (dt.Rows.Count > 0)
            {
                sb.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
                    sb.Append("\"sl\": \"" + dr["sl"].ToString() + "\",");
                    sb.Append("\"sje\": \"" + dr["sje"].ToString() + "\"");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
            }
            result = sb.ToString();
            return result;
        }
    }

}
