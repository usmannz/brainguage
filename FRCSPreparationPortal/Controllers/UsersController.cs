using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FRCSPreparationPortal.API;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Service.Contracts;
using Stripe;
using Stripe.Checkout;

namespace FRCSPreparationPortal.Controllers
{
    [Authorize, Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        private ICategoryService _categoryService;

        public UsersController(ILogger<UsersController> logger, IUserService userService, IConfiguration configuration, ICategoryService categoryService)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["AppSettings:StripeSettings:SecretKey"];
            _categoryService = categoryService;
        }


        [AllowAnonymous, HttpPost("auth")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginUser user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Something went wrong");
            }

            var response = await _userService.Authenticate(user.Email, user.Password);

            if (response == null)
            {

                return Unauthorized();
            }

            // else if (response.Tenant.StatusId != (int)TenantStatus.Active)
            // {

            //     return Ok(new { success = -1});
            // }

            else if (response.StatusId != (int)UserStatus.Active)
            {
                return Ok(new { success = -2});
            }

            return Ok(new { jwt = Jwt.Create(response), role = response.UserRoles, isEmail =response.isEmail, success = 1});
        }

        [HttpGet("GetAllDropDownUsers")]
        public async Task <ApiResponse<List<UserDropDown>>> GetAllDropDownUsers()
        {
            return await _userService.GetAllDropDownUsers();

        }
        [AllowAnonymous]
        [HttpGet("GetUserById")]
        public async Task<Users> GetUserById(int userId)
        {
            return await _userService.GetUserById(userId);
        }

        [AllowAnonymous,HttpPost("SignUpUser")]
        public async Task<IActionResult> SignUpUser([FromBody] SignUp signUp)
        {        
            return Ok(await _userService.SignUpUser(signUp));
        }
        [AllowAnonymous]
        [HttpPost("create-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] int userId)
        {
            var user =await GetUserById(userId);
            if (user == null) return NotFound("User not found");

            var product = await _categoryService.GetProductById(user.ProductsId);
            if (product == null) return NotFound("Product not found");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(product.Price * 100), // Stripe uses cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/success", // Redirect after success
                CancelUrl = "http://localhost:4200/cancel",  // Redirect after cancel
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }
    }
}
