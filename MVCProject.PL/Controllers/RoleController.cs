using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRole);
            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(SearchValue);
                var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(new List<RoleViewModel>() { MappedRole });
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                var Result = await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null) return NotFound();
            var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
            return View(ViewName, MappedRole);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(id);
                    if (Role is null) return NotFound();
                    Role.Name = model.RoleName;
                    var Result = await _roleManager.UpdateAsync(Role);
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete( string id)
        {
            try
            {
                var Role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(Role);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }

}
