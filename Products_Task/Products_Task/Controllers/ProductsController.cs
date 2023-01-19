using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_Task.Models;
using System.Data;

namespace Products_Task.Controllers
{
    public class ProductsController : Controller
    {
        ProductsTaskDbContext context;
        public ProductsController(ProductsTaskDbContext _context)
        {
                context = _context;
        }
        public IActionResult Index()
        {
            List<Product> products = context.Products.Include("Supplier").ToList();
            return View(products);
        }
        public IActionResult AddProduct()
        {
            List<Supplier> suppliers = context.Suppliers.ToList();
            ViewBag.Suppliers= suppliers;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewproduct(Product product)
        {
            //product.Supplier = context.Suppliers.SingleOrDefault(s=>s.SupplierId==product.SuplierId);
                if(product.UnitInStock<=product.RecorderLevel &&product.UnitInOrder==0)
            {
                ModelState.AddModelError("UnitInOrder", "You Must Order Because your stock lower than recorder unit");
            }
            if(ModelState.IsValid)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Supplier> suppliers = context.Suppliers.ToList();
            ViewBag.Suppliers = suppliers;
            return View("AddProduct");
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult EditProduct(int id)
        {
            Product product = context.Products.Include("Supplier").SingleOrDefault(s=>s.ProductId==id);
            List<Supplier> suppliers = context.Suppliers.ToList();
            ViewBag.Suppliers = suppliers;
            return View(product);
        }
        public IActionResult EditProductSave(Product Editedproduct)
        {
            var product = context.Products.Find(Editedproduct.ProductId);

            try
            {
                if (ModelState.IsValid)
                {
                    product.ProductName = Editedproduct.ProductName;
                    product.RecorderLevel = Editedproduct.RecorderLevel;
                    product.SuplierId = Editedproduct.SuplierId;
                    product.QuantityPerUnit = Editedproduct.QuantityPerUnit;
                    product.UnitInStock = Editedproduct.UnitInStock;
                    product.UnitInOrder = Editedproduct.UnitInOrder;
                    product.UnitPrice = Editedproduct.UnitPrice;
                    context.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch
            {
                ModelState.AddModelError("Error", "Values Can't be null");
            }
            List<Supplier> suppliers = context.Suppliers.ToList();
            ViewBag.Suppliers = suppliers;
            return View("EditProduct",product);
        }
    }
}
