using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;



namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {

        InMemoryRepository<ProductCategory> Context;

        public ProductCategoryManagerController()
        {
            Context = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productsCategories = Context.Collection().ToList();
            return View(productsCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productcategory = new ProductCategory();
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productcategory);
            }
            else
            {
                Context.Insert(productcategory);
                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory ProductCategory = Context.Find(Id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory, string Id)
        {
            ProductCategory productcategoryToEdit = Context.Find(Id);

            if (productcategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productcategory);
                }
                productcategoryToEdit.CategoryName = productcategory.CategoryName;


                Context.Commit();

                return RedirectToAction("Index");

            }
        }


        public ActionResult Delete(string Id)
        {
            ProductCategory productcategoryToDelete = Context.Find(Id);
            if (productcategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productcategoryToDelete = Context.Find(Id);
            if (productcategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(Id);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}