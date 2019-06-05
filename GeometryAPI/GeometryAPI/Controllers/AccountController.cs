using GeometryAPI.Common;
using GeometryAPI.Controllers.Base;
using GeometryAPI.Data;
using GeometryAPI.Data.Response;
using GeometryAPI.Entity;
using GeometryAPI.Entity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeometryAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseUtilsController
    {
        #region DataBase
        DbPostgreContext db;

        public AccountController(DbPostgreContext postrgreContext)
        {
            db = postrgreContext;
        }
        #endregion

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                string PasswordHash = GetHash(loginModel.Password);

                UserEntity user = await db.Users
                    .Include(u => u.Role)
                    .Include(g => g.Gender)
                    .Include(s => s.ShoppingCartLink)
                       .FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == PasswordHash);

                if (user != null)
                {
                    ClaimsIdentity identity = GetIdentity(user);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                var response = new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    GenderId = user.GenderId,
                    Name = user.Name,
                    LastName = user.LastName,
                    RoleId = user.RoleId
                };

                return Json(response);


            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные");
                }

                UserEntity user = await db.Users.FirstOrDefaultAsync(u => u.Email == registerModel.Email);

                if (user != null)
                {
                    return BadRequest("Такой пользователь уже существует");
                }

                string PasswordHash = GetHash(registerModel.Password);

                UserEntity userEntity = new UserEntity()
                {
                    Email = registerModel.Email,
                    GenderId = registerModel.GenderId,
                    Name = registerModel.Name,
                    LastName = registerModel.LastName,
                    Password = PasswordHash,
                    RoleId = GetCustomerRole()
                };

                await db.Users.AddAsync(userEntity);
                await db.SaveChangesAsync();

                UserResponse response = new UserResponse()
                {
                    Email = userEntity.Email,
                    GenderId = userEntity.GenderId,
                    Name = userEntity.Name,
                    LastName = userEntity.LastName,
                    RoleId = userEntity.RoleId,
                    Id = userEntity.Id
                };

                ClaimsIdentity identity = GetIdentity(userEntity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Json(response);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        #region Приватный регион
        private ClaimsIdentity GetIdentity(UserEntity user)
        {
            try
            {
                var claims = new List<Claim>
                {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.LastName),
                        new Claim(ClaimTypes.Gender, user.Gender.Name),
                        new Claim(CustomClaimType.UserId, user.Id.ToString()),
                        new Claim(CustomClaimType.ShoppingCart , user.ShoppingCartLink.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "GeometryCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        private Guid GetCustomerRole()
        {
            try
            {
                var role = db.Roles.FirstOrDefaultAsync(r => r.Name == "Customer").Result;

                return role.Id;
            }
            catch (Exception e)
            {
                return Guid.Empty;
            }
        }

        #endregion



    }
}
