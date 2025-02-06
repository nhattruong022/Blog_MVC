using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DataContext _dataContext;

        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                //kiểm tra email có tồn tại chưa
                var existingUser = _dataContext.Customers.FirstOrDefault(c => c.email == model.email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "Email already exists");
                    return View(model);
                }


                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already Exist");
                    return View(model);
                }

                //mã hóa mật khẩu
                var passwordHashed = new PasswordHasher<CustomerModel>();
                var hashedPassword = passwordHashed.HashPassword(new CustomerModel(), model.password);

                //tạo khách hàng mới
                var newCustomer = new CustomerModel
                {
                    Name = model.Hoten,
                    email = model.email,
                    phone = model.phone,
                    userName = model.Username,
                    password = hashedPassword,
                    role = model.role ?? "User",             
                };

                _dataContext.Customers.Add(newCustomer);
                _dataContext.SaveChanges();

                //chuyển về Login sau khi đăng ký thành công
                return RedirectToAction("Login", "Customer");

            }
            //nếu modelstate ko hợp lệ
            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }

   

        public IActionResult LogOut()
        {
            return View();
        }
    }
}
