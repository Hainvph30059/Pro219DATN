using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pro219.API.DTOs;
using Pro219.API.Utilities;
using Pro219.DAL.Models;
using Pro219.DAL.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pro219.API.Controllers
{
    [Route("Access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        CustomerRepository _customerRepository;
        UserRepository _userRepository;

        private readonly IConfiguration _configuration;
        private readonly TimeZoneInfo _gmtPlus7 = TimeZoneInfo.CreateCustomTimeZone("GMT+7", TimeSpan.FromHours(7), "GMT+7", "GMT+7");

        public AccessController(IConfiguration configuration, CustomerRepository customerRepository, UserRepository userRepository)
        {
            _configuration = configuration;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        [HttpPost("LoginCustomer")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            string username = loginModel.Username;
            string passwordHash = loginModel.PasswordHash;
            _customerRepository = new CustomerRepository();
            Customer customer = _customerRepository.GetByKeyAndPassword(username, passwordHash).Result;
            if (customer != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginModel.Username),
                new Claim(ClaimTypes.Role, "Customer"),
                 new Claim(ClaimTypes.Email, customer.Email),
                  new Claim(ClaimTypes.Name, customer.FullName),
                     new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var gmtPlus7Now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _gmtPlus7);
                var expirationGmt7 = gmtPlus7Now.AddMinutes(50);
                var expirationUtc = TimeZoneInfo.ConvertTimeToUtc(expirationGmt7, _gmtPlus7);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: expirationUtc,
                    signingCredentials: creds
                );

                return Ok(new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expirationGmt7,
                    LoginSuccess = true
                });
            }
            else
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Token = null,
                    Expiration = DateTime.MinValue,
                    LoginSuccess = false
                });
            }
        }

        [HttpPost("LoginStaff")]
        public IActionResult LoginStaff([FromBody] LoginModel loginModel)
        {
            string username = loginModel.Username;
            string passwordHash = loginModel.PasswordHash;
            _userRepository = new UserRepository();
            User user = _userRepository.GetByKeyAndPassword(username, passwordHash).Result;
            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginModel.Username),
                new Claim(ClaimTypes.Role, user.Role),
                 new Claim(ClaimTypes.Name, user.UserName)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var gmtPlus7Now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _gmtPlus7);
                var expirationGmt7 = gmtPlus7Now.AddMinutes(180);
                var expirationUtc = TimeZoneInfo.ConvertTimeToUtc(expirationGmt7, _gmtPlus7);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: expirationUtc,
                    signingCredentials: creds
                );

                return Ok(new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expirationGmt7,
                    LoginSuccess = true
                });
            }
            else
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Token = null,
                    Expiration = DateTime.MinValue,
                    LoginSuccess = false
                });
            }
        }

        [HttpGet("Check")]
        [Authorize]
        public IActionResult GetSecureData()
        {
            try
            {
                DateTime? expirationTime = null;
                bool isExpired = false;

                var authHeader = Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Replace("Bearer ", "");
                    if (!string.IsNullOrEmpty(token))
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(token);
                        var expirationUtc = jsonToken.ValidTo;
                        expirationTime = TimeZoneInfo.ConvertTimeFromUtc(expirationUtc, _gmtPlus7);
                        isExpired = expirationUtc < DateTime.UtcNow;
                        
                        if (isExpired)
                        {
                            return Unauthorized(new { message = "Token has expired", isExpired = true, expirationTime = expirationTime });
                        }
                    }
                }

                var userInfo = new
                {
                    username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    role = User.FindFirst(ClaimTypes.Role)?.Value,
                    email = User.FindFirst(ClaimTypes.Email)?.Value,
                    fullName = User.FindFirst(ClaimTypes.Name)?.Value,
                    phoneNumber = User.FindFirst(ClaimTypes.MobilePhone)?.Value,
                    expirationTime = expirationTime,
                    isExpired = isExpired
                };

                return Ok(userInfo);
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new { message = "Token has expired", isExpired = true });
            }
            catch
            {
                return Unauthorized(new { message = "Invalid token", isExpired = true });
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> RegisterUser([FromBody] RegisterModel registerModel)
        {
            Customer cus = new Customer();
            cus.PhoneNumber = registerModel.PhoneNumber;
            cus.Email = registerModel.Email;
            cus.FullName = registerModel.FullName;
            cus.DateOfBirth = registerModel.DateOfBirth ?? DateTime.Now;
            cus.CreateAt = DateTime.Now;
            cus.PasswordHash = registerModel.PasswordHash;
            cus.Status = "1";
            _customerRepository = new CustomerRepository();
            var user = _customerRepository.FindCustomerExistByKeyWord(cus.Email).Result;
            if (user == null)
            {
                Customer a = _customerRepository.AddCustomer(cus).Result;
                return Ok(a);
            }
            else
            {
                return NotFound("Dumplicate Email,Phone");

            }
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            if (string.IsNullOrEmpty(resetPasswordModel.PhoneNumber) || string.IsNullOrEmpty(resetPasswordModel.Email) || string.IsNullOrEmpty(resetPasswordModel.NewPassword))
            {
                return BadRequest("Phone number, email, and new password are required");
            }

            _customerRepository = new CustomerRepository();
            var customer = await _customerRepository.FindCustomerByEmailAndPhone(resetPasswordModel.Email, resetPasswordModel.PhoneNumber);
            
            if (customer == null)
            {
                return NotFound("Customer not found with the provided email and phone number");
            }

            UtilityFunc utilityFunc = new UtilityFunc();
            string hashedPassword = utilityFunc.HashPassword(resetPasswordModel.NewPassword);
            customer.PasswordHash = hashedPassword;
            customer.LastLogin = null;

            var updatedCustomer = await _customerRepository.UpdateCustomer(customer);
            
            if (updatedCustomer == null)
            {
                return StatusCode(500, "Failed to update password");
            }

            return Ok(true);
        }
    }

}
