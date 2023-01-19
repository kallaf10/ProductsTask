using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using NuGet.Packaging.Signing;
using Products_Task.Models;

namespace Products_Task.Controllers
{
    public class SuppliersController : Controller
    {
         ProductsTaskDbContext context;
        public SuppliersController(ProductsTaskDbContext _context)
        {
                context = _context;
        }
        public IActionResult Index()
        {
            List<Supplier> suppliers = context.Suppliers.Include("Products").ToList();
            return View(suppliers);
        }
        public IActionResult AllProductsOfSupplier(int id)
        {
            var supplier = context.Suppliers.Find(id);
            ViewBag.SupplierName = supplier.SupplierName;
            List<Product> products = context.Products.Include("Supplier").Where(s => s.SuplierId == id).ToList();
            return View(products);
        }
        public IActionResult Addsupplier()
        {
            return View();
        }
        public IActionResult AddNewpSupplier(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                context.Suppliers.Add(supplier);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View("Addsupplier");
        }

        public IActionResult EditSupplier(int id)
        {
            Supplier supplier = context.Suppliers.Find(id);
            return View(supplier);
        }
        public IActionResult EditSupplierSave(Supplier EditedSupplier)
        {
            Supplier supplier = context.Suppliers.Find(EditedSupplier.SupplierId);
            if(ModelState.IsValid)
            {
                supplier.SupplierName = EditedSupplier.SupplierName;
                context.SaveChanges();
            return RedirectToAction("Index");
            }
            return View("EditSupplier");
        }
        public IActionResult DeleteSupplier(int id)
        {
            Supplier supplier = context.Suppliers.Include("Products").SingleOrDefault(s=>s.SupplierId==id);
            if(supplier.Products.Count==0)
            {
                context.Suppliers.Remove(supplier);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error Message"] = "This Supplier has products So Cannot Delete him move his product to " +
                    "another supplier or delete product first then delete supplier ";
                return RedirectToAction("Index");
            }

        }
    }
}
