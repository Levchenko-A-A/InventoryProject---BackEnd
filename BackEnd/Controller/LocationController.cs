﻿using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controller
{
    class LocationController
    {
        public async static void getLocation(HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                List<Location> locations = db.Locations.ToList();
                string json = JsonSerializer.Serialize<List<Location>>(locations);
                string responseText = json;
                SendResponse(context, responseText);
            }
        }
        public async static void addLocation(string json, HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                string responseText;
                Location? locations = JsonSerializer.Deserialize<Location>(json);
                if (locations == null)
                {
                    SendResponse(context, "Ошибка: некорректные данные");
                }
                Location? user = await db.Locations.FirstOrDefaultAsync(u => u.Name == locations!.Name);
                if (user == null)
                {
                    db.Locations.Add(new Location()
                    {
                        Name = locations!.Name,
                        Description = locations.Description
                    });
                    await db.SaveChangesAsync();
                    responseText = "OK";
                }
                else
                    responseText = "Error";
                SendResponse(context, responseText);
            }
        }
        public async static void delLocation(string json, HttpListenerContext context)
        {
            int id = JsonSerializer.Deserialize<int>(json);
            using (TestdbContext db = new TestdbContext())
            {
                string responseText;
                Location locations = db.Locations.Find(id)!;
                if (locations != null)
                {
                    db.Locations.Remove(locations);
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
        public async static void updateLocation(string json, HttpListenerContext context)
        {
            using (TestdbContext db = new TestdbContext())
            {
                string responseText;
                Location? temp = JsonSerializer.Deserialize<Location>(json);
                if (temp == null)
                {
                    responseText = "error";
                }
                else
                {
                    if (await db.Locations.FindAsync(temp.Locationid) is Location found)
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
