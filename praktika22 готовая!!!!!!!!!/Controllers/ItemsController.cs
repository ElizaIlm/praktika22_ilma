using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using praktika22.Data.ViewModell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;

namespace praktika22.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private IItems IAllItems;
        private ICategorys IAllCategorys;
        VMItems VMItems = new VMItems();

        public ItemsController(IItems IAllItems, ICategorys IAllCategorys, IHostingEnvironment environment)
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
            this.hostingEnvironment = environment;
        }

        public ViewResult List(int id = 0)
        {
            ViewBag.Title = "Страница с предметами";
            VMItems.Items = IAllItems.AllItems; 
            VMItems.Categorys = IAllCategorys.AllCategorys;
            VMItems.SelectCategory = id;
            return View(VMItems);
        }

        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }

        [HttpPost]
        public RedirectResult Add(string name, string description, IFormFile files, int price, int idCategory)
        {
            string fileName = null;

            if (files != null)
            {
                fileName = files.FileName;
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);

                // Создаем директорию, если её нет
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
            }

            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = fileName;
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categorys() { Id = idCategory };

            int id = IAllItems.Add(newItems);
            return Redirect("/Items/Update?id=" + id);
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            Items item = IAllItems.GetItem(id);
            IEnumerable<Categorys> Categorys = IAllCategorys.AllCategorys;

            ViewBag.Item = item;
            return View(Categorys);
        }

        [HttpPost]
        public RedirectResult Update(int id, string name, string description, IFormFile files, float price, int idCategory)
        {
            Items existingItem = IAllItems.GetItem(id);
            string fileName = existingItem.Img; 

            if (files != null)
            {
                fileName = files.FileName;
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);

                // Создаем директорию, если её нет
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(fileStream);
                }
            }

            Items updatedItem = new Items();
            updatedItem.Name = name;
            updatedItem.Description = description;
            updatedItem.Img = fileName;
            updatedItem.Price = Convert.ToInt32(price);
            updatedItem.Category = new Categorys() { Id = idCategory };

            IAllItems.Update(updatedItem, id);
            return Redirect("/Items/List");
        }
        [HttpPost]
        public RedirectResult Delete(int id)
        {
            IAllItems.Delete(id);
            return Redirect("/Items/List");
        }
        public ActionResult Basket(int IdItem= -1)
        {
            if(IdItem != -1)
            { 
                Startup.BasketItem.Add(new ItemsBasket(1, IAllItems.AllItems.Where(x=>x.Id == IdItem).First()));
            }
            return Json(Startup.BasketItem);
        }
    }
}