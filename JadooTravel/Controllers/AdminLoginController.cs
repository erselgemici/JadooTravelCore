using JadooTravel.Dtos.AdminDtos;
using JadooTravel.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminLoginController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("AdminUser") != null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAdminDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var admin = await _adminService.LoginAsync(dto);

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminUser", admin.Username);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View(dto);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "AdminLogin");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("AdminUser") == null)
                return RedirectToAction("Login");

            var admins = await _adminService.GetAllAdminsAsync();
            return View(admins);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //if (HttpContext.Session.GetString("AdminUser") == null)
            //    return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminDto dto)
        {
            await _adminService.CreateAdminAsync(dto);
            TempData["Success"] = "Yeni admin eklendi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin == null) return NotFound();

            var model = new UpdateAdminDto
            {
                Id = admin.Id,
                Username = admin.Username
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdminDto dto)
        {
            await _adminService.UpdateAdminAsync(dto);
            TempData["Success"] = "Admin güncellendi.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _adminService.DeleteAdminAsync(id);
            TempData["Success"] = "Admin silindi.";
            return RedirectToAction("Index");
        }
    }
}
