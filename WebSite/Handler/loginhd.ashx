<%@ WebHandler Language="C#" Class="loginhd" %>

using System;
using System.Web;
using System.Text;

public class loginhd : IHttpHandler {
    HttpContext dfcontext;
    Common.JsEncryptHelper jsHelper = new Common.JsEncryptHelper();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        //context.Request.ContentType="application/x-www-form-urlencoded; charset=UTF-8";
        //context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
        dfcontext = context;
        string requestType = GetParam("ct");
        string result = "";
        switch (requestType)
        {
            case "loginin":
                string a = GetParam("username") + "";
                string b = GetParam("passwd") + "";
                a = jsHelper.Decrypt(a);
                b = jsHelper.Decrypt(b);
                context.Response.Write(new { success = true, a = a, b = b });
                //context.Response.Write(a);
                break;
        }
        context.Response.Write(result);
    }

    private string GetParam(string key)
    {
        if (dfcontext.Request.Params[key] != null)
            return dfcontext.Request.Params[key].ToString();
        return "";
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}