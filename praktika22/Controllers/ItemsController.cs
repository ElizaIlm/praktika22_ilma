using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using praktika22.Data.Interfaces;
using praktika22.Data.Models;
using praktika22.Data.ViewModell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;

namespace praktika22.Controllers
{
	public class ItemsController : Controller
	{
        private readonly IHostingEnvironment hostingEnvironment;
        private IItems IAllItems;
		private ICategorys IAllCategorys;
		VMItems VMItems= new VMItems();
		
		public ItemsController(IItems IAllItems, ICategorys IAllCategorys, IHostingEnvironment environment)
		{
			this.IAllItems = IAllItems;
			this.IAllCategorys = IAllCategorys;
			this.hostingEnvironment = environment;
		}

		public ViewResult List( int id = 0)
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
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = files.FileName;
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categorys() { Id = idCategory };

            int id = IAllItems.Add(newItems);

            return Redirect("/Items/Update?id=" + id);
        }
    }
}
