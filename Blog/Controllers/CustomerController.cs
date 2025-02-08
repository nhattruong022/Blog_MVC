using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                    ModelState.AddModelError("Email", "Email already exists");
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


        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var customer = _dataContext.Customers.FirstOrDefault(kh => kh.userName == model.userName);

                if (customer == null)
                {
                    ModelState.AddModelError("loi", "This customer does not exist");
                    return View();
                }

                //mã hóa mật khẩu
                var passwordHasher = new PasswordHasher<CustomerModel>();
                var passwordVerification = passwordHasher.VerifyHashedPassword(new CustomerModel(), customer.password, model.password);

                if (passwordVerification == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("loi", "Password is not correct");
                    return View();
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, customer.userName),
                        new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                        new Claim(ClaimTypes.Email, customer.email),
                        new Claim(ClaimTypes.Role, customer.role ?? "User")
                    };

                    //b1 tạo claim, b2 thêm các thuộc tính về đăng nhập, b3 thêm các thuộc tính về cookies

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, //code cookie tồn tại sau khi trình duyệt đóng 
                        RedirectUri = returnUrl ?? Url.Action("Index", "Home")
                    };

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly =true,  //để ngăn chặn JavaScript truy cập cookie, giúp tránh XSS.
						Secure = true, //chỉ gửi qua https
                        Expires = DateTime.UtcNow.AddDays(7), //code hết hạn sau 7 ngày
                        SameSite = SameSiteMode.Lax 
                    };

                    if (customer.role == "Admin")
                    {
                        HttpContext.SignInAsync("AdminCookie", new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        HttpContext.SignInAsync("UserCookie", new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

                        return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                    }
                }
            }
            return View();
        }




        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Customer");
        }

    }
}
