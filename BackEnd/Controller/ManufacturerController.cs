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
                var response = context.Response;
                string responseText = json;
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");
            }
        }
    }
}
