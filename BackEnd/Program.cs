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
            case "category": CategoryController.addCategory(query, context); break;
            case "location": LocationController.addLocation(query, context); break;
            case "role": RoleController.addRole(query, context); break;
            case "personrole": PersonroleController.addPersonRole(query, context); break;
            case "device": DeviceController.addDevice(query, context); break;
            case "verifyPasswordPerson": PersonController.chekPassword(query, context); break;
        }
    }
    else if (method == "GET")
    {
        switch (table)
        {
            case "person":
                {
                    if (query == "getPersonAll") PersonController.getPerson(context);
                    else PersonController.getPersonId(query, context);
                }
                break;
            case "manufacturer": ManufacturerController.getManufacturer(context); break;
            case "category": CategoryController.getCategory(context); break;
            case "location": LocationController.getLocation(context); break;
            case "role":
                {
                    if (query == "getRolleAll") RoleController.getRole(context);
                    else RoleController.getRoleId(query, context);
                }
                break;

            case "personrole": PersonroleController.getPersonRole(context); break;
            case "device": DeviceController.getDevices(context); break;
        }
    }
    else if (method == "PUT")
    {
        switch (table)
        {
            case "manufacturer": ManufacturerController.updateManufacturer(query, context); break;
            case "category": CategoryController.updateCategory(query, context); break;
            case "location": LocationController.updateLocation(query, context); break;
            case "role": RoleController.updateRole(query, context); break;
            case "personrole": PersonroleController.updatePersonRole(query, context); break;
            case "device": DeviceController.updateDevice(query, context); break;
        }
    }
    else if (method == "DELETE")
    {
        switch (table)
        {
            case "person": PersonController.delPerson(query, context); break;
            case "manufacturer": ManufacturerController.delManufacturer(query, context); break;
            case "category": CategoryController.delCategory(query, context); break;
            case "location": LocationController.delLocation(query, context); break;
            case "role": RoleController.delRole(query, context); break;
            case "personrole": PersonroleController.delPersonRole(query, context); break;
            case "device": DeviceController.delDevice(query, context); break;
        }
    }
}
server.Stop();