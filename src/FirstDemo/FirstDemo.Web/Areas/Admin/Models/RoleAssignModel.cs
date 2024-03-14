using Autofac;
using FirstDemo.Application.Features.Training.Services;
using FirstDemo.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class RoleAssignModel
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Username { get; set; }
        public List<SelectListItem>? Users { get; private set; }
        public List<SelectListItem>? Roles { get; private set; }


        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public RoleAssignModel()
        {

        }

        public RoleAssignModel(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        internal async Task LoadData()
        {
            Users = await (from c in _userManager.Users
             select new SelectListItem($"{c.FirstName} {c.LastName}", c.UserName))
            .ToListAsync();

            Roles = await (from c in _roleManager.Roles
                     select new SelectListItem(c.Name, c.Name))
                     .ToListAsync();
        }

        internal async Task AssignRole()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(Username);
            await _userManager.AddToRoleAsync(user, RoleName);
        }

        internal async Task AssignStaticClaim()
        {
            ApplicationUser user = await _userManager.FindByNameAsync("mizanur@yahoo.com");
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("ViewCourse", "true"));
        }
    }
}
