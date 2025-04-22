using Microsoft.AspNetCore.Mvc;
using TH01.Models.Interface;

namespace TH01.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public ActionResult Shop() 
        {
            return View(productRepository.GetAllProducts());
        }
    }
}
