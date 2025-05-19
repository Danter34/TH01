using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TH01.Models;
using TH01.Models.Interface;

namespace TH01.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        { 
            return View(productRepository.GetAllProducts());
        }
    }
}
