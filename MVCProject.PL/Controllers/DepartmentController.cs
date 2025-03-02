using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.BLL.Interfaces;
using MVCProject.DAL.Models;
using MVCProject.PL.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCProject.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Department> department;
            if (string.IsNullOrEmpty(SearchValue))
            {
                department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            }
            else
            {
                department = _unitOfWork.DepartmentRepository.GetByName(SearchValue);
            }
            var MappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);
            return View(MappedDepartment);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                await _unitOfWork.DepartmentRepository.AddAsync(MappedDepartment);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return NotFound();
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null) return BadRequest();
            var MappedDepartment = _mapper.Map<Department,DepartmentViewModel>(department);
            return View(ViewName, MappedDepartment);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
            if (id != departmentVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(MappedDepartment);
                    int count = await _unitOfWork.CompleteAsync();

                    if (count > 0)
                        TempData["Message"] = "Department Is Updated";
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
            if (id != departmentVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Delete(MappedDepartment);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentVM);
        }
    }
}
