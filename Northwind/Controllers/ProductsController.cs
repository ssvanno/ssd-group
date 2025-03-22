using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
