using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// BaseHandler 的摘要说明
/// </summary>
public class BaseHandler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //登录验证
        if (context.Session["lgname"] == null)
        {
            context.Response.StatusCode = 405;
            context.Response.Write("123");
            context.Response.End();
        }
        OnLoad(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    public virtual void OnLoad(HttpContext context)
    {
    }
}