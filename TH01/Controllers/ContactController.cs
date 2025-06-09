using Microsoft.AspNetCore.Mvc;
using TH01.Data;
using TH01.Models;
using TH01.Models.Interface;

namespace TH01.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactService;

        public ContactController(IContactRepository contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View(new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(Contact model)
        {
            if (ModelState.IsValid)
            {
                bool success = await _contactService.SendContactMessageAsync(model);

                if (success)
                {
                    ViewBag.SuccessMessage = "Tin nhắn của bạn đã được gửi thành công. Chúng tôi sẽ liên hệ lại với bạn sớm nhất!";
                    ModelState.Clear();
                    return View("Index", new Contact());
                }

                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi gửi tin nhắn. Vui lòng thử lại.";
                return View("Index", model);
            }

            ViewBag.ErrorMessage = "Vui lòng kiểm tra lại thông tin bạn đã nhập.";
            return View("Index", model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
