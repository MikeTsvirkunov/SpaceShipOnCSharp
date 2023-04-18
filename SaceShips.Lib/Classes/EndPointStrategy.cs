using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

public class EndPointStrategy : IStartegy
{
    private HttpListener listener;
    public EndPointStrategy(HttpListener listener)
    {
        this.listener = listener;
    }

    public object execute(params object[] args)
    {
        HttpListenerContext ctx = listener.GetContext();
        System.IO.Stream body = ctx.Request.InputStream;
        System.Text.Encoding encoding = ctx.Request.ContentEncoding;
        System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
        // string s = reader.ReadToEnd();
        return (object) reader.ReadToEnd();
    }
}

