using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Text;
using System.Net;
using System.Diagnostics;
using Hwdtech;
using System.Linq;
namespace XUnit.Coverlet.Collector;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class EP
{
    [Fact]
    public void EndPoint_test()
    {
        string url = "http://localhost:8000/";
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(url);
        var x = new EndPointStrategy(listener);
        string z = "";
        var values = new Dictionary<string, string>
            {
                { "type", "fire" },
                { "game id", "g1" },
                {"game item id", "spsh1"}
            };
        listener.Start();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);

            var content = new FormUrlEncodedContent(values);
            var massege = client.PostAsJsonAsync(url, values);
            z = (string) x.execute();

        }
        listener.Close();
        Assert.Equal("{\"type\":\"fire\",\"game id\":\"g1\",\"game item id\":\"spsh1\"}", z);
        var res = JsonSerializer.Deserialize<Dictionary<string, string>>(z);
        Assert.Equal(values["type"], res["type"]);
    }
}
