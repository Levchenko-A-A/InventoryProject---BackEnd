using System.Net;
using BackEnd.Controller;

HttpClient httpClient = new HttpClient();
HttpListener server = new HttpListener();
server.Prefixes.Add("http://127.0.0.1:8888/connection/");
server.Start();
while (true)
{
    var context = await server.GetContextAsync();       //ожидает входящие запросы
    var body = context.Request.InputStream;
    var method = context.Request.HttpMethod;
    Console.WriteLine(method);
    var encoding = context.Request.ContentEncoding;
    var reader = new StreamReader(body, encoding);
    string query = reader.ReadToEnd();
    string table = context.Request.Headers[0]!;
    if (method == "POST")
    {
        switch (table)
        {
            case "person": PersonController.AddPerson(query, context); break;
            case "verifyPasswordPerson": break;
        }
    }
    else if (method == "GET")
    {
        switch (table)
        {
            case "person": PersonController.getPerson(context); break;
        }
    }
    else if (method == "PUT")
    {
        switch (table)
        {
            case "person": PersonController.PutPerson(query, context); break;
        }
    }
    else if (method == "DELETE")
    {
        switch (table)
        {
            case "person": PersonController.delPerson(query, context); break;
        }
    }
}
server.Stop();