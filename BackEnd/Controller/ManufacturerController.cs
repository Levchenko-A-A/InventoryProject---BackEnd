using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackEnd.Controller
{
    class ManufacturerController
    {
        public async static void getManufacturer(HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                List<Manufacturer> manufacturer = db.Manufacturers.ToList();
                string json = JsonSerializer.Serialize<List<Manufacturer>>(manufacturer);
                string responseText = json;
                SendResponse(context, responseText);
            }
        }
        public async static void SendResponse(HttpListenerContext context, string message)
        {
            var response = context.Response;
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            response.ContentLength64 = buffer.Length;
            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;
            using Stream output = response.OutputStream;
            await output.WriteAsync(buffer);
            await output.FlushAsync();
            Console.WriteLine("Запрос обработан");
        }
    }
}
