using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.WebSockets;
using LearnAPI.Data;
using LearnAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace LearnAPI.Repositories
{
    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser,string> _signInManager;

        public AuthRepository()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        }
        public async Task<IdentityResult> RegisterUser(ApplicationUser _user)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = _user.UserName
            };

            var result = await _userManager.CreateAsync(user, _user.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();
        }
    }
}