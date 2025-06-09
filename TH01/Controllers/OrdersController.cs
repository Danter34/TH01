using Microsoft.AspNetCore.Mvc;
using TH01.Models.Interface; 
using TH01.Models; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims; 
using TH01.Data; 

namespace TH01.Controllers
{
    [Authorize] 
    public class OrdersController : Controller
    {
        private IOrderRepository orderRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private readonly CoffeeshopDbContext _context;

        public OrdersController(IOrderRepository orderRepository,
                                IShoppingCartRepository shoppingCartRepository,
                                CoffeeshopDbContext context) 
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            _context = context; 
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCartRepository.GetAllShoppingCartItems();
           
            if (ModelState.IsValid && (items != null && items.Any()))
            {
                // Thêm thông tin email của người dùng hiện tại vào Order
                // Điều này rất quan trọng để có thể hiển thị đơn hàng trong "MyOrders"
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (userEmail != null)
                {
                    order.Email = userEmail;
                }

                // Đặt ngày đặt hàng
                order.OrderPlaced = DateTime.Now;

                // Thêm trạng thái đơn hàng (nếu bạn đã thêm vào model Order)
                order.Status = "Pending"; // Mặc định là Pending khi mới đặt

                orderRepository.PlaceOrder(order); // Hàm này sẽ lưu Order và OrderDetails vào DB
                shoppingCartRepository.ClearCart();
                HttpContext.Session.SetInt32("CartCount", 0);

                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }

        // Hiển thị danh sách các đơn hàng của người dùng hiện tại
        public async Task<IActionResult> MyOrders()
        {
            // Lấy email của người dùng hiện tại đã đăng nhập từ Claims Principal
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                // Người dùng chưa đăng nhập hoặc không có email
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Truy vấn các đơn hàng dựa trên Email đã lưu trong bảng Order
            // và bao gồm thông tin chi tiết đơn hàng (OrderDetails)
            // cũng như thông tin sản phẩm (Product) trong từng OrderDetail
            var userOrders = await _context.Orders
                                            .Where(o => o.Email == userEmail) // Lọc đơn hàng theo Email
                                            .Include(o => o.OrderDetails)
                                                .ThenInclude(od => od.Product) // Bao gồm thông tin Product
                                            .OrderByDescending(o => o.OrderPlaced) 
                                            .ToListAsync();

            return View(userOrders);
        }
    }
}