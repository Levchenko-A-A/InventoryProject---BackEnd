using BackEnd.Model;
using Microsoft.EntityFrameworkCore;
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
        public async static void addManufacturer(string json, HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                string responseText;
                Manufacturer? manufacturer = JsonSerializer.Deserialize<Manufacturer>(json);
                if (manufacturer == null)
                {
                    SendResponse(context, "Ошибка: некорректные данные");
                }
                Manufacturer? user = await db.Manufacturers.FirstOrDefaultAsync(u => u.Name == manufacturer!.Name);
                if (user == null)
                {
                    db.Manufacturers.Add(new Manufacturer()
                    {
                        Name = manufacturer!.Name,
                        Description = manufacturer.Description
                    });
                    await db.SaveChangesAsync();
                    responseText = "OK";
                }
                else
                    responseText = "Error";

                var response = context.Response;
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
        public async static void delManufacturer(string json, HttpListenerContext context)
        {
            int id = JsonSerializer.Deserialize<int>(json);
            using (TestdbContext db = new TestdbContext())
            {
                string responseText;
                Manufacturer manufacturer = db.Manufacturers.Find(id)!;
                if (manufacturer != null)
                {
                    db.Manufacturers.Remove(manufacturer);
                    await db.SaveChangesAsync();
                    responseText = "OK";
                }
                else
                {
                    responseText = "Error";
                }
                SendResponse(context, responseText);
            }
        }
        public async static void updateManufacturer(string json, HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                string responseText;
                Manufacturer? temp = JsonSerializer.Deserialize<Manufacturer>(json);
                if (temp == null)
                {
                    responseText = "error";
                }
                else
                {
                    if (await db.Manufacturers.FindAsync(temp.Manufacturerid) is Manufacturer found)
                    {
                        found.Name = temp.Name;
                        found.Description = temp.Description;
                    }
                    await db.SaveChangesAsync();
                    responseText = "OK";
                }
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
