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
    var encoding = context.Request.ContentEncoding;
    var reader = new StreamReader(body, encoding);
    string query = reader.ReadToEnd();
    string table = context.Request.Headers[0]!;
    Console.WriteLine($"Metod: {method}");
    Console.WriteLine($"Table: {table}");
    if (method == "POST")
    {
        switch (table)
        {
            case "person": PersonController.addPerson(query, context); break;
            case "manufacturer": ManufacturerController.addManufacturer(query, context); break;
            case "verifyPasswordPerson": PersonController.chekPassword(query, context); break;
        }
    }
    else if (method == "GET")
    {
        switch (table)
        {
            case "person": PersonController.getPerson(context); break;
            case "manufacturer": ManufacturerController.getManufacturer(context); break;
        }
    }
    else if (method == "PUT")
    {
        switch (table)
        {
            case "person": PersonController.putPerson(query, context); break;
        }
    }
    else if (method == "DELETE")
    {
        switch (table)
        {
            case "person": PersonController.delPerson(query, context); break;
            case "manufacturer": ManufacturerController.delManufacturer(query, context); break;
        }
    }
}
server.Stop();