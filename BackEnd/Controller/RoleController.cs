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
    class RoleController
    {
        public async static void getRole(HttpListenerContext context)
        {
            using (DbinventoryContext db = new DbinventoryContext())
            {
                List<Role> roles = db.Roles.ToList();
                string json = JsonSerializer.Serialize<List<Role>>(roles);
                string responseText = json;
                SendResponse(context, responseText);
            }
        }
        public async static void getRoleId(string json, HttpListenerContext context)
        {
            int id = JsonSerializer.Deserialize<int>(json);
            using (DbinventoryContext db = new DbinventoryContext())
            {
                Role? role = await db.Roles.FirstOrDefaultAsync(p => p.Roleid == id);
                if (role != null)
                {
                    string jsonPerson = JsonSerializer.Serialize<Role>(role);
                    string responseText = jsonPerson;
                    SendResponse(context, responseText);
                }
            }
        }
        public async static void addRole(string json, HttpListenerContext context)
        {
            using (DbinventoryContext db = new DbinventoryContext())
            {
                string responseText;
                Role? roles = JsonSerializer.Deserialize<Role>(json);
                if (roles == null)
                {
                    SendResponse(context, "Ошибка: некорректные данные");
                }
                Role? user = await db.Roles.FirstOrDefaultAsync(u => u.Rolename == roles!.Rolename);
                if (user == null)
                {
                    db.Roles.Add(new Role()
                    {
                        Rolename = roles!.Rolename,
                        Description = roles.Description
                    });
                    await db.SaveChangesAsync();
                    responseText = "OK";
                }
                else
                    responseText = "Error";
                SendResponse(context, responseText);
            }
        }
        public async static void delRole(string json, HttpListenerContext context)
        {
            int id = JsonSerializer.Deserialize<int>(json);
            using (DbinventoryContext db = new DbinventoryContext())
            {
                string responseText;
                Role roles = db.Roles.Find(id)!;
                if (roles != null)
                {
                    db.Roles.Remove(roles);
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
        public async static void updateRole(string json, HttpListenerContext context)
        {
            using (DbinventoryContext db = new DbinventoryContext())
            {
                string responseText;
                Role? temp = JsonSerializer.Deserialize<Role>(json);
                if (temp == null)
                {
                    responseText = "error";
                }
                else
                {
                    if (await db.Roles.FindAsync(temp.Roleid) is Role found)
                    {
                        found.Rolename = temp.Rolename;
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
