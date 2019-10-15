<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Text;

public class Handler : IHttpHandler
{
    HttpContext dfcontext;
    Data.Game gm = new Data.Game();
    string sql = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        //context.Request.ContentType="application/x-www-form-urlencoded; charset=UTF-8";
        context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
        dfcontext = context;
        string requestType = GetParam("ct");
        string result = "";
        switch (requestType)
        {
            case "SaveIn":
                result = gm.SaveIn(GetParam("sJE"));
                break;
            case "GetSvstr":
                result = gm.GetSvstr(DateTime.Now.Year.ToString());
                break;
            case "AddGameInfo":
                result = gm.AddGameInfo(GetParam("svstr"));
                break;
            case "GetNewGameList":
                result = gm.GetNewGameListstr(GetParam("mWhere"));
                break;
            case "GetGameList":
                result = gm.GetGameListstr(GetParam("mWhere"), GetParam("pages"), GetParam("pagesize")).ToString();
                break;
            case "GetGameInfo":
                result = gm.GetGameInfostr(GetParam("id"));
                break;
            case "UpdGameInfo":
                result = gm.UpdGameInfo(GetParam("svstr"), GetParam("id"));
                break;
            case "GameCount":
                result = gm.GameCount(GetParam("nf"));
                break;
            case "DelGameInfo":
                    result = gm.DelGameInfo(GetParam("id"));
                    break;
        }
        context.Response.Write(result);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private string GetParam(string key)
    {
        if (dfcontext.Request.Params[key] != null)
            return dfcontext.Request.Params[key].ToString();
        return "";
    }



    //存入金额
    //public int SaveIn(string je)
    //{
    //    SqlConnection conn = new SqlConnection(sql);
    //    SqlCommand cmd = conn.CreateCommand();
    //    cmd.CommandText = string.Format("insert into tb_plan_detail (je,Svdate) values ({0},'{1}')",je,DateTime.Now.ToString("yyyy-MM-dd"));
    //    conn.Open();
    //    int result = cmd.ExecuteNonQuery();

    //    //关闭数据库
    //    conn.Close();
    //    return result;
    //}

    //public string AddGameInfo(string svstr)
    //{
    //    string[] svarr = svstr.Split('@');
    //    SqlConnection conn = new SqlConnection(sql);
    //    SqlCommand cmd = conn.CreateCommand();
    //    cmd.CommandText = string.Format("insert into tb_game (userid,console,name,price,buydate,remark) values (@userid,@console,@name,@price,@buydate,@remark); SELECT @@IDENTITY;");
    //    //cmd.Parameters.AddRange(p);
    //    for (int i = 0; i < svarr.Length; i++)
    //    {
    //        cmd.Parameters.AddWithValue("@" + svarr[i].Split('|')[0], svarr[i].Split('|')[1]);
    //    }
    //    cmd.Parameters.AddWithValue("@userid", "MguliN");
    //    cmd.Parameters.AddWithValue("@remark", "");
    //    conn.Open();
    //    string result = cmd.ExecuteScalar().ToString();

    //    //关闭数据库
    //    conn.Close();
    //    return result;
    //}

    //public string UpdGameInfo(string svstr, string id)
    //{
    //    string[] svarr = svstr.Split('@');
    //    SqlConnection conn = new SqlConnection(sql);
    //    SqlCommand cmd = conn.CreateCommand();
    //    cmd.CommandText = string.Format("update tb_game set console = @console,name=@name,price=@price,buydate=@buydate,remark=@remark where id=@id;");
    //    //cmd.Parameters.AddRange(p);
    //    for (int i = 0; i < svarr.Length; i++)
    //    {
    //        cmd.Parameters.AddWithValue("@" + svarr[i].Split('|')[0], svarr[i].Split('|')[1]);
    //    }
    //    cmd.Parameters.AddWithValue("@userid", "MguliN");
    //    cmd.Parameters.AddWithValue("@remark", "");
    //    cmd.Parameters.AddWithValue("@id", id);
    //    conn.Open();
    //    string result = cmd.ExecuteNonQuery().ToString();

    //    //关闭数据库
    //    conn.Close();
    //    return result;
    //}

    //public string GetGameListstr(string console,string pageindex,string pagesize)
    //{
    //    if (pageindex == "")
    //        pageindex = "1";
    //    SqlConnection conn = new SqlConnection(sql);
    //    SqlConnection connCount = new SqlConnection(sql);
    //    SqlCommand cmd = conn.CreateCommand();
    //    SqlCommand cmdCount = connCount.CreateCommand();
    //    string mSql = "";
    //    string mCSql = "";
    //    string mWhere = "";
    //    mSql = string.Format("SELECT top {1} * FROM tb_game where id not in (SELECT top ({1}*({0}-1)) id FROM tb_game order by buydate desc)", pageindex, pagesize);
    //    mCSql = string.Format("SELECT count(0) FROM tb_game ");
    //    if (console != "")
    //        mWhere = string.Format(" and console = '{0}'", console);
    //    mSql += mWhere + " order by tb_game.buydate desc";
    //    mCSql += mWhere;
    //    cmd.CommandText = mSql;
    //    conn.Open();
    //    connCount.Open();
    //    string result = "";
    //    StringBuilder sb = new StringBuilder();
    //    result = sb.ToString();
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        sb.Append("[");
    //        while (dr.Read())
    //        {
    //            sb.Append("{");
    //            sb.Append("\"id\": \"" + dr["id"].ToString() + "\",");
    //            sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
    //            sb.Append("\"name\": \"" + dr["name"].ToString() + "\",");
    //            sb.Append("\"price\": \"" + dr["price"].ToString() + "\",");
    //            sb.Append("\"buydate\": \"" + Convert.ToDateTime(dr["buydate"]).ToString("yyyy-MM-dd") + "\"");
    //            sb.Append("},");
    //        }
    //        sb.Remove(sb.Length - 1, 1);
    //        sb.Append("]");
    //    }
    //    cmdCount.CommandText = mCSql;
    //    string DCounts = cmdCount.ExecuteScalar().ToString();

    //    //关闭数据库
    //    conn.Close();
    //    connCount.Close();

    //    result = sb.ToString() + "&" + DCounts;

    //    return result;
    //}

    //public string GetGameInfostr(string id)
    //{
    //    SqlConnection conn = new SqlConnection(sql);
    //    SqlCommand cmd = conn.CreateCommand();
    //    string mSql = "";
    //    mSql = string.Format("SELECT top 1 * FROM tb_game ");
    //    if (id != "")
    //        mSql += string.Format(" where id = '{0}'", id);
    //    mSql += " order by buydate desc";
    //    cmd.CommandText = mSql;
    //    conn.Open();
    //    string result = "";
    //    StringBuilder sb = new StringBuilder();
    //    result = sb.ToString();
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        sb.Append("[");
    //        while (dr.Read())
    //        {
    //            sb.Append("{");
    //            sb.Append("\"id\": \"" + dr["id"].ToString() + "\",");
    //            sb.Append("\"name\": \"" + dr["name"].ToString() + "\",");
    //            sb.Append("\"price\": \"" + dr["price"].ToString() + "\",");
    //            sb.Append("\"console\": \"" + dr["console"].ToString() + "\",");
    //            sb.Append("\"buydate\": \"" + Convert.ToDateTime(dr["buydate"]).ToString("yyyy-MM-dd") + "\"");
    //            sb.Append("}");
    //        }
    //        sb.Append("]");
    //    }
    //    //关闭数据库
    //    conn.Close();
    //    result = sb.ToString();
    //    return result;
    //}

    //public string GetSvstr(string nf)
    //{
    //    int sumje = 0;
    //    SqlConnection conn = new SqlConnection(sql);
    //    SqlCommand cmd = conn.CreateCommand();
    //    cmd.CommandText = string.Format("select je from tb_plan_detail where YEAR(Svdate) = {0} ", nf);
    //    conn.Open();
    //    DataTable dt = new DataTable();
    //    string result = "";
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    //cmd.ExecuteNonQuery();
    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {
    //            sumje += int.Parse(dr[0].ToString());
    //            result += dr[0].ToString() + ",";
    //        }
    //    }

    //    //关闭数据库
    //    conn.Close();

    //    if (result.Length > 0)
    //        result = result.Substring(0, result.Length - 1) + "@" + sumje.ToString();

    //    return result;
    //}

}