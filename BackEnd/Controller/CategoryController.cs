﻿using BackEnd.Model;
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
    class CategoryController
    {
        public async static void getCategory(HttpListenerContext context)
        {
            using (DbinventoryContext db = new DbinventoryContext())
            {
                List<Category> categories = db.Categories.ToList();
                string json = JsonSerializer.Serialize<List<Category>>(categories);
                string responseText = json;
                SendResponse(context, responseText);
            }
        }
        public async static void getCategoryId(string json, HttpListenerContext context)
        {
            int id = JsonSerializer.Deserialize<int>(json);
            using (DbinventoryContext db = new DbinventoryContext())
            {
                Category? category = await db.Categories.FirstOrDefaultAsync(p => p.Categoryid == id);
                if (category != null)
                {
                    Console.WriteLine(category.Name);
                    string jsonPerson = JsonSerializer.Serialize<Category>(category);
                    string responseText = jsonPerson;
                    SendResponse(context, responseText);
                }
            }
        }
        public async static void addCategory(string json, HttpListenerContext context)
        {
            using (DbinventoryContext db = new DbinventoryContext())
            {
                string responseText;
                Category? categories = JsonSerializer.Deserialize<Category>(json);
                if (categories == null)
                {
                    SendResponse(context, "Ошибка: некорректные данные");
                }
                Category? user = await db.Categories.FirstOrDefaultAsync(u => u.Name == categories!.Name);
                if (user == null)
                {
                    db.Categories.Add(new Category()
                    {
                        Name = categories!.Name,
                        Description = categories.Description
                    });
                    await db.SaveChangesAsync();
                    responseText = "OK";
                }
                else
                    responseText = "Error";
                SendResponse(context, responseText);
            }
        }
        public async static void delCategory(string json, HttpListenerContext context)
        {
            int id = JsonSerializer.Deserialize<int>(json);
            using (DbinventoryContext db = new DbinventoryContext())
            {
                string responseText;
                Category categories = db.Categories.Find(id)!;
                if (categories != null)
                {
                    db.Categories.Remove(categories);
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
        public async static void updateCategory(string json, HttpListenerContext context)
        {
            using (DbinventoryContext db = new DbinventoryContext())
            {
                string responseText;
                Category? temp = JsonSerializer.Deserialize<Category>(json);
                if (temp == null)
                {
                    responseText = "error";
                }
                else
                {
                    if (await db.Categories.FindAsync(temp.Categoryid) is Category found)
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
