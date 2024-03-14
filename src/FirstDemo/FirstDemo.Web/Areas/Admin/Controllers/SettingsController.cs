using Autofac;
using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly ILifetimeScope _scope;

        public SettingsController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IActionResult Roles()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            var model = _scope.Resolve<RoleCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleCreateModel model)
        {
            if(ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.CreateRole();
            }
            return RedirectToAction(nameof(Roles));
        }

        public async Task<IActionResult> AssignRole()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.LoadData();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleAssignModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.AssignRole();
            }
            return RedirectToAction(nameof(Roles));
        }

        public async Task<IActionResult> AssignClaim()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.AssignStaticClaim();
            return View();
        }
    }
}
